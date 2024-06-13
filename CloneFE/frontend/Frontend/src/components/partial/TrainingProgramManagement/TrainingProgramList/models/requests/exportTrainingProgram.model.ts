export interface ExportTrainingProgramRequest {
  status: string | string[];
  createdBy: string | string[];
  programTimeToFrameTo: Date;
  programTimeToFrameFrom: Date;
  sort: string | string[];
}
