import { UnitChapter } from "../../TrainingProgramDetail/models/unitChapter.model";


export interface TrainingMaterial 
{
    trainingMaterialId: string;
    id: string;
    createdBy: string;
    createdDate: Date;
    isDeleted: boolean;
    modifiedBy: string;
    modifiedDate: Date;
    fileName: string;
    isFile: boolean;
    name: string;
    url: string;
    unitChapter: UnitChapter
}
