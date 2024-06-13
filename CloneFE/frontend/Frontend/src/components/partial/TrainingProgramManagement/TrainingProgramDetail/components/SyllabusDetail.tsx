import { Box, Fade } from "@mui/material";
import style from "../style.module.scss";
import SyllabusDayBox from "./SyllabusDayBox";
import { SyllabusDay } from "../models/syllabusDay.model";

interface SyllabusDetailBoxProps {
  isClicked: boolean;
  checked: boolean;
  syllabusId: string;
  syllabusDays: SyllabusDay[];
}

const SyllabusDetailBox: React.FC<SyllabusDetailBoxProps> = (props) => {
  return (
    <Box className={style.box} display={!props.isClicked ? "none" : "flex"}>
      <Fade in={props.checked}>
        <div style={{width: "100%"}}>
          {props.syllabusDays.length > 0
            ? props.syllabusDays.map((sDay) => (
                <SyllabusDayBox syllabusDay={sDay} syllabusId={props.syllabusId} key={sDay.syllabusDayId}/>
              ))
            : <></>}
        </div>
      </Fade>
    </Box>
  );
};

export default SyllabusDetailBox;
