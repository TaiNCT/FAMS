interface BaseReponse {
    isSuccess: boolean;
    message: string;
    statusCode: number;
    title: string;
}

interface SyllabusResult<T> extends BaseReponse {
    result: {
        data: Array<T>;
        metadata: Metadata;
    };
}

interface Metadata {
    currentPage: number;
    totalPage: number;
    totalItems: number;
    pageSize: number;
    hasPrevious: boolean;
    hasNext: boolean;
}

export interface SyllabusList {
    id: string;
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
    status: string;
    technicalRequirement: string;
    courseObjective: string;
    deliveryPrinciple: string;
    days: number;
    hours: number;
    output_standard?: Array<string> | "";
}

export interface SyllabusDetailHeader {
    topicCode: string;
    topicName: string;
    version: string;
    modifiedBy: string;
    modifiedDate: string;
    status: string;
}

export interface SyllabusDetailGeneral {
    attendeeNumber?: number;
    level?: string;
    technicalRequirement?: string;
    courseObjective?: string;
    days?: number;
    hours?: number;
    outputStandardCode?: string;
}

export interface SyllabusDetailOutline {
    dayNo: number;
    syllabusUnits?: Array<SyllabusUnit>;
}

export interface UnitChapter {
    name: string;
    duration: number;
    isOnline: boolean;
    outputStandardId: string;
    deliveryTypeId: string;
    outputStandardName: string;
    deliveryTypeName: string;
}

export interface SyllabusUnit {
    unitNo: number;
    name: string;
    duration: number;
    unitChapters?: Array<UnitChapter>;
}

export interface SyllabusDetailOther {
    assignment: number;
    finalPractice: number;
    final: number;
    finalTheory: number;
    gpa: number;
    quiz: number;
    deliveryPrinciple: string;
}

export interface SingleSyllabusListResult extends BaseReponse {
    result: {
        data: SyllabusList;
        metadata: Metadata;
    };
}

export interface SyllabusDetailOutlineResult extends BaseReponse {
    result: {
        data: Array<SyllabusDetailOutline>;
        metadata: Metadata;
    };
}

export interface SyllabusDetailHeaderResult extends BaseReponse {
    result: {
        data: SyllabusDetailHeader;
        metadata: Metadata;
    };
}

export interface SyllabusDetailGeneralResult extends BaseReponse {
    result: {
        data: SyllabusDetailGeneral;
        metadata: Metadata;
    };
}

export interface SyllabusDetailOtherResult extends BaseReponse {
    result: {
        data: SyllabusDetailOther;
        metadata: Metadata;
    };
}

export interface SyllabusListResult<T> extends BaseReponse {
    result: {
        data: Array<T>;
        metadata: Metadata;
    };
}

export interface AssessmentScheme {
    assignment: number;
    finalPractice: number;
    final: number;
    finalTheory: number;
    gpa: number;
    quiz: number;
}

export interface DeliveryType {
    descriptions: string;
    icon?: string;
    name: string;
}

export interface OutputStandard {
    code: string;
    descriptions: string;
    name: string;
}

export interface TrainingMaterial {
    createdBy?: string;
    fileName: string;
    isFile: boolean;
    name: string;
    url?: string;
}

export interface CreateUnitChapter {
    createdBy?: string;
    chapterNo: number;
    duration?: number;
    isOnline: boolean;
    name: string;
    deliveryTypeId?: string;
    outputStandardId?: string;
    trainingMaterials: TrainingMaterial[];
}

export interface CreateSyllabusUnit {
    createdBy?: string;
    duration?: number;
    name: string;
    unitNo: number;
    unitChapters?: CreateUnitChapter[];
}

export interface SyllabusDay {
    createdBy?: string;
    dayNo?: number;
    syllabusUnits?: CreateSyllabusUnit[];
}

export interface CreateSyllabus {
    topicCode: string;
    topicName: string;
    version: string;
    createdBy: string;
    attendeeNumber: number;
    level: string;
    technicalRequirement: string;
    courseObjective: string;
    deliveryPrinciple?: string;
    days?: number;
    hours?: number;
    assessmentSchemes: AssessmentScheme[];
    syllabusDays: SyllabusDay[];
}

export interface UpdateSyllabus {
    syllabusId: string;
    topicCode: string;
    topicName: string;
    version: string;
    createdBy: string;
    attendeeNumber: number;
    level: string;
    technicalRequirement: string;
    courseObjective: string;
    deliveryPrinciple: string;
    days: number;
    hours: number;
    assessmentSchemes: AssessmentScheme[];
    syllabusDays?: SyllabusDay[];
}