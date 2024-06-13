export type StudentInfoDTO = {
    studentId: string;
    id: number;
    fullName: string;
    dob: string;
    gender: string;
    phone: string;
    email: string;
    majorId: string;
    graduatedDate: string;
    gpa: number;
    address: string;
    faaccount: string;
    type: number;
    status: string;
    joinedDate: string;
    area: string;
    recer: string;
    university: string;
}

// Define the interface for the response object
export type ApiResponse = {
    students: StudentInfoDTO[];
    totalCount: number;
}

export type Major = {
    id: string;
    majorId: string;
    name: string;
}
