import { SyllabusUnit } from "./syllabusUnit.model";

export interface SyllabusDay {
    syllabusDayId: string;
    id: number;
    createBy?: string;
    createDate? : Date;
    isDeleted: boolean;
    modifiedBy?: string;
    modifiedDate?: Date;
    dayNo?: number;
    syllabusUnits: SyllabusUnit[];
}