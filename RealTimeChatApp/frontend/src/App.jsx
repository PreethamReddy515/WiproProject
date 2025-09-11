// src/App.jsx
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Register from "./pages/Register";
import Login from "./pages/Login";
import ChatPage from "./pages/ChatPage";

function RequireAuth({ children }) {
    const token = localStorage.getItem("token");
    if (!token) return <Navigate to="/login" replace />;
    return children;
}

export default function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Navigate to="/chat" replace />} />
                <Route path="/register" element={<Register />} />
                <Route path="/login" element={<Login />} />
                <Route
                    path="/chat"
                    element={
                        <RequireAuth>
                            <ChatPage />
                        </RequireAuth>
                    }
                />
                <Route path="*" element={<Navigate to="/chat" replace />} />
            </Routes>
        </Router>
    );
}
