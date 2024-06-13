// @ts-nocheck
import { Syllabus } from "./syllabus.model";

export interface TrainingProgram {
	id: number;
	trainingProgramCode: string;
	name: string;
	createdDate: Date;
	createdBy: string;
	updateDate: Date;
	updateBy: string;
	startTime: number;
	days: number;
	hours: number;
	status: string;
	syllabi: Syllabus[];
}
