// import { HubConnectionBuilder } from '@microsoft/signalr';
// import { useEffect, useState } from 'react';

// export default function Chat() {
//   const [messages, setMessages] = useState([]);
//   const [connection, setConnection] = useState(null);
//   const [input, setInput] = useState("");

//   useEffect(() => {
//     const token = localStorage.getItem("token"); // JWT token if used
//     const newConnection = new HubConnectionBuilder()
//       .withUrl("http://localhost:5054/hubs/chat", {
//         accessTokenFactory: () => token
//       })
//       .withAutomaticReconnect()
//       .build();

//     setConnection(newConnection);
//   }, []);

//   useEffect(() => {
//     if (connection) {
//       connection.start()
//         .then(() => console.log("Connected to SignalR!"))
//         .catch(err => console.error(err));

//       // Listen for private messages
//       connection.on("ReceiveMessage", (user, message, timestamp) => {
//         setMessages(prev => [...prev, { user, message, timestamp }]);
//       });

//       // Listen for group messages
//       connection.on("ReceiveGroupMessage", (user, message, timestamp) => {
//         setMessages(prev => [...prev, { user: `${user} (group)`, message, timestamp }]);
//       });

//       // Listen for typing indicator
//       connection.on("Typing", (user) => {
//         console.log(`${user} is typing...`);
//       });
//     }
//   }, [connection]);

//   const sendMessage = async () => {
//     if (connection && input) {
//       // Send private message
//       await connection.invoke("SendPrivate", "receiverUserIdHere", input); 
//       setInput("");
//     }
//   };

//   const sendGroupMessage = async () => {
//     if (connection && input) {
//       await connection.invoke("SendGroup", "groupNameHere", input);
//       setInput("");
//     }
//   };

//   return (
//     <div>
//       <div style={{ maxHeight: "300px", overflowY: "auto" }}>
//         {messages.map((m, i) => (
//           <div key={i}>
//             <b>{m.user}:</b> {m.message} <i>{m.timestamp}</i>
//           </div>
//         ))}
//       </div>
//       <input
//         value={input}
//         onChange={e => setInput(e.target.value)}
//         placeholder="Type a message"
//       />
//       <button onClick={sendMessage}>Send Private</button>
//       <button onClick={sendGroupMessage}>Send Group</button>
//     </div>
//   );
// }
