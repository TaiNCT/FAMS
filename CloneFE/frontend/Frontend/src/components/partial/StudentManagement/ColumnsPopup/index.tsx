/* eslint-disable @typescript-eslint/no-explicit-any */
import Popup from "reactjs-popup";
import styles from "./style.module.scss";
import dragIcon from "../../../../assets/LogoStManagement/dragindicator.png";
import {
  Dispatch,
  SetStateAction,
  useState,
  ChangeEvent,
  useEffect,
} from "react";
import styled from "styled-components";
interface SelectedColumnsState {
  [key: string]: boolean;
  FullName: boolean;
  Dob: boolean;
  Gender: boolean;
  Phone: boolean;
  Email: boolean;
  University: boolean;
  Major: boolean;
  GraduateTime: boolean;
  GPA: boolean;
  Address: boolean;
  RECer: boolean;
  Status: boolean;
}

//Style for Popup
const StyledPopup = styled(Popup)`
  // use your custom style for ".popup-overlay"
  &-overlay {
    background-color: rgba(0, 0, 0, 0.5);
  }
`;

const columns = [
  { id: "01", value: "FullName", label: "Full Name" },
  { id: "02", value: "Dob", label: "Birth Day" },
  { id: "03", value: "Gender", label: "Gender" },
  { id: "04", value: "Phone", label: "Phone" },
  { id: "05", value: "Email", label: "Email" },
  { id: "06", value: "University", label: "University" },
  { id: "07", value: "Major", label: "Major" },
  { id: "08", value: "GraduateTime", label: "Graduate Time" },
  { id: "09", value: "GPA", label: "GPA" },
  { id: "10", value: "Address", label: "Address" },
  { id: "11", value: "RECer", label: "RECer" },
  { id: "12", value: "Status", label: "Status" },
];

export default function ColumnsPopup({
  button,
  selectedColumns,
  setSelectedColumns,
}: {
  button: any;
  selectedColumns: any;
  setSelectedColumns: Dispatch<SetStateAction<SelectedColumnsState>>;
}) {
  const [isCheckAll, setIsCheckAll] = useState(false);
  const [popupOpen, setPopupOpen] = useState(false);
  const [userChoice, SetUserChoice] =
    useState<SelectedColumnsState>(selectedColumns);

  useEffect(() => {
    SetUserChoice(selectedColumns);
  }, [selectedColumns]);

  // Function to handle checkbox change
  function handleCheckboxChange(event: ChangeEvent<HTMLInputElement>) {
    const value = event.target.value;
    if (event.target.checked) {
      SetUserChoice({
        ...userChoice,
        [value]: true,
      });
    } else {
      SetUserChoice({
        ...userChoice,
        [value]: false,
      });
    }
  }

  useEffect(() => {
    // Check if all checkboxes are selected after state update
    setIsCheckAll(checkSelectAll());
  }, [userChoice]); // Run this effect whenever userChoice changes

  //function for select all checkbox
  function handleSelectAll() {
    setIsCheckAll(!isCheckAll);

    const updatedUserChoice: SelectedColumnsState = { ...userChoice }; // Create a copy of userChoice

    // Iterate through each property and set its value to true
    for (const key in updatedUserChoice) {
      if (Object.prototype.hasOwnProperty.call(updatedUserChoice, key)) {
        updatedUserChoice[key] = !isCheckAll;
      }
    }

    // Set the updated userChoice state
    SetUserChoice(updatedUserChoice);
  }

  function checkSelectAll() {
    for (const key in userChoice) {
      if (userChoice[key] != true) {
        return false;
      }
    }
    return true;
  }

  function saveToLocalStorage() {
    localStorage.setItem("selectedColumns", JSON.stringify(userChoice));
  }

  // Function to handle accept button click
  const handleAcceptClick = () => {
    setSelectedColumns(userChoice);
    saveToLocalStorage();
    setPopupOpen(false);
  };

  const handleResetClick = () => {
    //Get old data from Local Storage
    const selectedColumnsRaw = localStorage.getItem("selectedColumns");
    if (selectedColumnsRaw) {
      const items = JSON.parse(selectedColumnsRaw);
      setSelectedColumns(items);
    }
  };

  return (
    <>
      <StyledPopup
        open={popupOpen}
        onOpen={() => setPopupOpen(true)}
        className={styles.columsPopup}
        trigger={button}
        arrow={false}
      >
        <div className={styles.columns}>
          <div className={styles.frameRoot}>
            <div className={styles.selectColumns}>Select columns</div>
          </div>
          <section className={styles.dDcheckboxFrameParent}>
            <div className={styles.dDcheckboxFrame}>
              <div className={styles.dDcheckbox}>
                <div className={styles.ddCheckbox}>
                  <div className={styles.rectangleGlyph}>
                    <input
                      id="SelectAll"
                      className={styles.checkLabelDragIndicator}
                      type="checkbox"
                      name="columnCheckbox"
                      checked={isCheckAll}
                      onChange={handleSelectAll}
                    />
                  </div>
                  <div className={styles.label}>Select All</div>
                </div>
                <img
                  className={styles.dragIndicatorIcon}
                  loading="eager"
                  alt=""
                  src={dragIcon}
                />
              </div>
              {columns.map((column) => {
                return (
                  <div className={styles.dDcheckbox}>
                    <div className={styles.ddCheckbox}>
                      <div className={styles.rectangleGlyph}>
                        <input
                          id={column.id}
                          className={styles.checkLabelDragIndicator}
                          type="checkbox"
                          name="columnCheckbox"
                          value={column.value}
                          checked={userChoice[column.value]}
                          onChange={(e) => handleCheckboxChange(e)}
                        />
                      </div>
                      <div className={styles.label}>{column.label}</div>
                    </div>
                    <img
                      className={styles.dragIndicatorIcon}
                      loading="eager"
                      alt=""
                      src={dragIcon}
                    />
                  </div>
                );
              })}
            </div>
          </section>
          <div className={styles.frameDDButton}>
            <button onClick={handleResetClick} className={styles.ddButton}>
              <b className={styles.buttonLabel}>Reset</b>
            </button>
            <button onClick={handleAcceptClick} className={styles.ddButton1}>
              <b className={styles.buttonLabel1}>Apply</b>
            </button>
          </div>
        </div>
      </StyledPopup>
    </>
  );
}
