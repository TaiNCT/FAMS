// @ts-nocheck
import style from "./style.module.scss";
import { useState, useEffect, useRef, createContext, createRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { UpdateScoreManagement } from "../UpdateScoreManagement";
import EditIcon from "@mui/icons-material/Edit";
import CheckIcon from "@mui/icons-material/Check";
import { LoadingButton } from "@mui/lab";
import Button from "@mui/material/Button";
import { Bounce, toast } from "react-toastify";

// @ts-ignore
const api_route: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`; // http://localhost:5271

// const test_id = "cd9fe541-1c6f-4e9e-a94b-1ff748186975";

function toDate(data: string): string {
  // Convert any datetime string into ISO format
  if (!data) return "";
  var curr = new Date(data);
  curr.setDate(curr.getDate() + 1);
  return curr.toISOString().substring(0, 10);
}

// context to share to child components
export const DataContext = createContext({});

function StudentManagement() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [student, setStudent] = useState({});
  const [isloading, setIsloading] = useState(false);
  const [ischange, setIsChange] = useState(0);

  // Allow <input /> to be editable
  const [allowEditScore, seteditscore] = useState(false);
  const [allowEditOther, seteditother] = useState(false);
  const [allowEditGeneral, seteditgeneral] = useState(false);

  // references to <input />
  const idref = useRef(null);
  const emailref = useRef(null);
  const phoneref = useRef(null);
  const nameref = useRef(null);
  const genderref = useRef(null);
  const dobref = useRef(null);
  const locationref = useRef(null);
  const permanentref = useRef(null);

  //
  const majorref = useRef(null);
  const recerref = useRef(null);
  const uniref = useRef(null);
  const gparef = useRef(null);
  const gradref = useRef(null);

  const score = useRef({
    HTML: null,
    CSS: null,
    "Quiz 3": null,
    "Quiz 4": null,
    "Quiz 5": null,
    "Quiz 6": null,
    "Practice 1": null,
    "Practice 2": null,
    "Practice 3": null,
    MOCK: null,
    Final: null,
    GPA: null,
  });

  const [buttonDisabled, setButtonDisabled] = useState(false);
  const validateScore = () => {
    let isInvalid = false;
    for (const key in score.current) {
      const value = score.current[key];
      if (value < 0 || value > 100) {
        isInvalid = true;
        break;
      }
    }
    setButtonDisabled(isInvalid);
  };
  // Update Database function
  const UpdateData = () => {
    const gender = genderref.current.value.trim();

    const data = {
      id: idref.current.value.trim(),
      name: nameref.current.value.trim(),
      gender: gender === "Others" ? 3 : gender === "Female" ? 2 : 1,
      dob: new Date(dobref.current.value.trim()).toISOString(),
      phone: phoneref.current.value.trim(),
      email: emailref.current.value.trim(),
      location: locationref.current.value.trim(),
      university: uniref.current.value.trim(),
      major: majorref.current.value.trim(),
      gpa: parseFloat(gparef.current.value.trim()),
      recer: recerref.current.value.trim(),
      gradtime: new Date(gradref.current.value.trim()).toISOString(),

      html: score.current.HTML,
      css: score.current.CSS,
      quiz3: score.current["Quiz 3"],
      quiz4: score.current["Quiz 4"],
      quiz5: score.current["Quiz 5"],
      quiz6: score.current["Quiz 6"],
      final: score.current.Final,
      practice1: score.current["Pratice 1"],
      practice2: score.current["Pratice 2"],
      practice3: score.current["Pratice 3"],
      mock: score.current.MOCK,
      gpa2: score.current.GPA,
      level: 1,
    };

    setIsloading(true);

    validateScore();

    // checking if any value is below 0 or above 100
    if (data.html < 1 || typeof data.html !== "number" || data.html > 100) {
      toast.error('Invalid "html" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.css < 1 || typeof data.css !== "number" || data.css > 100) {
      toast.error('Invalid "css" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.quiz3 < 1 || typeof data.quiz3 !== "number" || data.quiz3 > 100) {
      toast.error('Invalid "quiz3" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.quiz4 < 1 || typeof data.quiz4 !== "number" || data.quiz4 > 100) {
      toast.error('Invalid "quiz4" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.quiz5 < 1 || typeof data.quiz5 !== "number" || data.quiz5 > 100) {
      toast.error('Invalid "quiz5" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.quiz6 < 1 || typeof data.quiz6 !== "number" || data.quiz6 > 100) {
      toast.error('Invalid "quiz6" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.final < 1 || typeof data.final !== "number" || data.final > 100) {
      toast.error('Invalid "final" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (
      data.practice1 < 1 ||
      typeof data.practice1 !== "number" ||
      data.practice1 > 100
    ) {
      toast.error('Invalid "Practice 1" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (
      data.practice2 < 1 ||
      typeof data.practice2 !== "number" ||
      data.practice2 > 100
    ) {
      toast.error('Invalid "Practice 2" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (
      data.practice3 < 1 ||
      typeof data.practice3 !== "number" ||
      data.practice3 > 100
    ) {
      toast.error('Invalid "Practice 3" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.mock < 1 || typeof data.mock !== "number" || data.mock > 100) {
      toast.error('Invalid "MOCK" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.gpa2 < 1 || typeof data.gpa2 !== "number" || data.gpa2 > 100) {
      toast.error('Invalid "GPA" field. Should in the range of 1-100');
      setIsloading(false);
      return;
    }
    if (data.level < 1 || typeof data.level !== "number" || data.level > 100) {
      toast.error('Invalid "Level" field. Should in the range of 1-100');
      return;
    }

    if (buttonDisabled) {
      return;
    }
    axios
      .post(`${api_route}/api/ScoreUpdate/update/${id}`, data)
      .then((resp) => {
        toast.success(resp.data.msg, {
          position: "top-right",
          autoClose: 3000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
        setIsloading(false);
      })
      .catch((e) => {
        toast.error(
          "Unexpected error happened while trying to change the data.",
          {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
          }
        );
        setIsloading(false);
      });
  };

  useEffect(() => {
    axios
      .get(`${api_route}/api/students/${id}`)
      .then((resp) => {
        setStudent(resp.data);
      })
      .catch((e) => {
        console.error("Error fetching student details:", e);
        toast.error("Error fetching student details", {
          position: "top-right",
          autoClose: 3000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
      });
  }, [id]);

  // HTML elements
  return (
    <DataContext.Provider
      value={{
        data: student,
        scoreRef: score,
        student: student,
        ischange: ischange,
        setIsChange: setIsChange,
      }}
    >
      <div className={style.App}>
        <div className={style.container}>
          <h1 className={style.header}>Student details</h1>

          <div
            className={`${style.content} ${
              allowEditGeneral ? style.allowedit : ""
            }`}
          >
            <div className={style.subheader}>
              <h2>General</h2>
            </div>
            <div className={style["input-group"]}>
              <div>
                <div>
                  <label>ID </label>
                  <input ref={idref} defaultValue={student.id} />
                </div>
                <div>
                  <label>Name: </label>
                  <input ref={nameref} defaultValue={student.fullName} />
                </div>
                <div>
                  <label>Gender :</label>
                  <input
                    ref={genderref}
                    defaultValue={
                      student.gender === 1
                        ? "Male"
                        : student.gender === 2
                        ? "Female"
                        : "Others"
                    }
                  />
                </div>
                <div>
                  <label>Date of Birth:</label>
                  <input
                    ref={dobref}
                    type={allowEditGeneral ? "date" : ""}
                    defaultValue={
                      allowEditGeneral ? toDate(student.dob) : student.dob
                    }
                  />
                </div>
              </div>
              <div>
                <div>
                  <label>Phone :</label>
                  <input ref={phoneref} defaultValue={student.phone} />
                </div>
                <div>
                  <label>Email :</label>
                  <input ref={emailref} defaultValue={student.email} />
                </div>
                <div>
                  <label>Location:</label>
                  <input ref={locationref} defaultValue={student.location} />
                </div>
              </div>
            </div>
          </div>

          <div
            className={`${style.content} ${
              allowEditOther ? style.allowedit : ""
            }`}
          >
            <div className={style.subheader}>
              <h2>Other</h2>
            </div>

            <div className={style["input-group"]}>
              <div>
                <div>
                  <label>University:</label>
                  <input ref={uniref} defaultValue={student.university} />
                </div>
                <div>
                  <label>Major:</label>
                  <input ref={majorref} defaultValue={student.major} />
                </div>
                <div>
                  <label>RECer:</label>
                  <input ref={recerref} defaultValue={student.reCer} />
                </div>
              </div>
              <div>
                <div>
                  <label>GPA:</label>
                  <input ref={gparef} defaultValue={student.gpa} />
                </div>
                <div>
                  <label>Graduation time:</label>
                  <input
                    ref={gradref}
                    type={allowEditOther ? "date" : ""}
                    defaultValue={
                      allowEditOther
                        ? toDate(student.graduatedDate)
                        : student.graduatedDate
                    }
                  />
                </div>
              </div>
            </div>
          </div>

          <div className={style.content}>
            <div className={style.subheader}>
              <h2>Scores</h2>
              {!allowEditScore && (
                <EditIcon onClick={() => seteditscore(true)} />
              )}
              {allowEditScore && (
                <CheckIcon onClick={() => seteditscore(false)} />
              )}
            </div>
            <h3 className="text-3xl uppercase mb-3 divacking-wide">
              Fresher Develop Operation
            </h3>
            <p className="border-b border-black font-semibold mb-3">
              HCM22_FR_DevOps_01
            </p>

            <UpdateScoreManagement edit={allowEditScore} />
          </div>
          <div className={style.overwrite_stack}>
            <Button
              variant="outlined"
              color="error"
              onClick={() => navigate(-1)}
            >
              Cancel
            </Button>
            <LoadingButton
              loading={isloading}
              variant="contained"
              onClick={UpdateData}
            >
              Save
            </LoadingButton>
          </div>
        </div>
      </div>
    </DataContext.Provider>
  );
}

export { StudentManagement };
