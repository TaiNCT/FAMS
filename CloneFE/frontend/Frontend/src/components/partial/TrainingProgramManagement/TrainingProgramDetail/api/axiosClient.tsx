import axios from "axios";

// @ts-ignore
const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

// const BASE_URL = process.env.APP_API_KEY;
const BASE_URL = backend_api;
const api = axios.create({ baseURL: BASE_URL });
export default api;
