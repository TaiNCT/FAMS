import React from "react";
import style from "../../../assert/css/AttendeeForm.module.scss";

export default function AttendeeForm({
  planned,
  setPlanned,
  actual,
  setActual,
  accepted,
  setAccepted,
  errorsValidate,
  setErrorsValidate,
})
{
  const [error, setError] = React.useState({
    planned: "",
    accepted: "",
    actual: "",
  });

  const handleChange = (event) =>
  {
    const { name, value } = event.target;
    if (value <= 0 && value != "")
    {
      setError((prevError) => ({
        ...prevError,
        [name]: "Giá trị phải là số dương",
      }));
      switch (name)
      {
        case "planned":
          setPlanned(value);
          break;
        case "accepted":
          setAccepted(value);
          break;
        case "actual":
          setActual(value);
          break;
        default:
          break;
      }
    } else if (value == "")
    {
      setError((prevError) => ({
        ...prevError,
        [name]: "",
      }));
      switch (name)
      {
        case "planned":
          setPlanned(value);
          break;
        case "accepted":
          setAccepted(value);
          break;
        case "actual":
          setActual(value);
          break;
        default:
          break;
      }
    } else
    {
      setError((prevError) => ({ ...prevError, [name]: "" }));
      switch (name)
      {
        case "planned":
          setPlanned(value);
          localStorage.setItem('planned', value);
          const newPlanettendence = {...errorsValidate};
          newPlanettendence.plannedAttendee = "";
          setErrorsValidate(newPlanettendence);
          break;
        case "accepted":
          setAccepted(value);
          localStorage.setItem('accepted', value);
          const newAcptAttendence = {...errorsValidate};
          newAcptAttendence.acceptedAttendee = "";
          setErrorsValidate(newAcptAttendence);
          break;
        case "actual":
          setActual(value);
          localStorage.setItem('actual', value);
          const newActualAttendence = {...errorsValidate};
          newActualAttendence.actualAttendee = "";
          setErrorsValidate(newActualAttendence);
          break;
        default:
          break;
      }
    }
  }

  React.useEffect(() =>
  {
    const planned = localStorage.getItem('planned');
    const accepted = localStorage.getItem('accepted');
    const actual = localStorage.getItem('actual');

    if (planned) setPlanned(planned);
    if (accepted) setAccepted(accepted);
    if (actual) setActual(actual);
  }, []);

  return (
    <div className={style.container}>
      <div className={style.content}>
        <div className={style.planned}>
          <div style={{ marginBottom: "14px" }}>Planned</div>
          {planned ? <input id="planned" name="planned" value={planned} onChange={handleChange} />
            : <input id="planned" name="planned" onChange={handleChange} />}
          {error.planned && (
            <div style={{ textAlign: "center" }}>{error.planned}</div>
          )}
          {errorsValidate.plannedAttendee && (
            <div style={{ textAlign: "center" }}>
              {errorsValidate.plannedAttendee}
            </div>
          )}
        </div>
        <div className={style.accepted}>
          <div style={{ marginBottom: "14px" }}>Accepted</div>
          {accepted ? <input id="accepted" name="accepted" value={accepted} onChange={handleChange} />
            : <input id="accepted" name="accepted" onChange={handleChange} />}
          {error.accepted && (
            <div style={{ textAlign: "center" }}>{error.accepted}</div>
          )}
          {errorsValidate.acceptedAttendee && (
            <div style={{ textAlign: "center" }}>
              {errorsValidate.acceptedAttendee}
            </div>
          )}
        </div>
        <div className={style.actual}>
          <div style={{ marginBottom: "14px" }}>Actual</div>
          {actual ? <input id="actual" name="actual" value={actual} onChange={handleChange} />
            : <input id="actual" name="actual" onChange={handleChange} />}
          {error.actual && (
            <div style={{ textAlign: "center" }}>{error.actual}</div>
          )}
          {errorsValidate.actualAttendee && (
            <div style={{ textAlign: "center" }}>
              {errorsValidate.actualAttendee}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
