import * as React from "react";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Input } from "antd";
import { useFormik } from "formik";
import Form from "react-bootstrap/Form";
import { CreateProgramSyllabus, CreateSyllabus } from "../api/ListApi";
import { Slide, toast } from "react-toastify";
import { Button } from "react-bootstrap";
import { validationSyllabus } from "./ValidationSyllabus";
import generateRandomString from "../../../Utils/RandomString";
export default function ModalSyllabus({ open, setOpen, programPicked, onAdd })
{
  const handleClose = () =>
  {
    setOpen(false);
  };
  const submitForm = async (values) =>
  {
    const syllabus = {
      topicCode: generateRandomString(),
      topicName: values.topicName,
      version: "v1.0",
      technicalRequirement: generateRandomString(),
      courseObjective: generateRandomString(),
      deliveryPrinciple: generateRandomString(),
      days: Number(values.days),
      hours: Number(values.hours),
      status: "Active",
    };

    const response = await CreateSyllabus(syllabus);
    const result = await response.json();
    if (response.ok)
    {
      //toast.success("Create Successfully");
      const createTrainingSyllabus = await CreateProgramSyllabus(
        programPicked.trainingProgramCode,
        result.data?.syllabusId
      );
      if (createTrainingSyllabus.ok)
      {
        toast.success("Create Successfully", {
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
        handleClose();
        if (onAdd)
        {
          onAdd();
        }
      }
    } else
    {
      toast.error("Create Failed", {
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
    }
  };
  const formik = useFormik({
    initialValues: {
      topicName: "",
      days: "",
      hours: "",
    },
    onSubmit: (values) =>
    {
      submitForm(values);
    },
    validationSchema: validationSyllabus,
  });
  return (
    <React.Fragment>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
        fullWidth={true}
      >
        <DialogTitle
          id="alert-dialog-title"
          style={{
            textAlign: "center",
            background: "#2d3748",
          }}
        >
          <div style={{ display: "flex", justifyContent: "center" }}>
            <div style={{ color: "white" }}>New Syllabus</div>
          </div>
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <Form onSubmit={formik.handleSubmit}>
              <div
                style={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "space-between",
                  paddingLeft: "20px",
                  paddingRight: "20px",
                  marginTop: "25px",
                }}
              >
                <label>Syllabus Name</label>
                <div>
                  <Input
                    style={{ width: "300px", height: "40px" }}
                    placeholder="Syllabus Name"
                    name="topicName"
                    value={formik.values.topicName}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    status={
                      formik.errors.topicName &&
                      formik.touched.topicName &&
                      "error"
                    }
                  />
                  <Form.Control.Feedback
                    style={{
                      color: "red",
                    }}
                    type="invalid"
                  >
                    {formik.errors.topicName}
                  </Form.Control.Feedback>
                </div>
              </div>

              <div
                style={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "space-between",
                  paddingLeft: "20px",
                  paddingRight: "20px",
                  marginTop: "30px",
                }}
              >
                <label>Day</label>
                <div>
                  <Input
                    style={{ width: "300px", height: "40px" }}
                    placeholder="Day"
                    name="days"
                    value={formik.values.days}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    status={
                      formik.errors.days && formik.touched.days && "error"
                    }
                  />
                  <Form.Control.Feedback
                    style={{
                      color: "red",
                    }}
                    type="invalid"
                  >
                    {formik.errors.days}
                  </Form.Control.Feedback>
                </div>
              </div>
              <div
                style={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "space-between",
                  paddingLeft: "20px",
                  paddingRight: "20px",
                  marginTop: "30px",
                }}
              >
                <label>Hours</label>
                <div>
                  <Input
                    style={{ width: "300px", height: "40px" }}
                    placeholder="Hours"
                    name="hours"
                    value={formik.values.hours}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    status={
                      formik.errors.hours && formik.touched.hours && "error"
                    }
                  />
                  <Form.Control.Feedback
                    style={{
                      color: "red",
                    }}
                    type="invalid"
                  >
                    {formik.errors.hours}
                  </Form.Control.Feedback>
                </div>
              </div>

              <DialogActions
                style={{ justifyContent: "center", marginTop: "20px" }}
              >
                <Button
                  style={{
                    textTransform: "none",
                    color: "red",
                    border: "none",
                    textDecoration: "underline",
                  }}
                  onClick={handleClose}
                >
                  Cancel
                </Button>
                <Button
                  style={{
                    textTransform: "none",
                    color: "white",
                    background: "#2d3748",
                    borderRadius: "12px",
                    padding: "6px 30px 6px 30px",
                    border: "none",
                  }}
                  type="submit"
                  autoFocus
                >
                  Create
                </Button>
              </DialogActions>
            </Form>
          </DialogContentText>
        </DialogContent>
      </Dialog>
    </React.Fragment>
  );
}
