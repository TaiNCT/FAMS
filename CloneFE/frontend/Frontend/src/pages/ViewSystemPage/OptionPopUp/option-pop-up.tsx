import { Popover } from "@mui/material";
import React from "react";
import "./option-pop-up.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { studentApi } from "../../../config/axios";
import { Link } from "react-router-dom";
import SendEmailPopupForm from "@/components/partial/EmailInformRemind/SendEmailPopupForm";
import MailOutlineIcon from "@mui/icons-material/MailOutline";

type OptionPopUpProp = {
  Student: any;
};

const OptionPopUp: React.FC<OptionPopUpProp> = ({ Student }) => {
  // POP-OVER SETTINGS
  const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(
    null
  );
  const [open_, setOpen] = React.useState(null);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const open = Boolean(anchorEl);
  const id = open ? "simple-popover" : undefined;

  // DELETE STUDENT
  const handleDelete = () => {
    studentApi
      .delete(`/delete/${Student.studentInfoDTO.studentId}`)
      .then((response) => {
        handleSuccess();
        window.location.reload();
      })
      .catch((error) => {
        console.error("Error updating student status: ", error);
      });
    handleClose();
  };

  const handleSuccess = () => {
    toast.success("Changed student status successful", {
      position: "top-right",
      autoClose: 2000, // Close after 3 seconds
    });
  };

  let HIGHEST_ID = 0; // Initialize HIGHEST_ID to 0 or any initial value
  let HIGHEST_CLASS_ID = null; // Initialize classIdOfHighestId to null

  for (const studentClass of Student.studentClassDTOs) {
    const id = studentClass.id;
    const classId = studentClass.classId;

    if (id && id > HIGHEST_ID) {
      HIGHEST_ID = id;
      HIGHEST_CLASS_ID = classId;
    }
  }
  let STUDENT_ID = Student.studentInfoDTO.studentId;

  return (
    <>
      <div className="option-pop-up-container">
        <Link to={`/score/class/${HIGHEST_CLASS_ID}/s/${STUDENT_ID}/edit`}>
          <button className="option-pop-up-button">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
            >
              <g clip-path="url(#clip0_3384_1794)">
                <path
                  d="M3 17.25V21H6.75L17.81 9.94L14.06 6.19L3 17.25ZM5.92 19H5V18.08L14.06 9.02L14.98 9.94L5.92 19ZM20.71 5.63L18.37 3.29C18.17 3.09 17.92 3 17.66 3C17.4 3 17.15 3.1 16.96 3.29L15.13 5.12L18.88 8.87L20.71 7.04C21.1 6.65 21.1 6.02 20.71 5.63Z"
                  fill="#2D3748"
                />
              </g>
            </svg>
            <span> Edit student</span>
          </button>
        </Link>
        <button
          className="option-pop-up-button"
          onClick={() => {
            setOpen(0);
          }}
        >
          <MailOutlineIcon />
          <span>Send Email</span>
        </button>
        <Link to={`/score/${HIGHEST_CLASS_ID}/edit/${STUDENT_ID}`}>
          <button className="option-pop-up-button">
            <div>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="11"
                height="18"
                viewBox="0 0 11 18"
                fill="none"
              >
                <path
                  d="M1.25 1.125H6.5C7.42826 1.125 8.3185 1.49375 8.97487 2.15013C9.63125 2.8065 10 3.69674 10 4.625V16.875C10 16.1788 9.72344 15.5111 9.23116 15.0188C8.73887 14.5266 8.07119 14.25 7.375 14.25H1.25V1.125Z"
                  stroke="#2D3748"
                  stroke-width="2"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="11"
                height="18"
                viewBox="0 0 11 18"
                fill="none"
              >
                <path
                  d="M9.75 1.125H4.5C3.57174 1.125 2.6815 1.49375 2.02513 2.15013C1.36875 2.8065 1 3.69674 1 4.625V16.875C1 16.1788 1.27656 15.5111 1.76884 15.0188C2.26113 14.5266 2.92881 14.25 3.625 14.25H9.75V1.125Z"
                  stroke="#2D3748"
                  stroke-width="2"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
            </div>
            <span>Score management</span>
          </button>
        </Link>
        <button
          aria-describedby={id}
          onClick={handleClick}
          className="option-pop-up-button"
          disabled={Student.studentInfoDTO.status.toLowerCase() != "inactive"}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            viewBox="0 0 24 24"
            fill="none"
          >
            <g clip-path="url(#clip0_3384_1804)">
              <path
                d="M14.12 10.47L12 12.59L9.87 10.47L8.46 11.88L10.59 14L8.47 16.12L9.88 17.53L12 15.41L14.12 17.53L15.53 16.12L13.41 14L15.53 11.88L14.12 10.47ZM15.5 4L14.5 3H9.5L8.5 4H5V6H19V4H15.5ZM6 19C6 20.1 6.9 21 8 21H16C17.1 21 18 20.1 18 19V7H6V19ZM8 9H16V19H8V9Z"
                fill="#2D3748"
              />
            </g>
          </svg>
          <span>Delete student</span>
        </button>
        <Popover
          id={id}
          open={open}
          anchorEl={anchorEl}
          onClose={handleClose}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
        >
          <div className="confirm-pop-up-container">
            <div className="confirm-pop-up-top">
              <div className="confirm-pop-up-top-1">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_3218_21886)">
                    <path
                      d="M12 5.99L19.53 19H4.47L12 5.99ZM12 2L1 21H23L12 2ZM13 16H11V18H13V16ZM13 10H11V14H13V10Z"
                      fill="#E74A3B"
                    />
                  </g>
                </svg>
                Delete Student
              </div>
              <div className="confirm-pop-up-top-2">
                Are you sure delete {Student.studentInfoDTO.fullName} ?
              </div>
            </div>
            <div className="confirm-pop-up-bottom">
              <div></div>
              <div>
                <button onClick={handleClose} className="cancel-button">
                  Cancel
                </button>
                <button onClick={handleDelete} className="delete-button">
                  Delete
                </button>
              </div>
            </div>
          </div>
        </Popover>
      </div>
      <SendEmailPopupForm
        setOpen={setOpen}
        open={open_}
        title="FAMS - score report"
        sendto={Student.studentInfoDTO.email}
        studentid={Student.studentInfoDTO.studentId}
      />
    </>
  );
};

export default OptionPopUp;
