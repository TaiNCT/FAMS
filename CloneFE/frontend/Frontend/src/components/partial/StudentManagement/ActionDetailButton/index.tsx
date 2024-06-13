import * as React from "react";
import { styled, alpha } from "@mui/material/styles";
import Button from "@mui/material/Button";
import Menu, { MenuProps } from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import styles from "./style.module.scss";
import scoreIcon from "../../../../assets/LogoStManagement/book-open.png";
import deleteIcon from "../../../../assets/LogoStManagement/deleteforever.png";
import createIcon from "../../../../assets/LogoStManagement/create.png";
import MoreHorizIcon from "@mui/icons-material/MoreHoriz";
import { Link, useNavigate, useParams } from "react-router-dom";
import { Popover } from "@mui/material";
import { StudentInfor } from "../../../../model/StudentLamNS";
import { studentApi } from "../../../../config/axios";
import { toast } from "react-toastify";

const StyledMenu = styled((props: MenuProps) => (
  <Menu
    elevation={0}
    anchorOrigin={{
      vertical: "bottom",
      horizontal: "right",
    }}
    transformOrigin={{
      vertical: "top",
      horizontal: "right",
    }}
    {...props}
  />
))(({ theme }) => ({
  "& .MuiPaper-root": {
    borderRadius: 6,
    marginTop: theme.spacing(1),
    minWidth: 180,
    color:
      theme.palette.mode === "light"
        ? "rgb(55, 65, 81)"
        : theme.palette.grey[300],
    boxShadow:
      "rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px",
    "& .MuiMenu-list": {
      padding: "4px 0",
    },
    "& .MuiMenuItem-root": {
      width: 251,
      "& .MuiSvgIcon-root": {
        fontSize: 18,
        color: theme.palette.text.secondary,
        marginRight: theme.spacing(1.5),
      },
      "&:active": {
        backgroundColor: alpha(
          theme.palette.primary.main,
          theme.palette.action.selectedOpacity
        ),
      },
    },
  },
}));

export default function CustomizedMenus({
  student,
}: {
  student: StudentInfor;
}) {
  // const navigate = useNavigate();
  const navitate = useNavigate();
  const { classId } = useParams();

  const [openActionDetail, setOpenActionDetail] =
    React.useState<null | HTMLElement>(null);

  const [openDeletePopup, setOpenDeletePopup] =
    React.useState<null | HTMLElement>(null);
  const open = Boolean(openActionDetail);
  const openDelete = Boolean(openDeletePopup);

  const handleClickActionDetail = (event: React.MouseEvent<HTMLElement>) => {
    setOpenActionDetail(event.currentTarget);
  };
  const handleCloseActionDetail = () => {
    setOpenActionDetail(null);
  };

  const handleClickDelete = (event: React.MouseEvent<HTMLElement>) => {
    handleCloseActionDetail();
    setOpenDeletePopup(event.currentTarget);
  };
  const handleCloseDeletePopup = () => {
    setOpenDeletePopup(null);
  };

  // DELETE STUDENT
  const handleDelete = () => {
    studentApi
      .delete(`/delete/${student.studentId}`)
      .then((response) => {
        toast.success("Delete successful!");
        window.location.reload();
      })
      .catch((error) => {
        toast.error(error.message);
      });
    handleCloseDeletePopup();
  };

  return (
    <>
      <Button
        id="demo-customized-button"
        aria-controls={open ? "demo-customized-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        variant="contained"
        disableElevation
        onClick={handleClickActionDetail}
        className={styles.ActionButton}
        endIcon={<MoreHorizIcon sx={{ color: "black" }} />}
        sx={{ minWidth: "0" }}
      ></Button>
      <StyledMenu
        disableScrollLock={true}
        id="demo-customized-menu"
        MenuListProps={{
          "aria-labelledby": "demo-customized-button",
        }}
        anchorEl={openActionDetail}
        open={open}
        onClose={handleCloseActionDetail}
      >
        <MenuItem disableRipple>
          <Link
            style={{ display: "flex" }}
            to={`/score/${classId}/edit/${student.studentId}`}
          >
            <img className={styles.menuIcon} alt="" src={scoreIcon} />
            Score Management
          </Link>
        </MenuItem>
        <MenuItem onClick={handleClickDelete} disableRipple>
          <img className={styles.menuIcon} alt="" src={deleteIcon} />
          Delete student
        </MenuItem>
        <MenuItem disableRipple>
          <Link
            style={{ display: "flex" }}
            to={`/score/class/${classId}/s/${student.studentId}/edit`}
          >
            <img className={styles.menuIcon} alt="" src={createIcon} />
            Update student
          </Link>
        </MenuItem>
        {/* <Divider sx={{ my: 0.5 }} /> */}
      </StyledMenu>

      {/* Delete popup */}
      <Popover
        disableScrollLock={true}
        id={"Delete popup"}
        open={openDelete}
        anchorEl={document.body}
        onClose={handleCloseDeletePopup}
        anchorOrigin={{
          vertical: "center",
          horizontal: "center",
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
              Are you sure delete {student.fullName} ?
            </div>
          </div>
          <div className="confirm-pop-up-bottom">
            <div></div>
            <div>
              <button
                onClick={handleCloseDeletePopup}
                className="cancel-button"
              >
                Cancel
              </button>
              <button onClick={handleDelete} className="delete-button">
                Delete
              </button>
            </div>
          </div>
        </div>
      </Popover>
    </>
  );
}
