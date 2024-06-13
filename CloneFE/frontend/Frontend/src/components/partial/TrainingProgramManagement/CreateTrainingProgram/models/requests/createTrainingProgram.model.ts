export interface createTrainingProgram {
    createdBy?: string;
    createdDate?: Date;
    updatedBy?: string;
    updatedDate?: Date;
    days?: number;
    hours?: number;
    //startTime: number
    name: string;
    status: string;
    syllabiIDs: string[];
}
