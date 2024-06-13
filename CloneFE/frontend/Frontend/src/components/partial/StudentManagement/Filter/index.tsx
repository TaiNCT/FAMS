/* eslint-disable @typescript-eslint/no-explicit-any */
import { ChangeEvent, useEffect, useState } from "react";
import styles from "./style.module.scss";
import Popup from "reactjs-popup";
import dayjs, { Dayjs } from "dayjs";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import CloseIcon from "@mui/icons-material/Close";
import { useSearchParams } from "react-router-dom";
import styled from "styled-components";

//Style for Popup
const StyledPopup = styled(Popup)`
  // use your custom style for ".popup-overlay"
  &-overlay {
    background-color: rgba(0, 0, 0, 0.5);
  }
`;

export default function Filter({
  setFilters,
  handleChangePageNumber,
  button,
  filters,
  disableCheckAll,
}: {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  setFilters: any;
  handleChangePageNumber: any;
  button: JSX.Element;
  filters: any;
  disableCheckAll: () => void;
}) {
  //Popup state hook
  const [popupOpen, setPopupOpen] = useState(false);

  //SearchParams
  const [search, setSearch] = useSearchParams();

  const [genderUserChoices, setGenderUserChoices] = useState<string[]>([]);
  const [statusUserChoice, setStatusUserChoice] = useState<string[]>([]);
  const [dobUserChoice, setDobUserChoice] = useState(dayjs(null));

  useEffect(() => {
    setGenderUserChoices(filters.gender);
    setStatusUserChoice(filters.status);
    setDobUserChoice(filters.dob);
  }, [filters]);

  //Functions Handle events
  function handleChangeDate(newDate: Dayjs) {
    setDobUserChoice(newDate);
  }

  type handleChangeEvent = (newchoiceArray: string[]) => void;

  function handleChangeGender(newGenderArray: string[]) {
    setGenderUserChoices(newGenderArray);
  }

  function handleChangeStatus(newStatusArray: string[]) {
    setStatusUserChoice(newStatusArray);
  }

  function handleChangeCheckbox(
    choice: string[],
    setchoice: handleChangeEvent,
    event: ChangeEvent<HTMLInputElement>
  ) {
    if (event.target.checked) {
      setchoice([...choice, event.target.value]);
    } else {
      const newArray = choice.filter((value) => value !== event.target.value);
      setchoice(newArray);
    }
  }

  function handleApplyClick() {
    //Set dob to search params
    const dbDate =
      dobUserChoice.format("YYYY-MM-DD") != "Invalid Date"
        ? dobUserChoice.format("YYYY-MM-DD")
        : "";

    dbDate.length > 0 ? search.set("dob", dbDate) : search.delete("dob");

    //Set gender to search params
    genderUserChoices.length > 0
      ? search.set("gender", genderUserChoices.join(","))
      : search.delete("gender");

    //Set status to serach params
    statusUserChoice.length > 0
      ? search.set("status", statusUserChoice.join(","))
      : search.delete("status");
    setFilters({
      ...filters,
      status: statusUserChoice,
      gender: genderUserChoices,
      dob: dobUserChoice,
    });

    search.set("pageNumber", "1");
    handleChangePageNumber(1);
    setSearch(search, { replace: true });

    //set popup state to close
    setPopupOpen(false);
    disableCheckAll();
  }

  function handleResetClick() {
    search.delete("status");
    search.delete("gender");
    setSearch(search, { replace: true });
    setFilters({ dob: dayjs(null), gender: [], status: [] });
    setPopupOpen(false);
  }

  return (
    <StyledPopup
      open={popupOpen}
      onOpen={() => setPopupOpen(true)}
      className={styles.filterPopup}
      trigger={button}
      closeOnDocumentClick={false}
      arrow={false}
    >
      <div className={styles.frameParent}>
        <div className={styles.instanceParent}>
          <div className={styles.frameGroup}>
            <div className={styles.closeButtonWrapper}>
              <button onClick={() => setPopupOpen(false)}>
                <CloseIcon />
              </button>
            </div>
            <div className={styles.dateOfBirthWrapper}>
              <p className={styles.dateOfBirth}>Date of birth</p>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DemoContainer components={["DatePicker", "DatePicker"]}>
                  <DatePicker
                    slotProps={{
                      textField: {
                        size: "small",
                        error: false,
                      },
                    }}
                    value={dobUserChoice}
                    onChange={(newDate) => handleChangeDate(newDate!)}
                  />
                </DemoContainer>
              </LocalizationProvider>
            </div>
            <div className={styles.checkboxContainer}>
              <div className={styles.genderWrapper}>
                <p>Gender</p>
                <div className={styles.checkboxModule}>
                  <div className={styles.checkboxItems}>
                    <input
                      onChange={(event) =>
                        handleChangeCheckbox(
                          genderUserChoices,
                          handleChangeGender,
                          event
                        )
                      }
                      checked={genderUserChoices.includes("Male")}
                      type="checkbox"
                      value={"Male"}
                    />
                    <p>Male</p>
                  </div>
                  <div className={styles.checkboxItems}>
                    <input
                      onChange={(event) =>
                        handleChangeCheckbox(
                          genderUserChoices,
                          handleChangeGender,
                          event
                        )
                      }
                      checked={genderUserChoices.includes("Female")}
                      type="checkbox"
                      value={"Female"}
                    />
                    <p>Female</p>
                  </div>
                  <div className={styles.checkboxItems}>
                    <input
                      onChange={(event) =>
                        handleChangeCheckbox(
                          genderUserChoices,
                          handleChangeGender,
                          event
                        )
                      }
                      checked={genderUserChoices.includes("Others")}
                      type="checkbox"
                      value={"Others"}
                    />
                    <p>Others</p>
                  </div>
                </div>
              </div>

              {
                <div className={styles.genderWrapper}>
                  <p>Status</p>
                  <div className={styles.checkboxModule}>
                    <div className={styles.checkboxItems}>
                      <input
                        onChange={(event) =>
                          handleChangeCheckbox(
                            statusUserChoice,
                            handleChangeStatus,
                            event
                          )
                        }
                        checked={statusUserChoice.includes("InClass")}
                        type="checkbox"
                        value={"InClass"}
                      />
                      <p>In Class</p>
                    </div>
                    <div className={styles.checkboxItems}>
                      <input
                        onChange={(event) =>
                          handleChangeCheckbox(
                            statusUserChoice,
                            handleChangeStatus,
                            event
                          )
                        }
                        checked={statusUserChoice.includes("DropOut")}
                        type="checkbox"
                        value={"DropOut"}
                      />
                      <p>Drop out</p>
                    </div>
                    <div className={styles.checkboxItems}>
                      <input
                        onChange={(event) =>
                          handleChangeCheckbox(
                            statusUserChoice,
                            handleChangeStatus,
                            event
                          )
                        }
                        checked={statusUserChoice.includes("Finish")}
                        type="checkbox"
                        value={"Finish"}
                      />
                      <p>Finish</p>
                    </div>
                    <div className={styles.checkboxItems}>
                      <input
                        onChange={(event) =>
                          handleChangeCheckbox(
                            statusUserChoice,
                            handleChangeStatus,
                            event
                          )
                        }
                        checked={statusUserChoice.includes("Reserve")}
                        type="checkbox"
                        value={"Reserve"}
                      />
                      <p>Reserve</p>
                    </div>
                  </div>
                </div>
              }
            </div>
            <div className={styles.buttonWrapper}>
              <button onClick={handleResetClick} className={styles.button}>
                Clear
              </button>
              <div className={styles.button1}>
                <button
                  onClick={() => handleApplyClick()}
                  className={styles.button}
                >
                  Save
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </StyledPopup>
  );
}
