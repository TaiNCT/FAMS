import { TrainingMaterial } from "../../TrainingProgramMaterial/models/trainingMaterial.model";
import { DeliveryType } from "./deliveryType.model";
import { OutputStandard } from "./outputStandard.model";

export interface UnitChapter{
    id: number,
    createBy?: string;
    createDate?: Date;
    isDeleted: boolean;
    modifiedBy?: string;
    modifiedDate?: Date;
    chapterNo: number;
    duration?: number;
    isOnline: boolean;
    name: string;
    deliveryType: DeliveryType;
    outputStandard: OutputStandard;
    trainingMaterials: TrainingMaterial[];
}