import { UnitChapter } from "./unitChapter.model";

export interface SyllabusUnit {
    syllabusUnitId: string;
    id: number;
    createBy?: string;
    createDate? : Date;
    modifiedBy?: string;
    modifiedDate?: Date;
    duration?: number;
    name: string;
    unitNo: number;
    unitChapters: UnitChapter[]
}