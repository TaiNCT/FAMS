import dayjs, { Dayjs } from "dayjs";
import { RefObject } from "react";
import { TextField } from "@mui/material";

export interface IComponentProps {}

// Define an interface to strongly type the "data" state
export interface IData {
  id: string;
  name: string | null;
  gender: string | null;
  dob: Dayjs | null;
  status: string | null;
  phone: string | null;
  email: string | null;
  permanent_res: string | null;
  cert_status: boolean | null; // ? how many status are there
  cert_date: Dayjs | null;
}

export interface AxiosIData {
  id: string | null;
  name: string | null;
  sid: string | null;
  gender: string | null;
  dob: string | null;
  status: string | null;
  phone: string | null;
  email: string | null;
  address: string | null;
  certificateStatus: boolean | null; // ? how many status are there
  certificateDate: string | null;

  // "Others" section
  university: string | null;
  gpa: number | null;
  major: string | null;
  recer: string | null;
  gradtime: Dayjs | null;
}

export interface AxiosClassDetail {
  data: {
    startDate: string | null;
    endDate: string | null;
  };
}

export interface RefsType {
  id: RefObject<typeof TextField> | null;
  email: RefObject<typeof TextField> | null;
  phone: RefObject<typeof TextField> | null;
  name: RefObject<typeof TextField> | null;
  dob: RefObject<Dayjs>;
  cert_date: RefObject<Dayjs>;
  gender: RefObject<string>;
  status: RefObject<string>;
  location: RefObject<string>;
  cert_status: RefObject<string>;
  permanent_res: RefObject<string>;
}

export interface IClass {
  startDate: Dayjs | null;
  endDate: Dayjs | null;
}
