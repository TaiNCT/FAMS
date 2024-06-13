export type Syllabus = {
  syllabusId: number;
  title: string;
  status: string;
  version: string;
  createDate: Date;
  updateDate: Date;
  updateBy: string;
  intructorId: number;
  instructorName: string;
};

//General Tab
export type GeneralTabData = {
  level: string;
  attendance_number: number;
  output_standard: string[];
  technical_requirements: string[];
};

export type Lession = {
  id: number;
  title: string;
  tag: string;
  duration: number;
  status: "Online" | "Offline";
  delivery_type: "exam" | "lab" | "lecture" | "quiz" | "review";
};

export type Unit = {
  index: number;
  title: string;
  duration: number;
  lessions: Lession[];
};

export type SyllabusDay = {
  index: number;
  units: Unit[];
};

//Outline Tab
export type OutlineTabCircularData = {
  delivery_type: "exam" | "lab" | "lecture" | "quiz" | "review";
  count: number;
};

//Other Tab
export type OtherTabData = {
  quiz: number;
  assignment: number;
  final: number;
  gpa: number;
};

export type TrainingDeliveryPrinciple = {
  icon:
    | "checkbox"
    | "checkbox-outline-blank"
    | "filter-center-focus"
    | "grade"
    | "info"
    | "radio-button-checked"
    | "radio-button-unchecked"
    | "report-problem"
    | "supplier"
    | "verified-user"
    | "warning-logo";
  title: string;
  description: string;
};
