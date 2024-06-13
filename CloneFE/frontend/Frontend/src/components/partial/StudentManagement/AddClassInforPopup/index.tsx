// @ts-nocheck
import Popup from "reactjs-popup";
import styles from "./style.module.scss";
import { useEffect, useState } from "react";
import Select from "react-select";
import { studentApi } from "../../../../config/axios";
import { Class } from "../../../../model/StudentLamNS";
import styled from "styled-components";
import closeIcon from "../../../../assets/LogoStManagement/cancel-ugh.png";
import { StudentClasses } from "../../../../model/StudentLamNS";
import dayjs from "dayjs";
import { toast } from "react-toastify";

const StyledPopup = styled(Popup)`
  // use your custom style for ".popup-overlay"
  &-overlay {
    background-color: rgba(0, 0, 0, 0.5);
  }
`;

export default function AddClassInforPopup({
  button,
  studentId,
  fetchdata,
}: {
  button: any;
  studentId: string;
  fetchdata: () => Promise<void>;
}) {
  const now = new Date();
  const [popupOpen, setPopupOpen] = useState(false);
  const [ClassList, setClassList] = useState<Class[]>();
  const [error, setError] = useState("");
  const [request, setRequest] = useState<StudentClasses>({
    studentId: studentId,
    studentClassId: null,
    result: 1,
    method: 1,
    gpaLevel: 1,
    finalScore: 0,
    classId: "",
    certificationStatus: 1,
    certificationDate: dayjs().toISOString(),
    attendingStatus: "InClass",
    class: null,
  });

  function handleChangeReactSelect(value: string, name: string) {
    setError("");
    setRequest({
      ...request,
      [name]: value,
    });
  }

  function handleSaveClick(
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) {
    event.preventDefault();
    if (request.classId === "") {
      setError("Please select an option.");
      return;
    } else {
      studentApi
        .post(`/AddNewStudentClassInfor`, request)
        .then((response) => {
          toast.success(response.data.result, {
            autoClose: 2500,
          });
          fetchdata();
        })
        .catch((error) => {
          toast.error(error.message, { autoClose: 2500 });
        });
      setPopupOpen(false);
    }
  }

  function handleCloseClick(
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) {
    event.preventDefault();
    setRequest({
      ...request,
      classId: "",
    });
    setError("");
    setPopupOpen(false);
  }

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await studentApi.get(
          `/GetListClassStudentNotIn?studentId=${studentId}`
        );

        setClassList(response.data.result);
      } catch (error) {
      }
    };
    fetchData();
  }, []);

  const options = ClassList?.map((classInfo) => ({
    value: classInfo.classId,
    label: classInfo.className,
  }));

  return (
    <>
      <StyledPopup
        open={popupOpen}
        onOpen={() => setPopupOpen(true)}
        className={styles.myPopup}
        trigger={button}
        arrow={false}
      >
        <section>
          <div className={styles.inputGroup}>
            <div className={styles.PopupHeader}>
              <div style={{ opacity: "0" }}>1</div>
              <div>Add Class Infor</div>
              <button onClick={(event) => handleCloseClick(event)}>
                <img src={closeIcon} alt="closeIcon" />
              </button>
            </div>
            <form action="">
              <div className={styles.formContainer}>
                <div className={styles.inputFormGroup}>
                  <label htmlFor="">Class</label>
                  <Select
                    options={options}
                    name="classId"
                    onChange={(e) =>
                      handleChangeReactSelect(e ? e.value : "", "classId")
                    }
                  />
                </div>
                {error && <div style={{ color: "red" }}>{error}</div>}
                <div className={styles.formFooter}>
                  <button
                    onClick={(event) => handleCloseClick(event)}
                    className={styles.cancelButton}
                  >
                    Cancel
                  </button>
                  <button
                    onClick={(e) => handleSaveClick(e)}
                    className={styles.CreateButton}
                  >
                    Create
                  </button>
                </div>
              </div>
            </form>
          </div>
        </section>
      </StyledPopup>
    </>
  );
}
