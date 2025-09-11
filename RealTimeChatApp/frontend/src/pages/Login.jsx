// src/pages/Login.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault();
        try {
            const res = await api.post("/Auth/login", { email, password });
            localStorage.setItem("token", res.data.token);
            localStorage.setItem("currentUser", JSON.stringify(res.data.user));
            navigate("/chat");
        } catch (err) {
            console.error(err);
            alert(err?.response?.data ?? "Login failed.");
        }
    }

    return (
        <div style={{ minHeight: "100vh", display: "flex", justifyContent: "center", alignItems: "center" }}>
            <form onSubmit={handleLogin} style={{ display: "flex", flexDirection: "column", width: 300 }}>
                <input
                    placeholder="Email"
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    style={{ marginBottom: 10, padding: 8 }}
                />
                <input
                    placeholder="Password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    style={{ marginBottom: 10, padding: 8 }}
                />
                <button type="submit" style={{ padding: 10, background: "#007aff", color: "#fff", border: "none" }}>
                    Login
                </button>
            </form>
        </div>
    );
}
