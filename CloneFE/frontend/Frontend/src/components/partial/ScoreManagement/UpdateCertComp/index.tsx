import React, { RefObject, useRef } from "react";
import style from "./style.module.scss";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
import { DropDown } from "../../../global/DropDown";
import { useMediaQuery } from "react-responsive";
import { DatePickerComp } from "../../../global/DatePickerComp";
import { toast, Slide, ToastOptions, ToastContainer } from "react-toastify";
import axios, { AxiosResponse, AxiosError } from "axios";
import dayjs, { Dayjs } from "dayjs";

// importing constant assets
import { cities } from "../../../../assets/vietnam_cities"; // List of all cities in Vietnam
import * as I from "./interfaces"; // Main interfaces for Typescript to statically type variables
import { useNavigate, useParams } from "react-router-dom";
import { OthersSection } from "./OthersSection";
import { Reserving } from "./Reserving";

// Import the slideshow
import { ListClassInfor } from "../../StudentManagement/ListClassInfor";
import { FormHelperText } from "@chakra-ui/react";
import { FormControl } from "react-bootstrap";
import { display } from "@mui/system";
import GlobalLoading from "../../../global/GlobalLoading";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;
// Constructing a full URL which points to the API in the backend
// PLease notice for this to succeed, create a local ".env" file with the
// format similar to that inside ".env.example"

// const backend_api: string = "http://localhost:5271";
// Only for testing

// const TESTING_ID: string = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50";
// Testing ID when the product is still in development mode, later on this will be useless

const UpdateCertComp: React.FC<I.IComponentProps> = () => {
  const { classid, student } = useParams();
  const IsPhone = useMediaQuery({ query: "(max-width: 640px)" });
  const [status, setStatus] = useState(null);

  // state "data" will basically store informatoin relating to the current user,
  // the overall information will be a JSON with the structure that matches the IData
  // interface to make sure it works properly without type conflicting
  const [data, setData] = useState<I.IData | null>(null);

  // "navigate" will be used to navigate backward one page
  const navigate = useNavigate();

  // Whenever there is an error in any TextField or Dayjs refs, simply add it to this array
  // and it will automatically the coresponding elements
  const [errorsTest, setErrorsTest] = useState<any>({});
  const [phoneNumber, setPhoneNumber] = useState("");

  // Resuable pop-ups config which will be applied to react-toastify pop-ups later
  const pop_config: ToastOptions = {
    position: IsPhone ? "bottom-center" : "top-right",
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
    transition: Slide,
  };

  // Create references to the components ( TextField, DropDown, etc ). Elements which contains
  // user-input data
  var refs: I.RefsType = {
    id: useRef<typeof TextField>(null),
    email: useRef<typeof TextField>(null),
    phone: useRef<typeof TextField>(null),
    name: useRef<typeof TextField>(null),

    // datetime references
    dob: useRef<Dayjs>(null),
    cert_date: useRef<Dayjs>(null),

    // drop-down references
    gender: useRef<string>(null),
    status: useRef<string>(null),
    cert_status: useRef<string>(null),
    permanent_res: useRef<string>(null),
  };

  //Change phone number to format
  const handleChangePhoneNumber = (event) => {
    setErrorsTest({ ...errorsTest, phone: "" });
    let inputPhoneNumber = event.target.value;
    // Remove all non-numeric characters
    inputPhoneNumber = inputPhoneNumber.replace(/\D/g, "");
    // Apply the custom input mask "xxxx xxx xxx x"
    const formattedPhoneNumber = inputPhoneNumber
      .replace(/(\d{4})(\d{1,3})?(\d{1,3})?(\d{1})?/, "$1 $2 $3 $4")
      .trim();
    setPhoneNumber(formattedPhoneNumber.substring(0, 12));
  };

  const handleChangeDOB = () => {
    setErrorsTest({ ...errorsTest, dob: "" });
  };

  // When the user clicks the "Save"
  const save_general = () => {
    // Collecting data and put it in a variable first for easy reference later on
    const req = {
      classid: classid,
      studentid: student,

      // @ts-ignore
      name: refs.name?.current?.value?.trim(),
      // @ts-ignore
      sid: refs.id?.current?.value?.trim(),
      gender: refs.gender.current,
      dob: refs.dob.current?.toISOString(),
      status: refs.status.current.replace(/\s/g, ""),
      // @ts-ignore
      phone: phoneNumber,
      // @ts-ignore
      email: refs.email?.current?.value?.trim(),
      permanentResidence: refs.permanent_res.current,
      certificateStatus: refs.cert_status.current === "Done",
      certificateDate: refs.cert_date.current?.toISOString(),
    };

    // Validation on the frontend first before proceeding

    //validation require
    let isValid = true;

    //Create object to save error message
    let errorObject = errorsTest;
    const phoneNumberToVLD = phoneNumber.replace(/\s/g, "");
    if (phoneNumberToVLD.length !== 10) {
      errorObject = {
        ...errorObject,
        phone: "Please enter a valid 10-digit phone number",
      };
      isValid = false;
    } else if (phoneNumberToVLD.charAt(0) !== "0") {
      errorObject = {
        ...errorObject,
        phone: "Phone number must start with 0",
      };

      isValid = false;
    }

    const old = dayjs().diff(dayjs(req.dob), "year");
    if (old < 18) {
      errorObject = {
        ...errorObject,
        dob: "Student must be at least 18 years old",
      };

      isValid = false;
    }
    if (!isValid) {
      setErrorsTest(errorObject);
      return;
    } else {
      setErrorsTest({});
    }

    // Actually sending an update HTTP request to the API endpoint
    // it will either show "success" or "error" popup using toastify
    toast.promise(
      new Promise((resolve, reject) => {
        // Updating student cert
        axios
          .post(`${backend_api}/api/cert/update`, req)
          .then((resp) => {
            resolve(resp.data.msg);
            setStatus(req.status);
          })
          .catch((e: AxiosError) => {
            switch (e?.request?.status) {
              case 400:
                reject(JSON.parse(e?.request?.responseText).title);
                break;
              case 404:
                reject("Unexpected error occurred");
                break;
              case 500:
                reject(e?.request?.responseText);
                break;
              default:
                break;
            }
          });
      }),
      {
        pending: "Promise is pending",
        success: {
          render({ data }) {
            return `${data}`;
          },
        },
        error: {
          render({ data }) {
            return `${data}`;
          },
        },
      },
      pop_config
    );
  };

  const fetchData = () => {
    axios
      .post(`${backend_api}/api/cert/get`, {
        classid: classid,
        studentid: student,
      })
      .then((resp: AxiosResponse<I.AxiosIData>) => {
        // Setting the title
        document.title = `FAMS - certificate of ${resp.data.name}`;

        setData({
          id: resp.data.sid,
          gender: resp.data.gender,
          email: resp.data.email,
          phone: resp.data.phone,
          name: resp.data.name,
          permanent_res: resp.data.address,
          dob: dayjs(resp.data.dob),
          status: resp.data.status,
          cert_status: resp.data.certificateStatus,
          cert_date: dayjs(resp.data.certificateDate),
          // "Others" section
          university: resp.data.university,
          gpa: resp.data.gpa,
          major: resp.data.major,
          recer: resp.data.recer,
          gradtime: dayjs(resp.data.gradtime),
        } as I.IData);
        setPhoneNumber(resp.data.phone);

        setStatus(resp.data.status);
      })
      .catch((e) => {
        if (e.response.status === 404) {
          setData({
            id: student,
            gender: "Others",
            email: null,
            phone: null,
            name: null,
            location: null,
            permanent_res: null,
            dob: null,
            status: null,
            cert_status: null,
            cert_date: null,
          } as I.IData);
        }
      });
  };

  // Getting data of a specific student
  useEffect(() => {
    // Run once in order to fetch data from the API as an initial request
    // then render into the Virtual DOM of the components from below
    fetchData();
  }, []);

  const isBetween = useRef(true);
  // Getting data of a specific student
  useEffect(() => {
    // Run once in order to fetch data from the API as an initial request
    // then render into the Virtual DOM of the components from below
    axios
      .get(`${backend_api}/api/Class/ViewInfoClassDetail?classId=${classid}`)
      .then((resp: AxiosResponse<I.AxiosClassDetail>) => {
        const currentDay = dayjs();
        isBetween.current =
          (currentDay.isAfter(dayjs(resp.data.data.startDate)) &&
            currentDay.isBefore(dayjs(resp.data.data.endDate))) ||
          currentDay.isSame(dayjs(resp.data.data.startDate)) ||
          currentDay.isSame(dayjs(resp.data.data.endDate));
      })

      .catch((e) => {
        // if (e.response.status === 404) {
        // }
      });
  }, []);

  // Check if today is between of start and end day of class for change status

  // Main components HTML and CSS
  return (
    <div className={`${style.main} ${IsPhone ? style.phone : ""}`}>
      {data !== null && (
        // Main component HTML Elements when the data has been successfully retrieved
        <>
          <div>
            <h1>Student Details</h1>
          </div>
          <div>
            {/* General section */}
            <section className={style.body}>
              <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light"
              />
              <h2>General</h2>
              <div className={style.col1}>
                <TextField
                  inputProps={{ "data-ucc": "id" }}
                  inputRef={refs.id}
                  defaultValue={data?.id}
                  disabled
                  label="ID"
                  variant="outlined"
                  helperText=" "
                />
                <TextField
                  inputProps={{ "data-ucc": "name" }}
                  data-cy="name"
                  disabled={true}
                  inputRef={refs.name}
                  defaultValue={data?.name}
                  label="Name"
                  variant="outlined"
                  helperText=" "
                />
                <div>
                  <DropDown
                    disabled={status !== "InClass"}
                    ref_={refs.gender}
                    options={["Female", "Male"]}
                    tag="Gender"
                    defaultValue={data?.gender as string}
                  />
                  <p style={{ opacity: 0 }}>.</p>
                </div>

                <div>
                  <DatePickerComp
                    disabled={status !== "InClass"}
                    ref_={refs.dob}
                    value={data?.dob as Dayjs}
                    tag="Date of birth"
                    resetError={handleChangeDOB}
                  />
                  <p
                    style={{
                      color: "red",
                      marginLeft: "14px",
                      marginTop: "3px",
                      fontWeight: 400,
                      fontSize: "0.75rem",
                      fontFamily: "Inter",
                    }}
                  >
                    {errorsTest.dob ? errorsTest.dob : "\u00A0"}
                  </p>
                </div>

                <DropDown
                  ref_={refs.status}
                  options={["In Class", "Drop Out", "Finish", "Reserve"]}
                  tag="Status"
                  defaultValue={
                    data?.status == "InClass"
                      ? "In Class"
                      : data?.status == "DropOut"
                      ? "Drop Out"
                      : data?.status
                  }
                  disabled={
                    !(
                      status == "InClass" ||
                      (status == "DropOut" && isBetween.current)
                    )
                  }
                />
              </div>
              <div className={style.col2}>
                <TextField
                  disabled={status !== "InClass"}
                  inputProps={{ "data-ucc": "phone" }}
                  error={errorsTest.phone}
                  inputRef={refs.phone}
                  defaultValue={data?.phone}
                  label="Phone"
                  variant="outlined"
                  value={phoneNumber ? phoneNumber : data?.phone}
                  helperText={errorsTest.phone ? errorsTest.phone : " "}
                  InputProps={{ maxRows: 12 }}
                  onChange={(e) => handleChangePhoneNumber(e)}
                />
                <TextField
                  inputProps={{ "data-ucc": "email" }}
                  disabled={true}
                  inputRef={refs.email}
                  defaultValue={data?.email}
                  label="Email"
                  variant="outlined"
                  helperText=" "
                />

                <div>
                  <DropDown
                    disabled={status !== "InClass"}
                    ref_={refs.permanent_res}
                    options={cities}
                    tag="Permanent residence"
                    defaultValue={data?.permanent_res as string}
                  />
                  <p style={{ opacity: 0 }}>.</p>
                </div>

                <div>
                  <DropDown
                    disabled={status !== "InClass"}
                    ref_={refs.cert_status}
                    options={["Not yet", "Done"]}
                    tag="Certification status"
                    defaultValue={data?.cert_status ? "Done" : "Not yet"}
                    // @ts-ignore
                    onCallbac
                    k={(e: string) => {
                      const ret = { ...data };
                      ret.cert_status = e === "Done";
                      setData(ret);
                    }}
                  />
                  <p style={{ opacity: 0 }}>.</p>
                </div>

                {data?.cert_status && (
                  <DatePickerComp
                    disabled={status !== "InClass"}
                    value={data?.cert_date as Dayjs}
                    ref_={refs.cert_date}
                    tag="Certification date"
                  />
                )}
              </div>
              <div className={style.bottom}>
                <section>
                  {/* Navigate backward one page when the user clicks on "Cancel" */}
                  <Button
                    onClick={() => navigate(-1)}
                    variant="text"
                    color="error"
                  >
                    Cancel
                  </Button>
                  {/* Update the data in the database when the user clicks on "Save" */}
                  <Button
                    disabled={
                      status !== "InClass" &&
                      !(status == "DropOut" && isBetween.current)
                    }
                    onClick={save_general}
                    variant="contained"
                  >
                    Save
                  </Button>
                </section>
              </div>
            </section>

            {/* "Others" section */}
            <OthersSection
              status={status}
              classid={classid}
              id={student}
              data={data}
            />

            {/* Class information section */}
            <section className={`${style.body} ${style.classinfo}`}>
              <ListClassInfor studentId={student} />
              {/* <div className={style.bottom}>
                <section>
                  <Button
                    onClick={() => navigate(-1)}
                    variant="text"
                    color="error"
                  >
                    Cancel
                  </Button>
                  <Button variant="contained">Save</Button>
                </section>
              </div> */}
            </section>

            {/* Add Reserving section */}
            <Reserving />
            <GlobalLoading isLoading={data === null} />
          </div>
        </>
      )}
    </div>
  );
};
export { UpdateCertComp };
