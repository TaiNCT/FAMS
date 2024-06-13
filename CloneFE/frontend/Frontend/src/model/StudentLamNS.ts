export type StudentInfor = {
  studentId: string;
  id: number;
  fullName: string;
  dob: number;
  gender: string;
  phone: string;
  email: string;
  majorId: string;
  graduationDate: number;
  gpa: number;
  address: string;
  faAccount: string;
  type: number;
  status: string;
  joinedDate: number;
  area: string;
  recer: string;
  university: string;
  audit: number;
  mock: number;
};

export type Major = {
  majorId: string;
  id: string;
  name: string;
};

export type Class = {
  classId: string;
  className: string;
  createBy: string;
  createdDate: string;
  duration: string;
  endDate: string;
  location: string;
  programId: string;
  startDate: string;
  status: string;
  classCode: string;
  studentDTOClasses: string;
  updatedDate: string;
};

export type StudentClasses = {
  studentId: string;
  studentClassId: string | null;
  result: number;
  method: number;
  gpaLevel: number;
  finalScore: number;
  classId: number;
  class: Class | null;
  certificationStatus: number;
  certificationDate: string;
  attendingStatus: string;
};

export type Student = {
  studentInfor: StudentInfor;
  major: Major;
  studentClasses: StudentClasses[];
};

export type Students = {
  studentInfor: StudentInfor;
};

export type Response<T> = {
  result?: T;
  isSuccess: boolean;
  message: string;
};

export type StudentInclassResponse = {
  students: Student[];
  totalCount: number;
};
