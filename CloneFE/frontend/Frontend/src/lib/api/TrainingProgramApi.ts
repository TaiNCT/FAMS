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

export const getAllTrainingProgram = async () => {
  try {
    const { data } = await axiosClient.get(
      `${BASE_URL}/api/ExpandClass/GetTrainingProgramList`
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const getSyllabusByTrainingProgram = async (
  trainingProgramCode: string
) => {
  try {
    const { data } = await axiosClient.get(
      `${BASE_URL}/api/Class/trainingProgram/${trainingProgramCode}/syllabuses`
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};