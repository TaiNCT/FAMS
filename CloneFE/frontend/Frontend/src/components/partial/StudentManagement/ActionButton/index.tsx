import * as React from "react";
import { styled, alpha } from "@mui/material/styles";
import Button from "@mui/material/Button";
import Menu, { MenuProps } from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import styles from "./style.module.scss";
import deleteIcon from "../../../../assets/LogoStManagement/deleteforever.png";
import createIcon from "../../../../assets/LogoStManagement/create.png";
import { toast } from "react-toastify";
import { studentApi } from "../../../../config/axios";
import UpdateStatusPopup from "../UpdateStatusPopup";
import { useParams } from "react-router-dom";

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
  selectedStudentIds,
}: {
  selectedStudentIds: string[];
}) {
  const { classId } = useParams();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [deleteSuccess, setDeleteSuccess] = React.useState(true);
  const [updateStatusPopupOpen, setUpdateStatusPopupOpen] =
    React.useState(false);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleDelete = () => {
    // Make API call to delete students with selected IDs
    selectedStudentIds.forEach((studentId) => {
      studentApi
        .delete(`/delete/${studentId}`)
        .then((response) => {})
        .catch((error) => {
          setDeleteSuccess(false);
        });
    });
    if (deleteSuccess) {
      toast.success("Delete Successfully");
    } else {
      toast.error("Something wrong when delete students");
    }
    window.location.reload();
  };
  const handleUpdateStatusClick = () => {
    setUpdateStatusPopupOpen(true);
    handleClose();
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
        onClick={handleClick}
        endIcon={<KeyboardArrowDownIcon />}
        className={styles.ActionButton}
      >
        Action
      </Button>
      <StyledMenu
        disableScrollLock={true}
        id="demo-customized-menu"
        MenuListProps={{
          "aria-labelledby": "demo-customized-button",
        }}
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
      >
        <MenuItem onClick={handleDelete} disableRipple>
          <img className={styles.menuIcon} alt="" src={deleteIcon} />
          Delete student
        </MenuItem>
        <MenuItem onClick={handleUpdateStatusClick} disableRipple>
          {" "}
          {/* Open update status popup */}
          <img className={styles.menuIcon} alt="" src={createIcon} />
          Update status student
        </MenuItem>
        {/* <Divider sx={{ my: 0.5 }} /> */}
      </StyledMenu>
      {/* Render update status popup */}
      <UpdateStatusPopup
        selectedStudentIds={selectedStudentIds}
        open={updateStatusPopupOpen}
        classID={classId}
        onClose={() => setUpdateStatusPopupOpen(false)}
      />
    </>
  );
}
