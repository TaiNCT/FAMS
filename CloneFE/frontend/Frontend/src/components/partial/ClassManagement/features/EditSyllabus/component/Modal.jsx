import WarningAmberIcon from "@mui/icons-material/WarningAmber";
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import ButtonUtil from "../../ClassList/components/Button";
import buttonStyle from "../../../assert/css/Button.module.scss";
import React from 'react';
import { DeleteSyllabusCard } from "../api/ListApi";
import { Bounce, Slide, toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


export default function Modal({ open, setOpen, syllabusData, onDelete, counter, setCounter })
{
    const handleClose = () =>
    {
        setOpen(false);
    }

    const handleDelete = async () =>
    {
        try
        {
            const response = await DeleteSyllabusCard(syllabusData.trainingProgramCode, syllabusData.id);
            if (!response.ok)
            {
                throw new Error('Failed to delete syllabus');
            }
            toast.success('Syllabus deleted successfully', {
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
            setOpen(false);
            if (onDelete)
            {
                onDelete();
            }
            setCounter(counter + 1)
        } catch (error)
        {
            console.error(error);
            toast.error('Failed to delete syllabus', {
                position: "top-right",
                autoClose: 4000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "light",
                transition: Bounce,
            });
        }
    }

    return (
        <React.Fragment>
            <Dialog open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                <DialogTitle id="alert-dialog-title">
                    <span className="flex items-center text-indigo-800">
                        <WarningAmberIcon style={{ color: "red", marginBottom: "0.4rem", }} />
                        <span style={{ marginLeft: "10px" }}>Delete Syllabus</span>
                    </span>
                    <hr></hr>
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description" style={{ color: "black" }}>
                        Do you really want to delete <span style={{ fontWeight: "bold" }}>{syllabusData.name}</span> <br />
                        This class cannot be restored.
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} style={{
                        color: "red",
                        fontWeight: "bold",
                        textTransform: "math-auto",
                    }}>
                        Cancel
                    </Button>
                    <ButtonUtil name="Delete" style={buttonStyle} onClick={handleDelete} />
                </DialogActions>
            </Dialog>
        </React.Fragment>
    );
}