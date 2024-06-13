import { create } from "zustand";
import { SyllabusDay } from "../types";

type SyllabusHeader = {
  topicName: string;
  topicCode: string;
  version: string;
  setTopicName: (topicName: string) => void;
  setTopicCode: (topicCode: string) => void;
  setVersion: (version: string) => void;
}

type SyllabusGeneral = {
  level: string;
  attendeeNumber: number;
  technicalRequirement: string;
  courseObjective: string;
  setLevel: (level: string) => void;
  setAttendeeNumber: (attendeeNumber: number) => void;
  setTechnicalRequirement: (technicalRequirement: string) => void;
  setCourseObjective: (courseObjective: string) => void;
}

type SyllabusOther = {
  quiz: number;
  assignment: number;
  final: number;
  finalTheory: number;
  finalPractice: number;
  gpa: number;
  deliveryPrinciple: string;
  setQuiz: (quiz: number) => void;
  setAssignment: (assignment: number) => void;
  setFinal: (final: number) => void;
  setFinalTheory: (finalTheory: number) => void;
  setFinalPractice: (finalPractice: number) => void;
  setGpa: (gpa: number) => void;
  setDeliveryPrinciple: (deliveryPrinciple: string) => void;
}

export type SyllabusDaysState = {
  syllabusDays: SyllabusDay[];
  setSyllabusDays: (syllabusUnits: SyllabusDay[]) => void;
};

export const UseSyllabusOutline = create<SyllabusDaysState>((set) => ({
  syllabusDays: [],
  setSyllabusDays: (syllabusDays: SyllabusDay[]) => {
    const updatedSyllabusDays: SyllabusDay[] = syllabusDays.map((syllabusUnit, index) => {
      // code logic here
      return {
        dayNo: syllabusUnit.dayNo,
        syllabusUnits: syllabusUnit.syllabusUnits.map((unitChapter, index) => {
          return {
            name: unitChapter.name,
            unitNo: index + 1,
            duration: unitChapter.unitChapters.reduce((total, chapter) => total + chapter.duration, 0),
            unitChapters: unitChapter.unitChapters.map((chapter, index) => {
              if (chapter.isOnline === undefined) {
                chapter.isOnline = false;
              }
              return {
                chapterNo: index + 1,
                duration: chapter.duration,
                isOnline: chapter.isOnline,
                name: chapter.name,
                deliveryTypeId: chapter.deliveryTypeId,
                outputStandardId: chapter.outputStandardId,
                trainingMaterials: [
                  {
                    fileName: 'filename',
                    isFile: false,
                    name: 'filename',
                  }
                ],
              };
            }),
          };
        })
      };
    });
    set({ syllabusDays: updatedSyllabusDays });
  },
}));

export const UseSyllabusHeader = create<SyllabusHeader>((set) => ({
  topicCode: "",
  topicName: "",
  version: "",
  setTopicCode: (topicCode) => set({ topicCode }),
  setTopicName: (topicName) => set({ topicName }),
  setVersion: (version) => set({ version }),
}));

export const UseSyllabusGeneral = create<SyllabusGeneral>((set) => ({
  level: "",
  attendeeNumber: 0,
  technicalRequirement: "",
  courseObjective: "",
  setLevel: (level) => set({ level }),
  setAttendeeNumber: (attendeeNumber) => set({ attendeeNumber }),
  setTechnicalRequirement: (technicalRequirement) => set({ technicalRequirement }),
  setCourseObjective: (courseObjective) => set({ courseObjective }),
}));

export const UseSyllabusOther = create<SyllabusOther>((set) => ({
  quiz: 0,
  assignment: 0,
  final: 0,
  finalTheory: 0,
  finalPractice: 0,
  gpa: 0,
  deliveryPrinciple: "",
  setQuiz: (quiz) => set({ quiz }),
  setAssignment: (assignment) => set({ assignment }),
  setFinal: (final) => set({ final }),
  setFinalTheory: (finalTheory) => set({ finalTheory }),
  setFinalPractice: (finalPractice) => set({ finalPractice }),
  setGpa: (gpa) => set({ gpa }),
  setDeliveryPrinciple: (deliveryPrinciple) => set({ deliveryPrinciple }),
}));