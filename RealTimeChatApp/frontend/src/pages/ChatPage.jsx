// src/pages/ChatPage.jsx
import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import { HubConnectionBuilder } from '@microsoft/signalr';


export default function ChatPage() {
    const navigate = useNavigate();
    const me = JSON.parse(localStorage.getItem("currentUser") || "{}"); // { id, email, displayName }

    const [allMessages, setAllMessages] = useState([]); // raw from API
    const [loading, setLoading] = useState(true);
    const [selectedKey, setSelectedKey] = useState(null); // e.g. "u:Neha", "g:2"
    const [composeText, setComposeText] = useState("");
    const [newChatName, setNewChatName] = useState("");
    const [showCreateGroup, setShowCreateGroup] = useState(false);
    const [newGroupName, setNewGroupName] = useState("");
    const [groupMembers, setGroupMembers] = useState("");
    const [creatingGroup, setCreatingGroup] = useState(false);
    const [editingGroupName, setEditingGroupName] = useState(false);
    const [editedGroupName, setEditedGroupName] = useState("");
    const [forceRefresh, setForceRefresh] = useState(0); // Force refresh counter
    const [selectedFile, setSelectedFile] = useState(null);

        // --- SIGNALR CONNECTION STATE ---
    const [connection, setConnection] = useState(null);

    const [groupParticipants, setGroupParticipants] = useState([]);
    const [showParticipants, setShowParticipants] = useState(false);
    const [addMemberName, setAddMemberName] = useState("");
    const [removeMemberName, setRemoveMemberName] = useState("");


    // Initialize SignalR connection
    useEffect(() => {
        const token = localStorage.getItem("token");
        const newConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:5054/hubs/chat", {
                accessTokenFactory: () => token
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    // Start connection and listen for messages
    useEffect(() => {
        if (!connection) return;

        const handleMessage = (sender, message, timestamp, receiverId) => {
            setAllMessages(prev => [...prev, {
                id: Date.now(),
                sender,
                receiver: receiverId,
                groupId: selectedKey?.startsWith("g:") ? parseInt(selectedKey.split(":")[1]) : 0,
                content: message,
                sentAtUtc: timestamp || new Date().toISOString()
            }]);
        };

        connection.start()
            .then(() => connection.on("ReceiveMessage", handleMessage))
            .catch(err => console.error("SignalR Connection Error:", err));

        return () => {
            if (connection) connection.off("ReceiveMessage", handleMessage);
        };
    }, [connection, selectedKey]);

    async function loadGroupParticipants(groupId) {
    try {
        const res = await api.get(`/Group/members/${groupId}`);
        setGroupParticipants(res.data || []);
        setShowParticipants(true);
    } catch (err) {
        console.error(err);
        alert("Failed to load participants.");
    }
}

async function addMemberToGroup() {
    if (!currentGroupId || !addMemberName.trim()) return;
    try {
        await api.post("/Group/add-members", {
            groupId: currentGroupId,
            usernames: [addMemberName.trim()]
        });
        alert(`Added ${addMemberName} to group.`);
        setAddMemberName("");
        loadGroupParticipants(currentGroupId);
    } catch (err) {
        console.error(err);
        alert("Failed to add member.");
    }
}

async function removeMemberFromGroup() {
    if (!currentGroupId || !removeMemberName.trim()) return;
    try {
        await api.post("/Group/remove-members", {
            groupId: currentGroupId,
            usernames: [removeMemberName.trim()]
        });
        alert(`Removed ${removeMemberName} from group.`);
        setRemoveMemberName("");
        loadGroupParticipants(currentGroupId);
    } catch (err) {
        console.error(err);
        alert("Failed to remove member.");
    }
}


    // fetch messages
    async function loadMessages() {
        setLoading(true);
        try {
            const res = await api.get("/Users/all-messages");
            // response shape expected: [{ Id, SenderId, ReceiverId, Sender, Receiver, GroupId, Content, AttachmentUrl, SentAtUtc }]
            setAllMessages(res.data || []);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        if (!localStorage.getItem("token")) {
            navigate("/login");
            return;
        }
        loadMessages();
    }, [navigate, forceRefresh]);

    // --- Send message via SignalR with file support ---
async function sendMessage() {
  if (!selectedKey || (!composeText.trim() && !selectedFile) || !connection) return;

  let fileUrl = null;

  try {
    // ✅ Upload file if selected
    if (selectedFile) {
      const formData = new FormData();
      formData.append("file", selectedFile);

      const response = await fetch("http://localhost:5054/api/files/upload", {
        method: "POST",
        headers: {
            Authorization: "Bearer " + localStorage.getItem("token"), // if you store token
        },
        body: formData,
      });

      const data = await response.json();
      console.log("Upload response:", data);

      fileUrl = `http://localhost:5054${data.attachmentUrl}`;
    //   fileUrl = data.attachmentUrl || data.url || data.path;
    }

    // ✅ Ensure SignalR connection is started
    if (connection.state !== "Connected") {
      await connection.start();
    }

    if (selectedKey.startsWith("g:")) {
      // --- Group message ---
      const groupId = parseInt(selectedKey.split(":")[1], 10);
      await connection.invoke("SendGroup", `Group_${groupId}`, composeText, fileUrl);

      await api.post("/Chat/send", {
        receiverId: null,
        groupId,
        content: composeText,
        attachmentUrl: fileUrl
      });

    } else {
      // --- Private message ---
      const username = selectedKey.slice(2);
      const userRes = await api.get(`/Users/find/${encodeURIComponent(username)}`);
      const receiverId = userRes.data.id;

      await connection.invoke("SendPrivate", receiverId, composeText, fileUrl);

      await api.post("/Chat/send", {
        receiverId,
        groupId: 0,
        content: composeText,
        attachmentUrl: fileUrl
      });
    }

    // ✅ Reset input
    setComposeText("");
    setSelectedFile(null);

    // Refresh messages after a delay
    setTimeout(() => {
      loadMessages();
      setForceRefresh(prev => prev + 1);
    }, 300);

  } catch (err) {
    console.error("Send message error:", err);
    alert("Failed to send message: " + (err.response?.data?.message || err.message || "Unknown error"));
  }
}


    
    // Extract group names from system messages (messages that start with "GROUP_NAME:")
    const extractGroupNamesFromMessages = useMemo(() => {
        const groupNamesMap = {};

        allMessages.forEach(message => {
            if (message.groupId && message.content && message.content.startsWith("GROUP_NAME:")) {
                const groupName = message.content.replace("GROUP_NAME:", "").trim();
                groupNamesMap[message.groupId] = groupName;
            }
        });

        return groupNamesMap;
    }, [allMessages]);

    // Build conversation list (users + groups) like WhatsApp left pane
    const conversations = useMemo(() => {
        // key -> { key, title, lastMessage, lastAt }
        const map = new Map();

        for (const m of allMessages) {
            const isGroup = m.groupId && m.groupId !== 0;

            if (isGroup) {
                const key = `g:${m.groupId}`;

                // Use group name from system messages if available, otherwise use "Group X"
                const title = extractGroupNamesFromMessages[m.groupId] || `Group ${m.groupId}`;
                const lastAt = new Date(m.sentAtUtc);

                // Skip system messages for preview
                const preview = m.content.startsWith("GROUP_NAME:")
                    ? "Group created"
                    : `${m.sender ?? "Unknown"}: ${m.content}`;

                const prev = map.get(key);
                if (!prev || new Date(prev.lastAt) < lastAt) {
                    map.set(key, { key, title, lastMessage: preview, lastAt, groupId: m.groupId });
                }
            } else {
                // personal: other person = if I'm the sender, other is Receiver; else Sender
                const other =
                    m.sender === me.displayName ? m.receiver : m.sender;

                if (!other) continue; // safeguard

                const key = `u:${other}`;
                const lastAt = new Date(m.sentAtUtc);
                const preview = `${m.sender === me.displayName ? "You" : m.sender}: ${m.content}`;

                const prev = map.get(key);
                if (!prev || new Date(prev.lastAt) < lastAt) {
                    map.set(key, { key, title: other, lastMessage: preview, lastAt });
                }
            }
        }

        // sort by most recent
        return Array.from(map.values()).sort((a, b) => new Date(b.lastAt) - new Date(a.lastAt));
    }, [allMessages, me.displayName, extractGroupNamesFromMessages]);

    // Get current group ID
    const currentGroupId = useMemo(() => {
        if (!selectedKey || !selectedKey.startsWith("g:")) return null;
        return parseInt(selectedKey.split(":")[1], 10);
    }, [selectedKey]);

    // Get current group name from system messages
    const currentGroupName = useMemo(() => {
        if (!currentGroupId) return null;
        return extractGroupNamesFromMessages[currentGroupId] || `Group ${currentGroupId}`;
    }, [currentGroupId, extractGroupNamesFromMessages]);



    


    // messages for selected conversation (filter out system messages)
    const visibleMessages = useMemo(() => {
        if (!selectedKey) return [];

        if (selectedKey.startsWith("g:")) {
            const groupId = parseInt(selectedKey.split(":")[1], 10);
            return allMessages
                .filter((m) => (m.groupId ?? 0) === groupId && !m.content.startsWith("GROUP_NAME:"))
                .sort((a, b) => new Date(a.sentAtUtc) - new Date(b.sentAtUtc));
        } else {
            const name = selectedKey.slice(2);
            return allMessages
                .filter(
                    (m) =>
                        (m.groupId ?? 0) === 0 &&
                        (m.sender === name || m.receiver === name || m.sender === me.displayName || m.receiver === me.displayName) &&
                        // keep only pairs between me and that user
                        ((m.sender === me.displayName && m.receiver === name) ||
                            (m.sender === name && m.receiver === me.displayName))
                )
                .sort((a, b) => new Date(a.sentAtUtc) - new Date(b.sentAtUtc));
        }
    }, [selectedKey, allMessages, me.displayName]);

    
    function startNewChat() {
        const name = newChatName.trim();
        if (!name) return;
        setSelectedKey(`u:${name}`);
        setNewChatName("");
    }

    async function createGroup() {
        if (!newGroupName.trim() || !groupMembers.trim()) {
            alert("Please provide both group name and member usernames");
            return;
        }

        setCreatingGroup(true);
        try {
            const memberList = groupMembers.split(",")
                .map(m => m.trim())
                .filter(m => m.length > 0);

            // Add current user to the group members
            if (!memberList.includes(me.displayName)) {
                memberList.push(me.displayName);
            }

            const response = await api.post("/Group/create", {
                Name: newGroupName,
                MemberUsernames: memberList
            });

            console.log("Group creation response:", response);

            // Extract group ID from response
            let groupId;
            if (response.data && response.data.Group && response.data.Group.Id) {
                groupId = response.data.Group.Id;
            } else if (response.data && response.data.group && response.data.group.id) {
                groupId = response.data.group.id;
            } else if (response.data && response.data.id) {
                groupId = response.data.id;
            } else {
                // If we can't find the ID in the response, try to extract it from the messages
                await loadMessages();
                const groupMessages = allMessages.filter(m => m.groupId && m.groupId !== 0);
                if (groupMessages.length > 0) {
                    const latestGroupId = Math.max(...groupMessages.map(m => m.groupId));
                    groupId = latestGroupId;
                } else {
                    throw new Error("Could not determine group ID from response");
                }
            }

            // Reset form
            setNewGroupName("");
            setGroupMembers("");
            setShowCreateGroup(false);

            // Send a system message with the group name - this will be visible to all participants
            try {
                await api.post("/Chat/send", {
                    receiverId: null,
                    groupId: groupId,
                    content: `GROUP_NAME:${newGroupName}`
                });
            } catch (systemMsgErr) {
                console.log("Could not send system message:", systemMsgErr);
                // Fallback: send regular message with group name
                await api.post("/Chat/send", {
                    receiverId: null,
                    groupId: groupId,
                    content: `Group "${newGroupName}" created!`
                });
            }

            // Force refresh to show the new group
            setForceRefresh(prev => prev + 1);

            // Select the newly created group
            setSelectedKey(`g:${groupId}`);

            // Refresh messages to ensure everything is up to date
            await loadMessages();

            alert(`Group "${newGroupName}" created successfully!`);
        } catch (err) {
            console.error("Group creation error:", err);
            alert("Failed to create group: " + (err.response?.data?.message || err.message || "Unknown error"));
        } finally {
            setCreatingGroup(false);
        }
    }

    async function updateGroupName() {
        if (!currentGroupId || !editedGroupName.trim()) return;

        try {
            // Send a system message to update the group name for all participants
            await api.post("/Chat/send", {
                receiverId: null,
                groupId: currentGroupId,
                content: `GROUP_NAME:${editedGroupName}`
            });

            setEditingGroupName(false);
            setEditedGroupName("");

            // Refresh messages to show the updated group name
            await loadMessages();

            alert("Group name updated for all participants!");
        } catch (err) {
            console.error(err);
            alert("Failed to update group name: " + (err.response?.data?.message || err.message));
        }
    }

    function startEditingGroupName() {
        if (!currentGroupId) return;
        setEditedGroupName(currentGroupName);
        setEditingGroupName(true);
    }

    function cancelEditingGroupName() {
        setEditingGroupName(false);
        setEditedGroupName("");
    }

    function logout() {
        localStorage.removeItem("token");
        localStorage.removeItem("currentUser");
        navigate("/login");
    }

    return (
        <div style={styles.wrapper}>
            {/* Sidebar */}
            <div style={styles.sidebar}>
                <div style={styles.sideHeader}>
                    <div>
                        <div style={{ fontWeight: 700 }}>{me.displayName || "Me"}</div>
                        <div style={{ fontSize: 12, opacity: 0.7 }}>{me.email}</div>
                    </div>
                    <button onClick={logout} style={styles.linkBtn}>Logout</button>
                </div>

                <div style={styles.newChatRow}>
                    <input
                        placeholder="Start new chat by username"
                        value={newChatName}
                        onChange={(e) => setNewChatName(e.target.value)}
                        style={styles.search}
                    />
                    <button onClick={startNewChat} style={styles.smallBtn}>Start</button>
                </div>

                <div style={{ padding: "12px" }}>
                    <button
                        style={{ ...styles.smallBtn, width: "100%", background: "#ff6d00" }}
                        onClick={() => setShowCreateGroup(!showCreateGroup)}
                    >
                        {showCreateGroup ? "Cancel" : "Create Group"}
                    </button>
                </div>

                {showCreateGroup && (
                    <div style={styles.groupForm}>
                        <input
                            placeholder="Group Name"
                            value={newGroupName}
                            onChange={(e) => setNewGroupName(e.target.value)}
                            style={styles.search}
                        />
                        <input
                            placeholder="Member usernames (comma separated)"
                            value={groupMembers}
                            onChange={(e) => setGroupMembers(e.target.value)}
                            style={styles.search}
                        />
                        <button
                            onClick={createGroup}
                            style={styles.smallBtn}
                            disabled={creatingGroup}
                        >
                            {creatingGroup ? "Creating..." : "Create Group"}
                        </button>
                    </div>
                )}

                <div style={styles.chatList}>
                    {loading ? (
                        <div style={{ padding: 12, color: "#666" }}>Loading…</div>
                    ) : conversations.length === 0 ? (
                        <div style={{ padding: 12, color: "#666" }}>No conversations yet.</div>
                    ) : (
                        conversations.map((c) => (
                            <div
                                key={c.key}
                                onClick={() => setSelectedKey(c.key)}
                                style={{
                                    ...styles.chatItem,
                                    background: selectedKey === c.key ? "#e8f0ff" : "transparent",
                                }}
                            >
                                <div style={styles.chatTitle}>{c.title}</div>
                                <div style={styles.chatPreview}>{c.lastMessage}</div>
                            </div>
                        ))
                    )}
                </div>
            </div>

            {/* Conversation */}
            <div style={styles.conversation}>
                {selectedKey ? (
                    <>
                        <div style={styles.convHeader}>
                            <div style={{ display: "flex", alignItems: "center", gap: 8 }}>
                                {editingGroupName && selectedKey.startsWith("g:") ? (
                                    <>
                                        <input
                                            value={editedGroupName}
                                            onChange={(e) => setEditedGroupName(e.target.value)}
                                            style={styles.editInput}
                                        />
                                        <button
                                            onClick={updateGroupName}
                                            style={styles.iconBtn}
                                            title="Save"
                                        >
                                            ✓
                                        </button>
                                        <button
                                            onClick={cancelEditingGroupName}
                                            style={styles.iconBtn}
                                            title="Cancel"
                                        >
                                            ✗
                                        </button>
                                    </>
                                ) : (
                                    <>
                                        <div style={{ fontWeight: 700 }}>
                                            {selectedKey.startsWith("g:")
                                                ? currentGroupName
                                                : selectedKey.slice(2)}
                                        </div>
                                        {selectedKey.startsWith("g:") && (
                                            <button
                                                onClick={startEditingGroupName}
                                                style={styles.iconBtn}
                                                title="Edit Group Name"
                                            >
                                                ✏️
                                            </button>
                                        )}
                                        {/* edit participants */}
                                        {selectedKey.startsWith("g:") && (
                                            <>
                                                <button
                                                    onClick={() => loadGroupParticipants(currentGroupId)}
                                                    style={styles.iconBtn}
                                                    title="Show Participants"
                                                >
                                                👥
                                        </button>
                                    </>
                                )}
                            </>
                            )}      
                                    </div>
                        </div>

                        <div style={styles.messages}>
                            {visibleMessages.map((m) => {
                                const mine = m.sender === me.displayName;
                                return (
                                    <div
                                        key={m.id + "-" + m.sentAtUtc}
                                        style={{
                                            display: "flex",
                                            justifyContent: mine ? "flex-end" : "flex-start",
                                            marginBottom: 8,
                                        }}
                                    >
                                        <div
                                            style={{
                                                maxWidth: "70%",
                                                background: mine ? "#daf8cb" : "#fff",
                                                border: "1px solid #e5e5e5",
                                                padding: "8px 10px",
                                                borderRadius: 10,
                                            }}
                                        >
                                            {selectedKey.startsWith("g:") && !mine && (
                                                <div style={{ fontSize: 12, opacity: 0.7, marginBottom: 4 }}>
                                                    {m.sender}
                                                </div>
                                            )}

                                            {/* ✅ Show text content */}
                                            {m.content && <div>{m.content}</div>}

                                            {/* ✅ Show attachment if available */}
                                            {m.attachmentUrl && (
                                                <div style={{ marginTop: 6 }}>
                                                <a
                                                    href={m.attachmentUrl}
                                                    target="_blank"
                                                    rel="noopener noreferrer"
                                                    style={{ color: "#007bff", textDecoration: "underline" }}
                                                >
                                                    📎 Download File
                                                </a>
                                                </div>
                                            )}

                                            <div style={{ fontSize: 10, opacity: 0.6, marginTop: 4 }}>
                                                {new Date(m.sentAtUtc).toLocaleString()}
                                            </div>
                                        </div>
                                    </div>
                                );
                            })}
                        </div>
                        {/* ✅ Step 4: Participants Panel */}
                        {showParticipants && selectedKey.startsWith("g:") && (
                            <div className="participants" style={{ padding: "12px", borderTop: "1px solid #eee" }}>
                                <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                                    <h4>Participants</h4>
                                    <button
                                        onClick={() => setShowParticipants(false)}
                                        style={{ background: "transparent", border: "none", cursor: "pointer", fontSize: 16 }}
                                        title="Close"
                                    >
                                    ✖
                                    </button>
                                </div>

                                {groupParticipants.map(p => (
                                    <div key={p.id} className="participant-row" style={{ display: "flex", justifyContent: "space-between", marginBottom: 6 }}>
                                        <span>{p.userName}</span>
                                        {/* <button onClick={() => removeMemberFromGroup(p.userName)}>❌</button> */}
                                    </div>
                                ))}

                                <div className="add-participant" style={{ marginTop: 8 }}>
                                    <input
                                        value={addMemberName}
                                        onChange={e => setAddMemberName(e.target.value)}
                                        placeholder="Enter username"
                                    />
                                    <button onClick={addMemberToGroup}>➕ Add</button>
                                </div>

                                <div className="remove-participant" style={{ marginTop: 8 }}>
                                    <input
                                        value={removeMemberName}
                                        onChange={e => setRemoveMemberName(e.target.value)}
                                        placeholder="Enter username to remove"
                                    />
                                    <button onClick={removeMemberFromGroup}>➖ Remove</button>
                                </div>
                            </div>
                        )}

                        <div style={styles.composer}> 
                            <input
                                type="file"
                                onChange={(e) => setSelectedFile(e.target.files[0])}
                                style={{ flex: 1, marginBottom: 4 }}
                            />

                            <input
                                placeholder="Type a message"
                                value={composeText}
                                onChange={(e) => setComposeText(e.target.value)}
                                style={styles.composeInput}
                                onKeyDown={(e) => e.key === "Enter" && sendMessage()}
                            />
                            <button onClick={sendMessage} style={styles.sendBtn}>Send</button>
                        </div>
                    </>
                ) : (
                    <div style={{ padding: 24, color: "#666" }}>Select a conversation to start chatting.</div>
                )}
            </div>
        </div>
    );
}

const styles = {
    wrapper: { display: "flex", height: "100vh", background: "#f1f5f9" },
    sidebar: { width: 360, borderRight: "1px solid #e5e5e5", background: "#fff", display: "flex", flexDirection: "column" },
    sideHeader: { padding: 12, display: "flex", alignItems: "center", justifyContent: "space-between", borderBottom: "1px solid #eee" },
    linkBtn: { background: "transparent", border: "none", color: "#007aff", cursor: "pointer", fontWeight: 600 },
    newChatRow: { padding: 12, display: "flex", gap: 8, borderBottom: "1px solid #eee" },
    search: { flex: 1, padding: "8px 10px", borderRadius: 8, border: "1px solid #ddd" },
    smallBtn: { padding: "8px 10px", borderRadius: 8, border: "none", background: "#007aff", color: "#fff", cursor: "pointer" },
    groupForm: { padding: 12, borderBottom: "1px solid #eee", display: "flex", flexDirection: "column", gap: 8 },
    chatList: { overflowY: "auto", flex: 1 },
    chatItem: { padding: 12, cursor: "pointer", borderBottom: "1px solid #f3f3f3" },
    chatTitle: { fontWeight: 700, marginBottom: 4 },
    chatPreview: { fontSize: 12, color: "#6a3e66ff" },
    conversation: { flex: 1, display: "flex", flexDirection: "column" },
    convHeader: { padding: 12, borderBottom: "1px solid #e5e5e5", background: "#fff", display: "flex", alignItems: "center", justifyContent: "space-between" },
    messages: { flex: 1, overflowY: "auto", padding: 16 },
    composer: { display: "flex", gap: 8, padding: 12, borderTop: "1px solid #e5e5e5", background: "#fff" },
    composeInput: { flex: 1, padding: "10px 12px", borderRadius: 20, border: "1px solid #ddd", outline: "none" },
    sendBtn: { padding: "10px 16px", borderRadius: 20, border: "none", background: "#00c853", color: "#fff", fontWeight: 700, cursor: "pointer" },
    editInput: { padding: "4px 8px", borderRadius: 4, border: "1px solid #ddd", outline: "none" },
    iconBtn: { background: "transparent", border: "none", cursor: "pointer", fontSize: 16, padding: 4 },
};









