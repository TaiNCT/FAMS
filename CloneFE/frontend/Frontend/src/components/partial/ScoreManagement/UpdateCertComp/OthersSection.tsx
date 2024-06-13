import style from "./style.module.scss";
import axios, { AxiosError } from "axios";
import { DropDown } from "../../../global/DropDown";
import { DatePickerComp } from "../../../global/DatePickerComp";
import { TextField } from "@mui/material";
import { universities } from "../../../../assets/vietnam_uni";
import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import { useRef } from "react";
import { Dayjs } from "dayjs";
import * as I from "./interfaces"; // Main interfaces for Typescript to statically type variables
import { Slide, toast, ToastOptions } from "react-toastify";
import { useMediaQuery } from "react-responsive";
import { useNavigate } from "react-router-dom";
import { isValid } from "date-fns";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function OthersSection({ classid, id, data, status }) {
  const [errors, setErrors] = useState<any>({});
  const [majors, setMajor] = useState<Array<string>>([]);
  const IsPhone = useMediaQuery({ query: "(max-width: 640px)" });
  const navigate = useNavigate();

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

  // References to the components
  var refs = {
    recer: useRef<typeof TextField>(null),
    gpa: useRef<typeof TextField>(null),
    university: useRef<string>(null),
    major: useRef<string>(null),
    gradtime: useRef<Dayjs>(null),
  };

  // Getting a list of marjos
  useEffect(() => {
    axios
      .get(`${backend_api}/api/cert/get/major`)
      .then((resp) => {
        setMajor(resp.data.map((e) => e.name));
      })
      .catch((e) => {
        toast.error(e.message);
      });
  }, []);

  // When the user clicks on "Save", the following function will execute
  const save_other_info = () => {
    // @ts-ignore
    const req = {
      classid: classid,
      studentid: id,
      university: refs.university.current,
      // @ts-ignore
      gpa: parseFloat(refs.gpa?.current?.value?.trim()),
      major: refs.major.current,
      // @ts-ignore
      recer: refs.recer?.current?.value?.trim(),
      gradtime: refs.gradtime.current?.toISOString(),
    };

    //Validation fields
    let isVaid = true;

    //create error message object
    let errorObject = errors;

    //validate gpa
    if (req.gpa < 0 || req.gpa > 100) {
      errorObject = {
        ...errorObject,
        GPA: "GPA value should be from 0 to 100",
      };
      isVaid = false;
    } else if (isNaN(req.gpa)) {
      errorObject = { ...errorObject, GPA: "GPA Is Require" };
      isVaid = false;
    }

    //Validate RECER
    const NoSpecialChar = /^[A-Za-z0-9 ]+$/;
    if (!NoSpecialChar.test(req.recer)) {
      errorObject = {
        ...errorObject,
        recer: "Field is not contain Special Character",
      };
      isVaid = false;
    } else if (req.recer.length <= 0) {
      errorObject = { ...errorObject, recer: "Field is require" };
      isVaid = false;
    }

    if (!isVaid) {
      setErrors(errorObject);
      return;
    } else {
      setErrors({});
    }
    // Actually sending an update HTTP request to the API endpoint
    // it will either show "success" or "error" popup using toastify
    toast.promise(
      new Promise((resolve, reject) => {
        // Updating student cert ( Others section )
        axios
          .post(`${backend_api}/api/cert/update/other`, req)
          .then((resp) => {
            resolve(resp.data.msg);
          })
          .catch((e: AxiosError) => {
            reject(e.request.responseText);
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

  return (
    <section className={style.body}>
      <h2>Others</h2>
      <div className={style.col1}>
        <div>
          <DropDown
            defaultValue={data.university}
            disabled={status !== "InClass"}
            ref_={refs.university}
            tag="University"
            options={universities}
          />
          <p style={{ opacity: "0" }}>.</p>
        </div>

        <div>
          <DropDown
            defaultValue={data.major}
            disabled={status !== "InClass"}
            ref_={refs.major}
            tag="Major"
            options={majors}
          />
          <p style={{ opacity: "0" }}>.</p>
        </div>

        <div>
          <TextField
            error={errors.recer}
            defaultValue={data.recer}
            inputRef={refs.recer}
            inputProps={{ "data-ucc": "recer" }}
            label="RECer"
            variant="outlined"
            disabled={status !== "InClass"}
            helperText={errors.recer ? errors.recer : " "}
            sx={{ width: "100%" }}
            onChange={() => setErrors({ ...errors, recer: "" })}
          />
          <p style={{ opacity: "0" }}>.</p>
        </div>
      </div>

      <div className={style.col2}>
        <div>
          <TextField
            error={errors.GPA}
            defaultValue={data.gpa}
            inputRef={refs.gpa}
            type="number"
            label="GPA"
            inputProps={{
              min: 0,
              step: "any",
              "data-ucc": "gpa",
            }}
            sx={{ width: "100%" }}
            helperText={errors.GPA ? errors.GPA : " "}
            disabled={status !== "InClass"}
            onChange={() => setErrors({ ...errors, GPA: "" })}
          ></TextField>
        </div>

        <div>
          <DatePickerComp
            value={data.gradtime}
            disabled={status !== "InClass"}
            ref_={refs.gradtime}
            tag="Graduation Time"
          />
          <p style={{ opacity: "0" }}>.</p>
        </div>
      </div>
      <div className={style.bottom}>
        <section>
          <Button onClick={() => navigate(-1)} variant="text" color="error">
            Cancel
          </Button>
          <Button
            disabled={status !== "InClass"}
            variant="contained"
            onClick={save_other_info}
          >
            Save
          </Button>
        </section>
      </div>
    </section>
  );
}

export { OthersSection };
