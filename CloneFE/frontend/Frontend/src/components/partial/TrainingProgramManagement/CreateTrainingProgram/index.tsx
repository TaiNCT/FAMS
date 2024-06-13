import SearchList from "./components/SearchList";
import style from "./style.module.scss";
import { FormEvent, useEffect, useState } from "react";
import { Syllabus } from "./models/syllabus.model";
import useFetchAxios from "./hooks/useFetchAxios";
import SyllabusList from "./components/SyllabusList";
import { HiMiniMagnifyingGlass } from "react-icons/hi2";
// import ImportDialog from "./components/Dialog";
import { Field, Form, Formik, useFormik } from "formik";
import { createTrainingProgram } from "./models/requests/createTrainingProgram.model";
import * as yup from "yup";
import axios from "axios";
import formatDateToYYYYMMDD from "./utils/dateFormat";
import { WEB_API_URL } from "../config";
import { toast } from "react-toastify";
import axiosAuth from "../api/axiosAuth";
import { Navigate, useNavigate } from "react-router-dom";

const ACTIVE = "Active";
const INACTIVE = "InActive";
const DRAFT = "Draft";
// type Status = typeof ACTIVE | typeof INACTIVE | typeof DRAFT;

const CreateTrainingProgram: React.FC = () => {
  const BASE_URL = WEB_API_URL;
  const navigate = useNavigate();

  // Context properties
  // const { setShowForm } = useGlobalContext();

  // Navigate
  // const navigate = useNavigate();

  // Fetch data
  const { data } = useFetchAxios();

  // New Training Program features
  // let status: Status = ACTIVE;

  // Import training program
  // const [file, setFile] = useState<File | null>();
  // const [encodingType, setEncodingType] = useState<string>();
  // const [columnSeparator, setColumnSeparator] = useState<string>();
  // const [scanning, setScanning] = useState<string>();
  // const [duplicateHandle, setDuplicateHandle] = useState<string>();

  // States
  const [isSubmit, setIsSubmit] = useState(false);
  const [searchValue, setSearchValue] = useState<string>("");
  const [searchResult, setSearchResult] = useState<number>(0);
  const [selectItems, setSelectItems] = useState<Syllabus[]>([]);
  const [items, setItems] = useState<Syllabus[]>([]);
  const [totalDay, setTotalDay] = useState<number>(0);
  const [totalHour, setTotalHour] = useState<number>(0);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);
  const [validateErrorMsg, setValidateErrorMsg] = useState<string | null>(null);
  const [statusChange, setStatusChange] = useState<string>(ACTIVE);

  // Initial Values
  const initalValues: createTrainingProgram = {
    createdBy: localStorage.getItem("username"),
    createdDate: new Date(),
    updatedBy: "",
    updatedDate: new Date(),
    days: 0,
    hours: 0,
    status: "",
    syllabiIDs: [],
    name: "",
  };

  // Validation Schema
  const schema = yup.object().shape({
    name: yup.string().min(10, "Name must at least 10 characters"),
  });

  const formik = useFormik({
    initialValues: initalValues,
    validationSchema: schema,
    onSubmit: async (values) => {

      if (formik.values.status == "") formik.values.status = ACTIVE;
      if (formik.values.syllabiIDs.length == 0) {
        toast.error("Please select syllabus", { autoClose: 1500 });
        return;
      }

      const result = await axiosAuth
        .post(`trainingprograms`, values)
        .then(() => {
          toast.success("Create training program succesfully", {
            autoClose: 2500,
          });
          let toastMessage = `Create training program succesfully`;
          handleCancel();
          navigate("/trainingprogram", { state: { toastMessage } });
        })
        .catch((err) => {
          toast.error(err.message, { autoClose: 2500 });
        });

    },
  });

  useEffect(() => {
    setItems(data);
    // count total day
    countTotalDay();
    // count total hour
    countTotalHour();
  }, [searchValue, selectItems]);

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const handleSubmit = async (event: FormEvent<HTMLButtonElement>) => {
    // setErrorMsg(null);
    event.preventDefault();

    if (formik.values.name === "") {
      setValidateErrorMsg("Please input Program Name");
    } else if (formik.values.name.length < 10) {
      setValidateErrorMsg("Program name must greater than 10 characters");
    } else {
      setIsSubmit(true);
      setErrorMsg(null);
    }
  };

  const handleBack = () => {
    setIsSubmit(false);
    setErrorMsg(null);
    setValidateErrorMsg(null);
  };

  const handleCancel = () => {
    // Navigate to list training program page
    setIsSubmit(false);
    setSearchValue("");
    setSelectItems([]);
    setErrorMsg(null);
    setValidateErrorMsg(null);

    // Set form value to defaults
    formik.values.name = "";
  };

  const handleSave = () => {
  };

  const handleDelete = (id: number) => {
    setSelectItems(selectItems.filter((syllabus) => syllabus.id !== id));
  };

  const handleOnSearch = (syllabus: Syllabus, searchTerm: string) => {
    // check duplicate select item
    const syllabusItem = selectItems.find((s) => s.id === syllabus.id);
    if (!syllabusItem) {
      // not exist
      // update selected list
      setSelectItems([...selectItems, syllabus]);
      setErrorMsg(null);

      // set formik values
      formik.values.syllabiIDs.push(syllabus.syllabusId);
    } else {
      setErrorMsg(`Syllabus (${syllabus.topicName}) is already selected`);
    }

    // update searcg result and its value
    setSearchResult(syllabus.id);
    setSearchValue(searchTerm);
  };

  const handleStatusChange = () => {
    const status =
      statusChange == ACTIVE
        ? INACTIVE
        : statusChange == INACTIVE
        ? DRAFT
        : ACTIVE;

    setStatusChange(status);
    formik.values.status = status;
  };

  const countTotalDay = () => {
    let days = 0;
    selectItems.map((syllabus) => (days += syllabus.days));
    days = Math.ceil(days);
    setTotalDay(days);

    formik.values.days = days;
  };

  const countTotalHour = () => {
    let hours = 0;
    selectItems.map((syllabus) => (hours += syllabus.hours));
    hours = Math.ceil(hours);
    setTotalHour(hours);

    formik.values.hours = hours;
  };

  return (
    <main className={style.main}>
      <Formik
        initialValues={initalValues}
        onSubmit={(values, actions) => {
          alert(JSON.stringify(values, null, 2));
          actions.setSubmitting(false);
        }}
        validationSchema={schema}
      >
        <Form className={style.form} onSubmit={formik.handleSubmit}>
          {!isSubmit && (
            <div className={style.newProgram}>
              <h1 className={style.header}>New Training Program</h1>
              <div className={style.formElement} style={{ marginTop: "2rem" }}>
                <div
                  style={{
                    display: "flex",
                    alignItems: "center",
                    width: "8rem",
                  }}
                >
                  <label htmlFor="program-name">Program Name: </label>
                </div>
                <Field
                  id="program-name"
                  variant="outlined"
                  type="text"
                  value={formik.values.name}
                  onChange={formik.handleChange("name")}
                  onBlur={formik.handleBlur("name")}
                  required
                />
              </div>
              {/* Show error */}
              {validateErrorMsg && <p style={{color: "red", fontWeight: "500", marginTop: "0rem", marginLeft: "1rem", marginBottom: "1rem", fontStyle: "italic"}}>{validateErrorMsg}</p>}
              <button onClick={handleSubmit}>Create</button>
            </div>
          )}
          {isSubmit && (
            <>
              <div className={style.detail}>
                <h1>
                  <p>Training Program</p>
                  <span>
                    <div>{formik.values.name}</div>
                    <div onClick={handleStatusChange}>
                      {statusChange == ACTIVE ? (
                        <p className={style.active}>Active</p>
                      ) : statusChange == INACTIVE ? (
                        <p className={style.inactive}>InActive</p>
                      ) : (
                        <p className={style.draft}>Draft</p>
                      )}
                    </div>
                  </span>
                </h1>
                <article className={style.trainingProgramDetail}>
                  <p>
                    {totalDay > 0 ? totalDay : `...`} days(
                    {totalHour > 0 ? totalHour : `...`} hours)
                  </p>
                  <span>
                    {selectItems.length ? (
                      <>
                        <span>
                          Modified on
                          <b style={{ marginLeft: "0.4rem" }}>
                            {formatDateToYYYYMMDD(
                              selectItems[selectItems.length - 1].modifiedDate
                            )}
                          </b>{" "}
                          by
                        </span>
                        <b style={{ marginLeft: "0.4rem" }}>
                          {selectItems[selectItems.length - 1].modifiedBy}
                        </b>
                      </>
                    ) : (
                      <>
                        Modified on ... by
                        <b style={{ fontWeight: "bold" }}>...</b>
                      </>
                    )}
                  </span>
                </article>
              </div>
              <div className={style.content}>
                <p>Content</p>
                {selectItems.length ? (
                  <SyllabusList
                    syllabi={selectItems}
                    handleDelete={handleDelete}
                  />
                ) : (
                  <></>
                )}
                <div className={style.searchPart}>
                  <div className={style.searchBox}>
                    <label htmlFor="search-syllabus">Search Syllabus</label>
                    <input
                      id="search-syllabus"
                      type="text"
                      value={searchValue}
                      onChange={(e) => setSearchValue(e.target.value)}
                      style={{ paddingLeft: "2rem" }}
                    />
                    <HiMiniMagnifyingGlass
                      className={style.manifyingClass}
                      size={24}
                    />
                  </div>
                  {items.length ? (
                    <SearchList
                      items={items}
                      handleOnSearch={handleOnSearch}
                      searchValue={searchValue}
                    />
                  ) : (
                    <></>
                  )}
                </div>
              </div>
              <div className={style.action}>
                <div>
                  <button onClick={handleBack}>Back</button>
                </div>
                <div>
                  <span onClick={handleCancel}>Cancel</span>
                  <button type="submit" onClick={handleSave}>
                    Save
                  </button>
                </div>
              </div>
            </>
          )}
          {/* Show error */}
          {errorMsg && <p className={style.errorMsg}>{errorMsg}</p>}
        </Form>
      </Formik>
    </main>
  );
};
export { CreateTrainingProgram };
