import { Button } from "@mui/material";
import style from "../../../assert/css/BodyInputClass.module.scss";
import logoDropDown from "../../../../../../assets/LogoStManagement/caretdown.png";
import BasicDateRangePicker from "./BasicDateRangePicker";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import { Select, Space } from "antd";
import GeneralForm from "./GeneralForm";
import React from "react";
import AttendeeForm from "./AttendeeForm";
import FormatTime from "../../../Utils/FormatTime";
import FormatDate from "../../../Utils/FormatDate";
import dayjs from "dayjs";
import { AttendeeTypeList } from "../../ClassList/api/ListApi";
import moment from "moment/moment";
function BodyInputClass({
  programPicked,
  generalOpen,
  setGeneralOpen,
  attendee,
  setAttendee,
  handleChildStateChange,
  className,
  Admin,
  setAdmin,
  setCalendarOpen,
  calendarOpen,
  errorsValidate,
  setErrorsValidate,
})
{
  const [timeFrom, setTimeFrom] = React.useState();
  const [timeTo, setTimeTo] = React.useState();
  const [fsu, setFsu] = React.useState();
  const [planned, setPlanned] = React.useState(Number());
  const [accepted, setAccepted] = React.useState(Number());
  const [actual, setActual] = React.useState(Number());
  const [listAttendee, setListAttendee] = React.useState();
  const [startDate, setStartDate] = React.useState();
  const [endDate, setEndDate] = React.useState();
  const [selectAttende, setSelectAttende] = React.useState("");
  const handleChange = (value) => {
    setSelectAttende(value);
    localStorage.setItem('attendeeValue', value);
  };
  const randomDuration = Math.floor(Math.random() * 21) + 10;
  React.useEffect(() => {
    handleChildStateChange({
      acceptedAttendee: Number(accepted),
      actualAttendee: Number(actual),
      plannedAttendee: Number(planned),
      startTime: FormatTime(dayjs(timeFrom)),
      endTime: FormatTime(dayjs(timeTo)),
      createdDate: FormatDate(new Date()),
      startDate: startDate,
      endDate: endDate,
      fsuId: fsu,
      className: className,
      attendeeLevelId: selectAttende,
      trainingProgramCode: programPicked.trainingProgramCode,
      duration: randomDuration,
    });
  }, [
    planned,
    actual,
    accepted,
    timeFrom,
    timeTo,
    startDate,
    endDate,
    className,
    fsu,
    selectAttende,
    programPicked,
  ]);

  React.useEffect(() => {
    const fetchApiData = async () => {
      const listAttendees = await AttendeeTypeList();
      try {
        if (listAttendees) {
          setListAttendee(listAttendees);
        }
      } catch (err) {
      }
    };
    fetchApiData();
  }, []);

  React.useEffect(() =>
  {
    const storedAttendee = localStorage.getItem("attendeeValue");
    if (storedAttendee)
    {
      setSelectAttende(storedAttendee);
    }
  }, [])
  const attendeeList = listAttendee?.map((item) => ({
    label: item.attendeeTypeName,
    value: item.attendeeTypeId,
  }));
 
  return (
    <div>
      <div>
        <div className={style.container}>
          <div style={{ width: "28%", height: "max-content" }}>
            <div
              onClick={
                Object.keys(programPicked).length > 0
                  ? () => setGeneralOpen(!generalOpen)
                  : null
              }
              disabled={Object.keys(programPicked).length <= 0}
              className={
                Object.keys(programPicked).length > 0
                  ? style.active
                  : style.button
              }
            >
              <div className={style.content}>
                <div style={{ display: "flex", gap: "10px" }}>
                  <CalendarTodayIcon />
                  <div>General</div>
                </div>
                <div style={{ display: "flex" }}>
                  <img
                    style={{
                      transform: generalOpen ? "rotate(0deg)" : "rotate(90deg)",
                    }}
                    width={20}
                    src={logoDropDown}
                  />
                </div>
              </div>
            </div>
            {generalOpen && (
              <GeneralForm
                timeFrom={timeFrom}
                timeTo={timeTo}
                setTimeFrom={setTimeFrom}
                setTimeTo={setTimeTo}
                setAdmin={setAdmin}
                Admin={Admin}
                setFsu={setFsu}
                errorsValidate={errorsValidate}
                FSU={fsu}
                setErrorsValidate={setErrorsValidate}
              />
            )}
          </div>
          <div style={{ width: "68%" }}>
            <div
              disabled={Object.keys(programPicked).length <= 0}
              onClick={
                Object.keys(programPicked).length > 0
                  ? () => setCalendarOpen(!calendarOpen)
                  : null
              }
              className={
                Object.keys(programPicked).length > 0
                  ? style.active
                  : style.button
              }
            >
              <div className={style.content}>
                <div
                  style={{ display: "flex", gap: "10px", alignItems: "center" }}
                >
                  <CalendarTodayIcon />
                  <div>Time frame</div>
                  <div>
                    <BasicDateRangePicker
                      setStartDate={setStartDate}
                      setEndDate={setEndDate}
                      programPicked={programPicked}
                      setErrorsValidate={setErrorsValidate}
                    />
                  </div>
                {(errorsValidate.startDate || errorsValidate.endDate) && (<div style={{color: "red"}}>Please Input Start Date & End Date</div>)}
                </div>
                <div style={{ display: "flex" }}>
                  <img
                    style={{
                      transform: calendarOpen
                        ? "rotate(0deg)"
                        : "rotate(90deg)",
                    }}
                    width={20}
                    src={logoDropDown}
                  />
                </div>
              </div>
            </div>
          </div>
          <div style={{ width: "28%" }}>
            <div
              onClick={
                Object.keys(programPicked).length > 0
                  ? () => setAttendee(!attendee)
                  : null
              }
              className={
                Object.keys(programPicked).length > 0
                  ? style.active
                  : style.button
              }
            >
              <div className={style.content}>
                <div style={{ display: "flex", gap: "10px" }}>
                  <StarBorderIcon />
                  <div>Attendee</div>
                </div>
                <div>
                  <Space wrap>
                    <Select
                      disabled={Object.keys(programPicked).length <= 0}
                      placeholder="select"
                      style={{
                        width: 140,
                      }}
                      onChange={handleChange}
                      options={attendeeList}
                      status={errorsValidate.attendeeLevelId ? "error" : null}
                      value={selectAttende}
                    />
                  </Space>
                </div>
                <div style={{ display: "flex" }}>
                  <img
                    style={{
                      transform: attendee ? "rotate(0deg)" : "rotate(90deg)",
                    }}
                    width={20}
                    src={logoDropDown}
                  />
                </div>
              </div>
            </div>
            {attendee && (
              <AttendeeForm
                planned={planned}
                setPlanned={setPlanned}
                actual={actual}
                setActual={setActual}
                accepted={accepted}
                setAccepted={setAccepted}
                errorsValidate={errorsValidate}
                setErrorsValidate={setErrorsValidate}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default BodyInputClass;
