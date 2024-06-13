import { Dialog, DialogContent, DialogContentText, DialogTitle, LinearProgress, Modal, Typography } from "@mui/material";
import AutorenewIcon from '@mui/icons-material/Autorenew';
import React from "react";
import { Stack } from "@mui/system";

export default function WaitingModal({ open, setOpen })
{
    const handleClose = () =>
    {
        setOpen(false);
    }
    return (
        <React.Fragment>
            <Dialog open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                maxWidth="md"
                fullWidth={true}>
                <DialogTitle id="alert-dialog-title">
                    <span className="flex items-center text-indigo-800">
                        <AutorenewIcon />
                        <span style={{ marginLeft: "10px" }}>Please wait a moment</span>
                    </span>
                    <hr></hr>
                </DialogTitle>
                <DialogContent style={{ width: '100%' }}>
                    <DialogContentText id="alert-dialog-description" style={{ color: "black" }}>
                        <Stack sx={{ width: '100%', color: 'grey.500' }} spacing={3}>
                            <LinearProgress color="success" />
                        </Stack>
                    </DialogContentText>
                </DialogContent>
            </Dialog>
        </React.Fragment >
    );
}