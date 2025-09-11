// import { useState } from "react";
// import api from "../api"; // make sure path is correct

// export default function UploadFile() {
//     const [file, setFile] = useState(null);
//     const [url, setUrl] = useState("");

//     const handleUpload = async () => {
//         if (!file) {
//             alert("Please select a file first!");
//             return;
//         }

//         const formData = new FormData();
//         formData.append("file", file);

//         try {
//             const res = await api.post("/files/upload", formData);
//             setUrl(res.data.attachmentUrl || res.data.url);
//             console.log("Upload successful", res.data);
//         } catch (err) {
//             console.error("Upload failed", err);
//             alert("Upload failed");
//         }
//     };

//     return (
//         <div style={{ padding: "20px" }}>
//             <h2>Upload File</h2>
//             <input
//                 type="file"
//                 onChange={(e) => setFile(e.target.files[0])}
//             />
//             <button onClick={handleUpload} style={{ marginLeft: "10px" }}>
//                 Upload
//             </button>

//             {url && (
//                 <div style={{ marginTop: "20px" }}>
//                     <p>File uploaded successfully!</p>
//                     <a href={url} target="_blank" rel="noreferrer">{url}</a>
//                     {(url.endsWith(".png") || url.endsWith(".jpg") || url.endsWith(".jpeg")) && (
//                         <div>
//                             <img src={url} alt="uploaded file" style={{ maxWidth: "300px", marginTop: "10px" }} />
//                         </div>
//                     )}
//                 </div>
//             )}
//         </div>
//     );
// }
