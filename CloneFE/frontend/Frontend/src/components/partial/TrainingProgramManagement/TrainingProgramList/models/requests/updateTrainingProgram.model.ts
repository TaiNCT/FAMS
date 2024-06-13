export interface updateTrainingProgram {
    trainingProgramCode: string | undefined,
    id: number | undefined,
    name: string,
    days: number,
    updatedBy: string,
    status: string,
    syllabiIDs: string[]
}