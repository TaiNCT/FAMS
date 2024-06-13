export type User = {
  id: string;
  fullName: string;
  email: string;
  dateOfBirth: string;
  gender: string;
  type: "Super Admin" | "Admin" | "Trainer";
}