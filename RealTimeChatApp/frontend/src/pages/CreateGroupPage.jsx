// // src/pages/CreateGroupPage.jsx
// import { useState } from "react";
// import { useNavigate } from "react-router-dom";
// import api from "../api/axios";

// export default function CreateGroupPage() {
//     const navigate = useNavigate();
//     const me = JSON.parse(localStorage.getItem("currentUser") || "{}");

//     const [groupName, setGroupName] = useState("");
//     const [usernames, setUsernames] = useState(""); // comma-separated

//     async function createGroup() {
//         const members = usernames
//             .split(",")
//             .map(u => u.trim())
//             .filter(Boolean);

//         // include the creator automatically
//         if (!members.includes(me.displayName)) members.push(me.displayName);

//         try {
//             await api.post("/Group/create", {
//                 name: groupName,
//                 memberUsernames: members // send usernames, not IDs
//             });
//             alert("Group created successfully!");
//             navigate("/chat"); // back to chat page
//         } catch (err) {
//             console.error(err);
//             alert("Failed to create group.");
//         }
//     }

//     return (
//         <div style={{ padding: 24 }}>
//             <h2>Create Group</h2>
//             <div style={{ marginBottom: 12 }}>
//                 <input
//                     placeholder="Group Name"
//                     value={groupName}
//                     onChange={(e) => setGroupName(e.target.value)}
//                     style={{ padding: 8, width: "100%", marginBottom: 8 }}
//                 />
//                 <input
//                     placeholder="Usernames (comma-separated)"
//                     value={usernames}
//                     onChange={(e) => setUsernames(e.target.value)}
//                     style={{ padding: 8, width: "100%" }}
//                 />
//             </div>
//             <button onClick={createGroup} style={{ padding: "8px 16px", background: "#007aff", color: "#fff", border: "none", borderRadius: 6 }}>
//                 Create Group
//             </button>
//         </div>
//     );
// }
