import React from "react";
import FormatDate from "../../../Utils/FormatDateInFigma";
import ModeEditOutlineOutlinedIcon from "@mui/icons-material/ModeEditOutlineOutlined";
function AfterPickProgram({
  programPicked,
  setProgramPicked,
  setGeneralOpen,
  setAttendee,
  setCalendarOpen,
  setSyllabusList,
})
{
  return (
    <div>
      <div>
        <p
          style={{
            letterSpacing: "3px",
            fontSize: "x-large",
            marginBottom: "7px",
          }}
        >
          <strong
            style={{
              fontWeight: "500",
              display: "flex",
              alignItems: "center",
              gap: "14px",
            }}
          >
            {programPicked.name}{" "}
            <span
              style={{ marginBottom: "5px", cursor: "pointer" }}
              onClick={() =>
              {
                setProgramPicked([]);
                setGeneralOpen(false);
                setAttendee(false);
                setCalendarOpen(false);
                setSyllabusList("");
                localStorage.removeItem('programPicked');
              }}
            >
              <ModeEditOutlineOutlinedIcon />
            </span>
          </strong>
        </p>
        <span style={{ fontSize: "smaller" }}>
          {programPicked.days} days ({programPicked.hours} hours) | Modified on{" "}
          {FormatDate(programPicked.createdDate)} by {programPicked.userId}
        </span>
      </div>
    </div >
  );
}
export default AfterPickProgram;
