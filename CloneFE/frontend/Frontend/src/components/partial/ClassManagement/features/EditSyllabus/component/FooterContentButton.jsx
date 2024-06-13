import React from "react";
import MultiSyllabus from "./MultiSyllabus";
import { Button } from "antd";
import ModalSyllabus from "../component/ModalSyllabus";
import { CreateProgramSyllabus } from "../api/ListApi";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

export default function FooterContentButton({programPicked, onAdd, counter, setCounter, handleSaveSyllabus})
{
  const [open, setOpen] = React.useState(false);
  const [syllabusPicked, setSyllabusPicked] = React.useState("");
  const navigate = useNavigate();
  const handleClickOpen = () =>
  {
    setOpen(true);
  };
  
  const handleClickSelected = async () =>
  {
    const response = await CreateProgramSyllabus( programPicked?.trainingProgramCode, syllabusPicked?.syllabusId);
    const result = await response.json();
    if (response.ok)
    {
      toast.success("Create Successfully");
      setSyllabusPicked(null)
      setCounter(counter + 1);
      if(onAdd){
        onAdd();
      }
    } else
    {
      toast.error("Create fail");
    }
  };
  return (
    <>
      <div
        style={{
          display: "flex",
          alignItems: "center",
          gap: "10px",
          marginTop: "20px",
        }}
      >
        <Button
          style={{
            textTransform: "none",
            background: "#2d3748",
            borderRadius: "10px",
            color: "white",
            padding: "4px 10px 4px 10px",
            fontSize: "smaller",
            margin: "0px",
            width: "max-content",
          }}
          onClick={handleClickOpen}
        >
          Add Syllabus
        </Button>{" "}
        <span>or</span>
        <MultiSyllabus setSyllabusPicked={setSyllabusPicked} programPicked={programPicked} counter={counter}/>
        {syllabusPicked && <Button onClick={handleClickSelected}>Add</Button>}
      </div>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          marginTop: "30px",
          marginBottom: "10px",
        }}
      >
        <div>
          <Button style={{ background: "#2d3748", color: "white" }} onClick={() => navigate('/createclass')}>
            Back
          </Button>
        </div>
        <div>
          <Button
            style={{
              color: "red",
              border: "none",
            }}
          >
          </Button>
          <Button style={{ background: "#2d3748", color: "white" }} onClick={handleSaveSyllabus}>
            Save
          </Button>
        </div>
      </div>
      <ModalSyllabus open={open} setOpen={setOpen} programPicked={programPicked} onAdd={onAdd}/>
    </>
  );
}
