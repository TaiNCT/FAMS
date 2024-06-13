import React, { useState } from "react";
import HeaderContent from "../component/HeaderContent";
import FooterEdit from "../layouts/FooterEdit";
import FooterContentButton from "../component/FooterContentButton";
import ListCardTwo from "../component/Card/ListCard";
import { useLocation, useNavigate } from "react-router-dom";
import Modal from "../component/Modal";
import {
  GetListOfSyllabusByTPCode,
  CreateClassDetail,
  CreateClassUser,
} from "../../CreateClass/api/ListApi";
import { Bounce, Slide, toast } from "react-toastify";
import ModalTrainer from "../component/ModalTrainer";
import { Button } from "antd";
import ModalWarning from "../component/ModalWarning";
import WaitingModal from "../../CreateClass/component/ModalWaiting";

export default function EditSyllabusPage() {
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = React.useState(false);
  const [open1, setOpen1] = React.useState(false);
  const [counter, setCounter] = React.useState(0);
  const [open, setOpen] = React.useState(false);
  const [open2, setOpen2] = React.useState(false);
  const [selectedSyllabus, setSelectedSyllabus] = React.useState("");
  const [syllabusList, setSyllabusList] = React.useState([]);
  const [classData, setClassData] = React.useState(() => {
    const storedClassData = localStorage.getItem("classData");
    return storedClassData ? JSON.parse(storedClassData) : {};
  });
  const warningShownRef = React.useRef(false);
  const [locationPicked, setLocationPicked] = React.useState("");
  const [trainerPicked, setTrainerPicked] = React.useState("");
  React.useEffect(() => {
    if (
      !warningShownRef.current &&
      (!classData.programPicked || !classData.programPicked.trainingProgramCode)
    ) {
      toast.warn("Please pick a Training Program", {
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
      warningShownRef.current = true;
      navigate("/createclass");
    }
  }, [navigate, classData.programPicked]);

  React.useEffect(() => {
    if (
      (!classData.programPicked || !classData.programPicked.length) &&
      !classData.className
    ) {
      toast.warn("Please create a Class", {
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
      navigate("/createclass");
    }
  }, []);

  const fetchData = React.useCallback(async () => {
    if (classData.programPicked?.trainingProgramCode) {
      const data = await GetListOfSyllabusByTPCode(
        classData.programPicked.trainingProgramCode
      );
      setSyllabusList(data);
    }
  }, []);

  React.useEffect(() => {
    fetchData(classData.programPicked);
  }, [fetchData]);

  if (!classData.programPicked) {
    return null;
  }

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

  const handleSaveSyllabus = async () => {
    if (!trainerPicked) {
      setOpen2(true);
      return;
    }
    const storedValues =
      JSON.parse(localStorage.getItem("selectedValues")) || {};
    if (classData.childState && storedValues) {
      locationPicked != null
        ? (classData.childState.locationId = locationPicked)
        : "Not Yet";
        classData.childState.classCode = classData.childState.className.substring(0,6) + "01"
      const response = await CreateClassDetail(classData.childState);
      // if (childState)
      const result = await response.json();
      setIsModalOpen(true);
      if (response.ok) {
        toast.success("Create successfully", {
          position: "top-right",
          autoClose: 4000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: false,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Slide,
        });
        if (storedValues.Admin != null) {
          const createClassUser = await CreateClassUser(
            result.data?.classId,
            storedValues.Admin,
            "Admin"
          );
          const createClassUser2 = await CreateClassUser(
            result.data?.classId,
            trainerPicked.userId,
            "Trainer"
          );
          if (createClassUser.ok && createClassUser2.ok) {
            navigate("/classList");
          }
        } else {
          navigate("/classList");
        }
      } else {
        setIsModalOpen(false);
        toast.error("Create fail", {
          position: "top-right",
          autoClose: 4000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: false,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Slide,
        });
      }
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <HeaderContent
        programPicked={classData.programPicked}
        className={classData.className}
      />
      <Button
        style={{ marginTop: "20px", background: "#2d3748", color: "white" }}
        onClick={() => setOpen1(true)}
      >
        Add Location & Trainer
      </Button>
      {syllabusList.map((syllabus) => (
        <div key={syllabus.id}>
          <ListCardTwo
            syllabusName={syllabus.topicName}
            syllabusStatus={syllabus.status}
            syllabusShortName={syllabus.topicCode}
            duration={{ days: syllabus.days, hours: syllabus.hours }}
            createdAt={new Date(syllabus.createdDate)}
            createdBy={syllabus.createdBy}
            onButtonClick={() => {
              setOpen(true);
              setSelectedSyllabus({
                name: syllabus.topicName,
                id: syllabus.syllabusId,
                trainingProgramCode:
                  classData.programPicked.trainingProgramCode,
              });
            }}
          />
        </div>
      ))}

      <FooterEdit>
        <FooterContentButton
          programPicked={classData.programPicked}
          onAdd={fetchData}
          counter={counter}
          setCounter={setCounter}
          handleSaveSyllabus={handleSaveSyllabus}
        />
      </FooterEdit>
      <WaitingModal open={isModalOpen} setOpen={setIsModalOpen} />
      <ModalWarning
        openSecond={open2}
        setOpenSecond={setOpen2}
        data={classData.childState}
      />
      <Modal
        open={open}
        setOpen={setOpen}
        syllabusData={selectedSyllabus}
        onDelete={fetchData}
        counter={counter}
        setCounter={setCounter}
      />
      <ModalTrainer
        open1={open1}
        setOpen1={setOpen1}
        setLocationPicked={setLocationPicked}
        locationPicked={locationPicked}
        setTrainerPicked={setTrainerPicked}
        trainerPicked={trainerPicked}
      />
    </div>
  );
}
