import style from "../style.module.scss";
import { TiDeleteOutline } from "react-icons/ti";
import formatDateToYYYYMMDD from "../utils/dateFormat";
import { FaArrowRightLong  } from "react-icons/fa6";
import { useState } from "react";
import SyllabusDetailBox from "../../TrainingProgramDetail/components/SyllabusDetail";
import { Syllabus } from "../models/syllabus.model";

interface SyllabusProps {
  syllabus: Syllabus;
  handleDelete: (id: number) => void;
  isDetailPage?: boolean | null;
}

const SyllabusDetail: React.FC<SyllabusProps> = (props) => {
  const [checked, setChecked] = useState<boolean>(false);
  const [isClicked, setIsClicked] = useState<boolean>(false);

  const handleClick = () => {
    setChecked((prev) => !prev);
    setIsClicked(!isClicked);
  };

  return (
    <>
      <div
        className={style.syllabus}
        style={props.isDetailPage ? { width: "92.6%", cursor: "pointer" } : {}}
        onClick={handleClick}
      >
        <div>
          <p
            style={{
              display: "flex",
              justifyContent: "flex-start",
              alignItems: "center",
            }}
          >
            <span>{props.syllabus.topicName}</span>
            <span>Active</span>
          </p>
          <p>
            <span>{props.syllabus.topicCode}</span>
            <span>
              {props.syllabus.days} days {props.syllabus.hours} hours
            </span>
            <span>
              {formatDateToYYYYMMDD(props.syllabus.modifiedDate)} by{" "}
              {props.syllabus.modifiedBy}
            </span>
          </p>
        </div>
        <div className={style.btnDelete}>
          {!props.isDetailPage ? (
            <TiDeleteOutline
              size={33}
              cursor={"pointer"}
              onClick={() => props.handleDelete(props.syllabus.id)}
            />
          ) : (
            <FaArrowRightLong 
              size={32}
              cursor={"pointer"}  
              style={{
                backgroundColor: "#111e2e",
                color: "white",
                width: "3rem",
                padding: "6px 12px",
                borderRadius: "8px",
              }}
            />
          )}
        </div>
      </div>
      {props.isDetailPage && (
        <SyllabusDetailBox
          syllabusId={props.syllabus.syllabusId}
          isClicked={isClicked}
          checked={checked}
          syllabusDays={props.syllabus.syllabusDays}
        />
      )}
    </>
  );
};

export default SyllabusDetail;
