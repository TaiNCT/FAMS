import ClassInput from "../component/ClassInput";
import React from "react";
import HeaderClass from "../component/HeaderClass";
import ButtonCreate from "../../../../ClassManagement/components/Form-Control/ButtonCreate";
import FormClass from "../component/FormClass";
import HeadClassPage from "../layouts/HeadClassPage";
import HeaderInputClass from "../component/HeaderInputClass";
import BodyClassPage from "../layouts/BodyClassPage";
import BodyInputClass from "../component/BodyInputClass";
import SelectItem from "../component/SelectItem";
import TrainingProgram from "../component/TrainingProgram";
import FooterCreateClass from "../component/FooterCreateClass";
import SearchProgram from "../component/MultiSelectProgram";
import AfterPickProgram from "../component/AfterPickProgram";
import { Button } from "@mui/material";
import FooterClassPage from "../layouts/FooterClassPage";
import ListCard from "../component/Card/Card";
import {
  CreateClassDetail,
  CreateClassUser,
  GetListOfSyllabusByTPCode,
} from "../api/ListApi";
import { Slide, toast } from "react-toastify";
import { useLocation, useNavigate } from "react-router-dom";
import WaitingModal from "../component/ModalWaiting";
import GlobalLoading from "../../../../../global/GlobalLoading";

function CreateClass() {
  const [isLoading, setIsLoading] = React.useState(true);
  const [isModalOpen, setIsModalOpen] = React.useState(false);
  const [msgClassName, setMsgClassName] = React.useState("");
  const navigate = useNavigate();
  const [errorsValidate, setErrorsValidate] = React.useState({});
  const [className, setClassName] = React.useState("");
  const [error, setError] = React.useState(false);
  const [formSubmitted, setFormSubmitted] = React.useState(false);
  const [selectItem, setSelectItem] = React.useState("Training Program");
  const [programPicked, setProgramPicked] = React.useState([]);
  const [generalOpen, setGeneralOpen] = React.useState(false);
  const [attendee, setAttendee] = React.useState(false);
  const [syllabusList, setSyllabusList] = React.useState([]);
  const [admin, setAdmin] = React.useState();
  const [calendarOpen, setCalendarOpen] = React.useState(false);
  const userName = localStorage.getItem("username");
  const [childState, setChildState] = React.useState({
    createdDate: "2024-04-01",
    classStatus: "Planning",
    classCode: className.substring(0,6) + "01",
    createdBy: userName != null ? userName : "Admin",
    startDate: "",
    endDate: "",
    startTime: "",
    endTime: "",
    acceptedAttendee: Number(""),
    actualAttendee: Number(""),
    className: "C# Fundamentals",
    locationId: "",
    plannedAttendee: Number(""),
    totalDays: "10",
    totalHours: "20",
    attendeeLevelId: "",
    fsuId: "",
  });
  const handleChange = (e) => {
    setClassName(e.target.value);
  };
  const handleCreate = () => {
    if (className === "") {
      setError(true);
    } else if (/[^a-zA-Z\s]/.test(className)) {
      setMsgClassName("Please input a validate name");
    } else {
      setError(false);
      setFormSubmitted(true);
      setMsgClassName(null);
      // Store the input value in localStorage
      localStorage.setItem("className", className);
    }
  };
  React.useEffect(() => {
    const fetchData = async () => {
      if (programPicked && programPicked.trainingProgramCode) {
        const data = await GetListOfSyllabusByTPCode(
          programPicked.trainingProgramCode
        );

        setSyllabusList(data);
      }
    };
    fetchData();
  }, [programPicked]);

  const handleChildStateChange = (newState) => {
    setChildState((prevState) => ({ ...prevState, ...newState }));
  };

  const handleCreateClass = async () => {
    let errors = {};
    let formValidate = true;

    if (childState.startTime > childState.endTime) {
      errors.timeFrom = "Thời gian bắt đầu phải trước thời gian kết thúc";
      formValidate = false;
    } else {
      errors.timeFrom = "";
    }
    if (!childState?.attendeeLevelId) {
      errors.attendeeLevelId = "Is required field";
      formValidate = false;
    } else {
      errors.attendeeLevelId = "";
    }
    if (!admin) {
      errors.admin = "Is required field";
      formValidate = false;
    } else {
      errors.admin = "";
    }
    if (!childState.fsuId || childState.fsuId == "") {
      errors.fsuId = "Is required field";
      formValidate = false;
    } else {
      errors.fsuId = "";
    }
    if (!childState.plannedAttendee || childState.plannedAttendee == 0) {
      errors.plannedAttendee = "Is required field";
      formValidate = false;
    } else {
      errors.plannedAttendee = "";
    }
    if (!childState.acceptedAttendee || childState.acceptedAttendee == 0) {
      errors.acceptedAttendee = "Is required field";
      formValidate = false;
    } else {
      errors.acceptedAttendee = "";
    }
    if (!childState.actualAttendee || childState.actualAttendee == 0) {
      errors.actualAttendee = "Is required field";
      formValidate = false;
    } else {
      errors.actualAttendee = "";
    }
    if (programPicked.length == 0 || !programPicked) {
      errors.programPicked = "Is required field";
      formValidate = false;
    } else {
      errors.programPicked = "";
    }
    if (!childState.startDate || childState.startDate == null) {
      errors.startDate = "Is required field";
      formValidate = false;
    } else {
      errors.startDate = "";
    }
    if (!childState.endDate || childState.endDate == null) {
      errors.endDate = "Is required field";
      formValidate = false;
    } else {
      errors.endDate = "";
    }
    setErrorsValidate(errors);
    if (formValidate) {
      setIsModalOpen(true);
      const response = await CreateClassDetail(childState);
      const result = await response.json();
      if (response.ok) {
        toast.success("Create successfully", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: false,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Slide,
        });
        if (admin != null) {
          const createClassUser = await CreateClassUser(
            result.data?.classId,
            admin,
            "Admin"
          );
          if (createClassUser.ok) {
            navigate("/classList");
          }
        } else {
          navigate("/classList");
        }
      } else {
        setIsModalOpen(false);
        toast.error("Create fail");
      }
    } else {
      return;
    }
  };

  React.useEffect(() => {
    const storedProgramPicked = localStorage.getItem("programPicked");
    if (storedProgramPicked) {
      setProgramPicked(JSON.parse(storedProgramPicked));
    }

    const storedClassName = localStorage.getItem("className");
    if (storedClassName) {
      setClassName(storedClassName);
      setFormSubmitted(true);
    }
  }, []);

  const handleClear = () => {
    setClassName("");
    setProgramPicked([]);
    setFormSubmitted(false);
    setSyllabusList([]);
    setAdmin([]);

    const keysToRemove = [
      "className",
      "programPicked",
      "selectedValues",
      "formValues",
      "planned",
      "accepted",
      "actual",
      "attendeeValue",
      "classData",
    ];

    keysToRemove.forEach((key) => {
      localStorage.removeItem(key);
    });
  };

  React.useEffect(() => {
    return () => {
      const currentlocation = location.pathname;
      const routesToKeep = ["/createclass", "/createclass/editClassSyllabus"];

      if (!routesToKeep.includes(currentlocation)) {
        const keysToRemove = [
          "className",
          "programPicked",
          "selectedValues",
          "formValues",
          "planned",
          "accepted",
          "actual",
          "attendeeValue",
          "classData",
        ];

        keysToRemove.forEach((key) => {
          localStorage.removeItem(key);
        });
      }
    };
  }, [navigate]);
    React.useEffect(() => {
      const timer = setTimeout(() => {
        setIsLoading(false);
      }, 300);
  
      return () => clearTimeout(timer);
    }, []);
  return (
    <>
    <GlobalLoading isLoading={(isLoading )}/>
    <section>
      <div>
        {!formSubmitted && (
          <>
            <HeaderClass />
            <FormClass>
              <ClassInput
                onchange={handleChange}
                error={error}
                msgClassName={msgClassName}
              />
              <ButtonCreate name={"Create"} onClick={handleCreate} />
            </FormClass>
          </>
        )}
        {formSubmitted && (
          <div style={{ marginTop: "1px" }}>
            <HeadClassPage>
              <HeaderInputClass
                programPicked={programPicked}
                className={className}
              />
            </HeadClassPage>
            <BodyClassPage>
              <BodyInputClass
                handleChildStateChange={handleChildStateChange}
                generalOpen={generalOpen}
                setGeneralOpen={setGeneralOpen}
                attendee={attendee}
                setAttendee={setAttendee}
                programPicked={programPicked}
                className={className}
                setAdmin={setAdmin}
                Admin={admin}
                setCalendarOpen={setCalendarOpen}
                calendarOpen={calendarOpen}
                errorsValidate={errorsValidate}
                setErrorsValidate={setErrorsValidate}
              />
              <SelectItem
                setSelectItem={setSelectItem}
                selectItem={selectItem}
              />
              {selectItem == "Training Program" &&
                Object.keys(programPicked).length > 0 && (
                  <TrainingProgram>
                    <AfterPickProgram
                      setProgramPicked={setProgramPicked}
                      programPicked={programPicked}
                      setGeneralOpen={setGeneralOpen}
                      setAttendee={setAttendee}
                      setCalendarOpen={setCalendarOpen}
                      setSyllabusList={setSyllabusList}
                    />
                  </TrainingProgram>
                )}
              {selectItem == "Training Program" &&
                programPicked.length <= 0 && (
                  <TrainingProgram>
                    <div>Training Program name</div>
                    <SearchProgram setProgramPicked={setProgramPicked} />
                    {errorsValidate.programPicked != "" && (
                      <span style={{ color: "red" }}>
                        {errorsValidate.programPicked}
                      </span>
                    )}
                  </TrainingProgram>
                )}
              {selectItem == "Training Program" &&
                syllabusList.length > 0 &&
                syllabusList.map((syllabus, index) => (
                  <div className="flex flex-col gap-5" key={index}>
                    <ListCard
                      syllabusName={syllabus.topicName}
                      syllabusStatus={syllabus.status}
                      syllabusShortName={syllabus.topicCode}
                      duration={{ days: syllabus.days, hours: syllabus.hours }}
                      createdAt={new Date(syllabus.createdDate)}
                      createdBy={syllabus.createdBy}
                    />
                  </div>
                ))}
              <FooterCreateClass />
            </BodyClassPage>
            <FooterClassPage>
              <Button
                sx={{ color: "red", textTransform: "none" }}
                onClick={handleClear}
              >
                Cancel
              </Button>
              <Button
                onClick={handleCreateClass}
                sx={{
                  backgroundColor: "#474747",
                  color: "white",
                  textTransform: "none",
                }}
              >
                Save as draft
              </Button>
              <Button
                sx={{
                  backgroundColor: "#2d3748",
                  color: "white",
                  textTransform: "none",
                }}
                onClick={() => {
                  localStorage.setItem(
                    "classData",
                    JSON.stringify({ programPicked, className, childState })
                  );
                  navigate("/createclass/editClassSyllabus");
                }}
              >
                Next
              </Button>
            </FooterClassPage>
            <WaitingModal open={isModalOpen} setOpen={setIsModalOpen} />
          </div>
        )}
      </div>
    </section>
    </>
  );
}
export default CreateClass;
