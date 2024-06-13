import { SyllabusUnit } from "../models/syllabusUnit.model";
import style from "../style.module.scss";
import { Box, Fade } from "@mui/material";
import UnitChapterDetail from "./UnitChapterDetail";
import { MdOutlineArrowDropDownCircle } from "react-icons/md";
import { useState } from "react";

interface SyllabusUnitProps {
  syllabusId: string,
  dayNo: number;
  syllabusUnit: SyllabusUnit;
  unitBoxClicked: boolean;
  checked: boolean;
}

const SyllabusUnit: React.FC<SyllabusUnitProps> = (props) => {
  const [isClicked, setIsClicked] = useState<boolean>(false);

  let unitTotal: number = 0;
  props.syllabusUnit.unitChapters.map((x) => {
    if (x.duration !== undefined) unitTotal += x.duration;
  });

  const handleClick = () => {
    setIsClicked(!isClicked);
  };

  return (
    <Box
      className={style.unitBox}
      display={!props.unitBoxClicked ? "none" : "flex"}
    >
      <Fade in={props.checked}>
        <div className={style.unitBoxContent}>
          <span className={style.unitNo}>Unit {props.syllabusUnit.unitNo}</span>
          <div className={style.syllabusUnit}>
            <div className={style.syllabusUnitHeader} style={{ width: "100%" }}>
              <div>
                <b style={{ marginTop: "0.5rem" }}>{props.syllabusUnit.name}</b>
                <i style={{ marginTop: "0.5rem", marginBottom: "0.5rem" }}>
                  {unitTotal && Math.round(unitTotal / 60).toFixed(1)}hrs
                </i>
              </div>
              <div>
                <span
                  style={{
                    display: "flex",
                    justifyContent: "flex-end",
                    float: "right",
                    color: "rgb(16, 76, 167)",
                  }}
                >
                  <MdOutlineArrowDropDownCircle
                    size={28}
                    onClick={handleClick}
                  />
                </span>
              </div>
            </div>
            {isClicked && props.syllabusUnit.unitChapters.length
              ? props.syllabusUnit.unitChapters.map((unit) => (
                  <UnitChapterDetail
                    syllabusId={props.syllabusId}
                    unitChapter={unit}
                    unitNo={props.syllabusUnit.unitNo}
                    dayNo={props.dayNo}
                  />
                ))
              : ""}
          </div>
        </div>
      </Fade>
    </Box>
  );
};

export default SyllabusUnit;
