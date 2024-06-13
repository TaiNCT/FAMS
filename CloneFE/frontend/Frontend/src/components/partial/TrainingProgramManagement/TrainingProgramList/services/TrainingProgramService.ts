import { memo } from "react";
import { toast } from "react-toastify";
import axiosMultipartForm from "../../api/axiosMultipartForm";
import { TrainingProgram } from "../models/TrainingProgram.model";
import { ApiResponse } from "../models/responses/apiResponse.model";
import { ErrorResponse } from "../models/responses/errorResponse.model";
import { TrainingProgramResponse } from "../models/responses/trainingProgramResponse.model";
import * as httpRequest from "../utils/httpRequest";

export const paginationList = async (
  currentPage: number,
  limit: number
): Promise<TrainingProgramResponse> => {
  const res = await httpRequest.get("trainingprograms", {
    params: {
      _page: currentPage,
      _perPage: limit,
    },
  });
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse.data;
};

export const sorting = async (
  sort: string,
  currentPage: number,
  limit: number
): Promise<TrainingProgramResponse> => {
  const res = await httpRequest.get("trainingprograms/sorting", {
    params: {
      sort: sort,
      _page: currentPage,
      _perPage: limit,
    },
  });
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse.data;
};

export const searching = async (
  value: string,
  currentPage: number,
  limit: number,
  sortColumn: string
): Promise<TrainingProgramResponse | ErrorResponse> => {
  try {
    const res = await httpRequest.get("trainingprograms/search", {
      params: {
        searchValue: value,
        _page: currentPage,
        _perPage: limit,
        sort: sortColumn,
      },
    });
    const apiResponse = res as ApiResponse<TrainingProgramResponse>;
    return apiResponse.data;
  } catch (error: any) {
    if (error.response && error.response.status === 404) {
      return {
        totalPage: 1,
        list: null,
      };
    }
    throw error;
  }
};

export const filtering = async (
  filterValues: any,
  currentPage: number,
  limit: number,
  sort: string
): Promise<TrainingProgramResponse | ErrorResponse> => {
  try {
    const { status, ...params } = filterValues;

    if (Array.isArray(status)) {
      status.forEach((value, index) => {
        params[`Status[${index}]`] = value;
      });
    }
    
    params._page = currentPage;
    params._perPage = limit;
    params.sort = sort;

    const queryString = new URLSearchParams(params).toString();

    const res = await httpRequest.get(`trainingprograms/filter?${queryString}`);

    const apiResponse = res as ApiResponse<TrainingProgramResponse>;
    return apiResponse.data;
  } catch (error: any) {
    if (error.response && error.response.status === 404) {
      return {
        totalPage: 1,
        list: null,
      };
    }
    throw error;
  }
};

export const getAuthors = async (): Promise<string[]> => {
  const res = await httpRequest.get("trainingprograms/authors");
  const authorsResponse = res as ApiResponse<string[]>;
  return authorsResponse.data;
};

export const updateStatus = async (
  trainingProgramCode: string
): Promise<ApiResponse<TrainingProgramResponse>> => {
  const res = await httpRequest.patch(
    `trainingprograms/${trainingProgramCode}`
  );
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse;
};

export const deleteTrainingProgram = async (
  trainingProgramCode: string
): Promise<ApiResponse<TrainingProgramResponse>> => {
  const res = await httpRequest.remove(
    `trainingprograms/delete/${trainingProgramCode}`
  );
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse;
};

export const duplicateTrainingProgram = async (
  trainingProgramCode: string,
  createdBy: string
): Promise<ApiResponse<TrainingProgramResponse>> => {
  // const res = await httpRequest.post(`trainingprograms/duplicate/${trainingProgramCode}/${createdBy}`);
  const res = await httpRequest.post("trainingprograms/duplicate", {
    Code: trainingProgramCode,
    CreatedBy: createdBy,
  });
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse;
};

export const updateTrainingProgram = async (
  trainingProgram: object | undefined
): Promise<ApiResponse<TrainingProgramResponse>> => {
  const res = await httpRequest.put("trainingprograms/update", trainingProgram);
  const apiResponse = res as ApiResponse<TrainingProgramResponse>;
  return apiResponse;
};

export const getTrainingProgramSyllabus = async (
  trainingProgramCode: string
): Promise<TrainingProgram> => {
  const res = await httpRequest.get(
    `trainingprograms/syllabus/${trainingProgramCode}`
  );
  const apiResponse = res as ApiResponse<TrainingProgram>;
  return apiResponse.data;
};

export const getTrainingProgramDetail = async (
  trainingProgramCode: string
): Promise<TrainingProgram> => {
  const res = await httpRequest.get(`trainingprograms/${trainingProgramCode}`);
  const apiResponse = res as ApiResponse<TrainingProgram>;
  return apiResponse.data;
};

export const uploadCSVFile = async (
  formData: FormData
): Promise<ApiResponse<Object>> => {
  try {
    const res = await axiosMultipartForm.post(
      "trainingprograms/uploadFile",
      formData
    );
    const apiResponse = res.data as ApiResponse<Object>;
    return apiResponse;
  } catch (error: any) {
    if (
      error.response &&
      (error.response.status === 409 || error.response.status === 400)
    ) {
      return {
        message: error.response.data.message,
        statusCode: error.response.status,
        errors: error.response.data.errors,
        data: {},
      };
    }
    throw error;
  }
};

export const downloadMaterial = async (
  trainingProgramCode: string,
  trainingProgramName: string
): Promise<void> => {
  try {
    await httpRequest.getWithBlob(
      `/trainingprograms/${trainingProgramCode}/materials`,
      `${trainingProgramName}-materials.zip`
    );
  } catch (error) {
    if (error instanceof Error) {
    } else {
    }
  }
};
