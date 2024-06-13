import axios, { AxiosInstance, AxiosResponse } from "axios";
import { WEB_API_URL } from "../../config";
import axiosAuth from "../../api/axiosAuth";
import { toast } from "react-toastify";

const BASE_URL = WEB_API_URL + "/api";

const httpRequest: AxiosInstance = axios.create({
  // baseURL: process.env.REACT_APP_BASE_URL_LOCAL,
  baseURL: BASE_URL,
  // timeout: 1000,
});

export const get = async <T>(path: string, options = {}): Promise<T> => {
  const response: AxiosResponse<T> = await axiosAuth.get(path, options);
  return response.data;
};

export const patch = async <T>(path: string, options = {}): Promise<T> => {
  const response: AxiosResponse<T> = await axiosAuth.patch(path, options);
  return response.data;
};

export const put = async <T>(path: string, options = {}): Promise<T> => {
  const response: AxiosResponse<T> = await axiosAuth.put(path, options);
  return response.data;
};

export const remove = async <T>(path: string, options = {}): Promise<T> => {
  const response: AxiosResponse<T> = await axiosAuth.delete(path, options);
  return response.data;
};

export const post = async <T>(path: string, options = {}): Promise<T> => {
  const response: AxiosResponse<T> = await axiosAuth.post(path, options);
  return response.data;
};

export const getWithBlob = async (path: string, fileName: string): Promise<void> => {
  const link = document.createElement("a");
  link.target = "_blank";
  link.download = fileName;

  await axiosAuth
    .get(path, { responseType: "blob" })
    .then((res) => {
      link.href = URL.createObjectURL(
        new Blob([res.data], { type: "application/zip" })
      );
      link.click();
    })
    .catch((error) => {
      if (error instanceof Error) {
        toast.error("Not found any materials");
      } else {
      }
    });
};

export default httpRequest;
