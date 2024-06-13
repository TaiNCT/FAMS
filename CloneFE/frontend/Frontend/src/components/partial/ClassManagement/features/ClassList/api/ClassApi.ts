import axios from "axios";
import { toast } from "react-toastify";
const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};

export const WEB_API_URL = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export const deleteClass = async (classId: string) => {
  try {
    const response = await axios.put(
      `${WEB_API_URL}/api/Class/${classId}/status`,
      {
        classStatus: "Deactive",
      }, config
    );
    toast.success("Class deleted successfully");
  } catch (error) {
    toast.error("Failed to delete class");
    if (error instanceof TypeError) {
      
    }
  }
};

export const DuplicateClass = async (classId: string) => {
  try {
    const url = `${WEB_API_URL}/api/Class/DuplicatedClass/${classId}`;
    const request = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
    };
    const response = await fetch(url, request);
    if (response) toast.success("Class duplicate successfully");
  } catch (error) {
    toast.error("Failed to duplicate class");
    if (error instanceof TypeError) {
    }
  }
};