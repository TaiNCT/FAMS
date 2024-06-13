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
import { toast } from "react-toastify";
import { Button } from "react-bootstrap";
import { validationSyllabus } from "./ValidationSyllabus";
import generateRandomString from "../../../Utils/RandomString";
import MultiTrainer from "./MultiTrainer";
import { LocationList } from "../../ClassList/api/ListApi";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
export default function ModalTrainer({
  open1,
  setOpen1,
  setLocationPicked,
  locationPicked,
  setTrainerPicked,
  trainerPicked
}) {
  const [locationList, setLocationList] = React.useState([]);
  React.useEffect(() => {
    const fetchApiData = async () => {
      const listLocation = await LocationList();
      setLocationList(listLocation);
    };
    fetchApiData();
  }, []);
  const handleClose = () => {
    setOpen1(false);
  };
  const submitForm = async (values) => {
  };
  const formik = useFormik({
    initialValues: {
      topicName: "",
      days: "",
      hours: "",
    },
    onSubmit: (values) => {
      submitForm(values);
    },
    // validationSchema: validationSyllabus,
  });
  const handleChange = (event) => {
    setLocationPicked(event.target.value);
  };
  return (
    <React.Fragment>
      <Dialog
        open={open1}
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
                <label>Select Trainer</label>
                <div>
                  <MultiTrainer setTrainerPicked={setTrainerPicked} trainerPicked={trainerPicked} />
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
                <label>Location</label>
                <div>
                  {/* <Input
                  style={{ width: "300px", height: "40px" }}
                  placeholder="Day"
                  name="days"
                  value={formik.values.days}
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  status={formik.errors.days && formik.touched.days && "error"}
                /> */}
                  <FormControl sx={{ m: 1, minWidth: 300 }} size="small">
                  <InputLabel id="demo-select-small-label">Location</InputLabel>
                    <Select
                      labelId="demo-select-small-label"
                      id="demo-select-small"
                      value={locationPicked}
                      label="Age"
                      onChange={handleChange}
                    >
                      {locationList.map((item) => (
                        <MenuItem value={item.locationId}>{item.name}</MenuItem>
                      ))}
                    </Select>
                  </FormControl>
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
              <DialogActions
                style={{ justifyContent: "center", marginTop: "20px" }}
              >
                <Button
                  style={{
                    textTransform: "none",
                    color: "green",
                    border: "none",
                    textDecoration: "underline",
                  }}
                  onClick={handleClose}
                >
                  Close And Go Save
                </Button>
              </DialogActions>
            </Form>
          </DialogContentText>
        </DialogContent>
      </Dialog>
    </React.Fragment>
  );
}
