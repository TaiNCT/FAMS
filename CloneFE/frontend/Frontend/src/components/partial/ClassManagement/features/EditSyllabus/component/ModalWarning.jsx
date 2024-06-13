import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import WarningAmberIcon from "@mui/icons-material/WarningAmber";
import buttonStyle from "../../../assert/css/Button.module.scss";
import * as React from "react";
import ButtonUtil from "../../ClassList/components/Button";
import { CreateClassDetail, CreateClassUser } from "../../CreateClass/api/ListApi";
import { Slide, toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

export default function ModalWarning({ openSecond, setOpenSecond, data })
{
    const navigate = useNavigate();
    const handleClose = () =>
    {
        setOpenSecond(false);
    }
    const handleSave = async () =>
    {
        const storedValues = JSON.parse(localStorage.getItem("selectedValues")) || {}
        if (data && storedValues)
        {
            const response = await CreateClassDetail(data);
            const result = await response.json();
            if (response.ok)
            {
                toast.success("Create successfully", {
                    position: "top-right",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: false,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                    transition: Slide,
                });
                if (storedValues.Admin != null)
                {
                    const createClassUser = await CreateClassUser(
                        result.data?.classId,
                        storedValues.Admin
                    );
                    const createClassUser2 = await CreateClassUser(
                        result.data?.classId,
                        trainerPicked.userId
                    )
                    if (createClassUser2.ok)
                    {
                        navigate("/classList");
                    }
                } else
                {
                    navigate("/classList");
                }
            } else
            {
                toast.error("Create fail", {
                    position: "top-right",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: false,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                    transition: Slide,
                });
                setOpenSecond(false);
            }
        }
    }
    return (
        <React.Fragment>
            <Dialog open={openSecond} onClose={handleClose} aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                <DialogTitle id="alert-dialog-title">
                    <span
                        style={{
                            display: "flex",
                            alignItems: "center",
                            gap: "10px",
                            color: "#2a4365",
                        }}
                    >
                        <WarningAmberIcon style={{ color: "red" }} /> Required Information
                    </span>
                    <hr style={{ border: "1px solid black" }}></hr>
                </DialogTitle>
                <DialogContent>
                    <DialogContentText
                        id="alert-dialog-description"
                        style={{ color: "black" }}
                    >
                        Trainer(s) is missing
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button style={{
                        color: "red",
                        textDecoration: "underline",
                        fontWeight: "bold",
                        textTransform: "math-auto",
                    }}
                        onClick={handleClose}
                    >
                        Cancel
                    </Button>
                    <Button style={{
                        backgroundColor: "#2d3748",
                        borderRadius: "8px",
                        color: "white",
                        border: "none",
                        textTransform: "math-auto",
                        fontWeight: "bold"
                    }} onClick={handleSave}>
                        Save
                    </Button>
                </DialogActions>
            </Dialog>
        </React.Fragment >
    )
}