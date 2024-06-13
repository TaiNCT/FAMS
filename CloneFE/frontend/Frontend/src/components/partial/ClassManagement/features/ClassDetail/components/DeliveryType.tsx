import { ReactNode } from "react";
import LabIcon from "@/assets/icons/delivery-types-icons/LabIcon";
import { Popover } from "antd";
import LectureIcon from "@/assets/icons/delivery-types-icons/LectureIcon";
import ReviewIcon from "@/assets/icons/delivery-types-icons/ReviewIcon";
import QuizIcon from "@/assets/icons/delivery-types-icons/QuizIcon";
import ExamIcon from "@/assets/icons/delivery-types-icons/ExamIcon";
import WorkShopIcon from "@/assets/icons/delivery-types-icons/WorkShopIcon";

type DeliveryTypeProps = {
  icon: "lab" | "lecture" | "review" | "quiz" | "exam" | "workshop";
};
const DeliveryType = ({ icon }: DeliveryTypeProps) => {
  let IconEl: ReactNode;
  let text: string = "";
  switch (icon) {
    case "lab": {
      IconEl = <LabIcon />;
      text = "Asignment/lab";
      break;
    }
    case "lecture": {
      IconEl = <LectureIcon />;
      text = "Concept/lecture";
      break;
    }
    case "review": {
      IconEl = <ReviewIcon />;
      text = "Guide/review";
      break;
    }
    case "quiz": {
      IconEl = <QuizIcon />;
      text = "Test/quiz";
      break;
    }
    case "exam": {
      IconEl = <ExamIcon />;
      text = "Exam";
      break;
    }
    case "workshop": {
      IconEl = <WorkShopIcon />;
      text = "Seminar/workshop";
      break;
    }
  }
  return (
    <div>
      <Popover content={text} trigger="hover">
        <div>{IconEl}</div>
      </Popover>
    </div>
  );
};

export default DeliveryType;
