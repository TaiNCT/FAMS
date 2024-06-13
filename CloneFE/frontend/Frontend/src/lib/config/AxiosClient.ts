import axios from "axios";
export const BASE_URL = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export const axiosClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-type": "application/json",
  },
});

axiosClient.interceptors.request.use(
  async (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
      config.headers.Accept = "application/json";
      config.headers["Content-Type"] = "application/json";
    }
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);