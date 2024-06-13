import { SyllabusDay } from "../../TrainingProgramDetail/models/syllabusDay.model";

export interface Syllabus {
  id: number;
  syllabusId: string;
  topicCode: string;
  topicName: string;
  version: string;
  createdBy: string;
  createdDate: string;
  modifiedBy: string;
  modifiedDate: string;
  attendeeNumber: number;
  level: string;
  technicalRequirement: string;
  courseObjective: string;
  deliveryPrinciple: string;
  days: number;
  hours: number;
  syllabusDays: SyllabusDay[]
  // output_standard?: Array<string> | "";
}
