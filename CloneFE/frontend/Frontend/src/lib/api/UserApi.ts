import { BASE_URL, axiosClient } from "../config/AxiosClient";

export const handleApiError = (error: any) => {
  try {
    const errorMessage =
      error.response?.data || "An unexpected error occurred.";
    const data = null;
    return { error: errorMessage, data };
  } catch (err) {
    throw new Error("An unexpected error occurred.");
  }
};

export const getAllAdmin = async () => {
  try {
    const { data } = await axiosClient.get(`${BASE_URL}/api/ExpandClass/GetUserBasic`);
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

