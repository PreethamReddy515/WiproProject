// src/api/axios.js
import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5054/api",
});

// attach token
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

// handle 401 -> login
api.interceptors.response.use(
    (res) => res,
    (err) => {
        if (err?.response?.status === 401) {
            alert("Session expired. Please login again.");
            localStorage.removeItem("token");
            localStorage.removeItem("currentUser");
            window.location.href = "/login";
        }
        return Promise.reject(err);
    }
);

export default api;
