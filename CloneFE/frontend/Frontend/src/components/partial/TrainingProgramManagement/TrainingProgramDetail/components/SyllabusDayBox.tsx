import style from "../style.module.scss";
import { SyllabusDay } from "../models/syllabusDay.model";
import { useState } from "react";
import SyllabusUnit from "./SyllabusUnit";

interface SyllabusDayProps {
  syllabusId: string;
  syllabusDay: SyllabusDay;
}

const SyllabusDayBox: React.FC<SyllabusDayProps> = (props) => {
  const [unitBoxClicked, setUnitBoxClicked] = useState<boolean>(false);
  const [checked, setChecked] = useState<boolean>(false);

  const handleClick = () => {
    setChecked((prev) => !prev);
    setUnitBoxClicked(!unitBoxClicked);
  };

  return (
    <div
      className={style.syllabusDayBox}
      style={{ cursor: "pointer", width: "100%"}}
    >
      <button className={style.title} onClick={handleClick} style={{width:"100%"}}>
        <p>Day {props.syllabusDay.dayNo}</p>
      </button>
      {props.syllabusDay.syllabusUnits.length
        ? props.syllabusDay.syllabusUnits.map((unit) => (
            <SyllabusUnit
              unitBoxClicked={unitBoxClicked}
              checked={checked}
              syllabusUnit={unit}
              dayNo={props.syllabusDay.dayNo!}
              syllabusId={props.syllabusId}
              key={unit.syllabusUnitId}
            />
          ))
        : ""}
    </div>
  );
};

export default SyllabusDayBox;
