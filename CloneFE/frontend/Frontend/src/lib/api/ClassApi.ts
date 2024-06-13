import { BASE_URL, axiosClient } from "../config/AxiosClient";

const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };
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

// export const getAllClass = async () => {
//     try {
//         const { data } = await axiosClient.get('/api/Class/GetAllClass/1?pageSize=10');
//         return { error: null, data };
//     } catch (error) {
//         return handleApiError(error);
//     }
// }

export const getClassById = async (id: string) => {
  try {
    const { data } = await axiosClient.get(
      `${BASE_URL}/api/Class/ViewInfoClassDetail?classId=${id}`
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const getAllAttendeeType = async () => {
  try {
    const { data } = await axiosClient.get(
      `${BASE_URL}/api/AttendeeType/GetAttendeeTypeList`
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const updateClass = async (request) => {
  try {
    const { data } = await axiosClient.put(
      `${BASE_URL}/api/Class/${request.classId}`,
      request
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const addClassUser = async (userId, classId) => {
  try {
    const { data } = await axiosClient.post(`${BASE_URL}/api/Class/AddUser`, {
      userId: userId,
      classId: classId,
    });
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};
export const deleteClassUser = async (userId, classId) => {
  try {
    const { data } = await axiosClient.delete(
      `${BASE_URL}/api/Class/${classId}/DeleteUser?userId=${userId}`
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const updateClassFsu  = async (classId, fsu: any) => {
  try {
    const { data } = await axiosClient.put(
      `${BASE_URL}/api/Class/${classId}/fsu`, fsu
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
};

export const updateClassAttendeeType = async (classId, AttendeeType: any) => {
  try {
    const { data } = await axiosClient.put(
      `${BASE_URL}/api/Class/${classId}/attendeetype`, AttendeeType
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
}

export const updateClassTrainingProgram = async (classId, trainingProgram: any) => {
  try {
    const { data } = await axiosClient.put(
      `${BASE_URL}/api/Class/${classId}/trainingprogram`, trainingProgram.trainingProgramCode
    );
    return { error: null, data };
  } catch (error) {
    return handleApiError(error);
  }
}