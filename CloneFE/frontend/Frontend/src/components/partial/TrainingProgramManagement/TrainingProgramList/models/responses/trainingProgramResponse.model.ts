import { TrainingProgram } from "../TrainingProgram.model";

export interface TrainingProgramResponse {
    list: TrainingProgram[];
    totalPage: number;
    totalRecord: number;
  }