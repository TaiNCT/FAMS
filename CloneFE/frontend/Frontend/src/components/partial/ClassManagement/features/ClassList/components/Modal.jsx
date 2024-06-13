import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import WarningAmberIcon from "@mui/icons-material/WarningAmber";
import ButtonUtil from "./Button";
import buttonStyle from "../../../assert/css/Button.module.scss";
export default function Modal({ open, setOpen, a }) {
  const handleClose = () => {
    setOpen(false);
  };

  return (
    <React.Fragment>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          <span
            style={{
              display: "flex",
              alignItems: "center",
              gap: "10px",
              color: "#2a4365",
            }}
          >
            <WarningAmberIcon style={{ color: "red" }} /> Delete Class
          </span>
          <hr></hr>
        </DialogTitle>
        <DialogContent>
          <DialogContentText
            id="alert-dialog-description"
            style={{ color: "black" }}
          >
            Do you really want to delete "DevOps foundation" class? <br />
            This class cannot be restored.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button
            style={{
              color: "red",
              textDecoration: "underline",
              fontWeight: "bold",
              textTransform: "math-auto",
            }}
            onClick={handleClose}
          >
            Cancel
          </Button>
          <ButtonUtil name="Delete" style={buttonStyle} />
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}
