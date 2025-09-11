import axios from "axios";

// Create Axios instance with backend base URL
const api = axios.create({
    baseURL: "http://localhost:5054/api", // your backend API
});

// Automatically add JWT to each request if available
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token"); // JWT saved after login
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
