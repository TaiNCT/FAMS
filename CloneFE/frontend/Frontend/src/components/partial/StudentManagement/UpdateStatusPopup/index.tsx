import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from "@mui/material";
import { toast } from "react-toastify";
import { studentApi } from "../../../../config/axios";
import styles from "./style.module.scss";

interface UpdateStatusPopupProps {
  selectedStudentIds: string[];
  open: boolean;
  onClose: () => void;
  classID: string;
}

const UpdateStatusPopup: React.FC<UpdateStatusPopupProps> = ({
  selectedStudentIds,
  open,
  onClose,
  classID,
}) => {
  const [status, setStatus] = useState<string>("");
  const [updateResults, setUpdateResults] = useState<boolean[]>([]);

  useEffect(() => {
    if (updateResults.length > 0) {
      const successfulUpdates = updateResults.filter((result) => result).length;
      const failedUpdates = updateResults.filter((result) => !result).length;
      toast.success(`${successfulUpdates} students updated successfully`);
      if (failedUpdates > 0) {
        toast.error(`${failedUpdates} students failed to update`);
      }
    }
  }, [updateResults]);

  const handleUpdateStatus = () => {
    const updatePromises = selectedStudentIds.map(
      (studentId) =>
        studentApi
          .post(`/changeStatus/s/${studentId}/c/${classID}/status/${status}`)
          .then(() => true) // Mark as successful if no error
          .catch(() => false) // Mark as unsuccessful if error
    );

    Promise.all(updatePromises)
      .then((results) => {
        setUpdateResults(results);
      })
      .catch(() => {
        toast.error("Something went wrong when updating student status");
      })
      .finally(() => {
        onClose();
      });
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <div className={styles.popupContainer}>
        <DialogTitle>
          <div className={styles.popupTitle}>Update Status</div>
        </DialogTitle>
        <DialogContent>
          <div className={styles.popupContent}>
            <div className={styles.popupMessage}>
              Are you sure to update status of {selectedStudentIds.length}{" "}
              Student?
            </div>
            <FormControl className={styles.popupSelect} fullWidth>
              <InputLabel id="status-label">Status</InputLabel>
              <Select
                labelId="status-label"
                id="status"
                value={status}
                label="Status"
                onChange={(e) => setStatus(e.target.value as string)}
              >
                <MenuItem value="InClass">InClass</MenuItem>
                <MenuItem value="DropOut">DropOut</MenuItem>
                <MenuItem value="Finish">Finish</MenuItem>
                <MenuItem value="Reserve">Reserve</MenuItem>
              </Select>
            </FormControl>
          </div>
        </DialogContent>
        <DialogActions className={styles.popupActions}>
          <Button onClick={onClose}>Cancel</Button>
          <Button className={styles.popupButton} onClick={handleUpdateStatus}>
            Update
          </Button>
        </DialogActions>
      </div>
    </Dialog>
  );
};

export default UpdateStatusPopup;
