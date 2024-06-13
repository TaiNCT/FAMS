import { studentApi } from "../axios";
import { useQuery, UseQueryResult } from "react-query";
import { Response, StudentInclassResponse } from "../../model/StudentLamNS";
import { useSearchParams } from "react-router-dom";
import { AxiosError } from "axios";

export function useItems(
  param: string,
  pagination: {
    pageNumber: number;
    pageSize: number;
  }
): UseQueryResult<Response<StudentInclassResponse>, AxiosError> {
  const [search] = useSearchParams({
    classId: param,
    pageNumber: String(pagination.pageNumber),
    pageSize: String(pagination.pageSize),
  });

  return useQuery(
    ["GetStudentsInclass", search.toString()],
    () =>
      studentApi
        .get("GetStudentsInclass", {
          params: search,
        })
        .then((res) => res.data)
        .catch((error) => {}),
    {
      staleTime: 120000,
      retry: false,
    }
  );
}
