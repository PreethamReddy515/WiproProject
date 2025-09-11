// src/pages/Register.jsx
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../api/axios";

export default function Register() {
    const [displayName, setDisplayName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    async function handleRegister(e) {
        e.preventDefault();
        try {
            await api.post("/Auth/register", { email, password, displayName });
            alert("Registered! Please login.");
            navigate("/login");
        } catch (err) {
            console.error(err);
            alert(err?.response?.data ?? "Registration failed.");
        }
    }

    return (
        <div style={styles.center}>
            <div style={styles.card}>
                <h2 style={{ marginBottom: 16 }}>Create account</h2>
                <form onSubmit={handleRegister} style={{ width: "100%" }}>
                    <input
                        style={styles.input}
                        placeholder="Display name"
                        value={displayName}
                        onChange={(e) => setDisplayName(e.target.value)}
                        required
                    />
                    <input
                        style={styles.input}
                        placeholder="Email"
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                    <input
                        style={styles.input}
                        placeholder="Password"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                    <button style={styles.primaryBtn} type="submit">Register</button>
                </form>
                <div style={{ marginTop: 12 }}>
                    Already have an account? <Link to="/login">Login</Link>
                </div>
            </div>
        </div>
    );
}

const styles = {
    center: { minHeight: "100vh", display: "flex", alignItems: "center", justifyContent: "center", background: "#f5f6f8" },
    card: { width: 360, padding: 24, background: "#fff", borderRadius: 12, boxShadow: "0 6px 24px rgba(0,0,0,0.08)", display: "flex", flexDirection: "column", alignItems: "center" },
    input: { width: "100%", padding: "10px 12px", marginBottom: 12, borderRadius: 8, border: "1px solid #ddd", outline: "none" },
    primaryBtn: { width: "100%", padding: "10px 12px", borderRadius: 8, border: "none", background: "#007aff", color: "#fff", cursor: "pointer", fontWeight: 600 },
};
