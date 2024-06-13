export type UserViewProgram = {
  id: string;
  programName: string;
  createOn: string;
  createBy: string;
  duration: number;
  status: "Active" | "Inactive" | "Draft";
}