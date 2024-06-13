export default interface UpdateMaterialRequest 
{
    syllabusId: string;
    trainingMaterialId: string;
    dayNo: number;
    unitNo: number;
    chapterNo: number;
    fileName: string;
    modifiedBy: string;
    file?: File | undefined;
}
