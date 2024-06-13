import * as React from "react";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { TextField } from "@mui/material";
import dayjs from "dayjs";

export default function BasicDateTimePicker({ selectedStartDate, setSelectedStartDate, selectedEndDate, setSelectedEndDate })
{
  const handleStartDateChange = (date) =>
  {
    const formattedDate = dayjs(date).format('YYYY-MM-DD');
    setSelectedStartDate(formattedDate);
  };

  const handleEndDateChange = (date) =>
  {
    const formattedDate = dayjs(date).format('YYYY-MM-DD');
    setSelectedEndDate(formattedDate);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DemoContainer components={["DatePicker"]}>
        <div style={{ width: "50%" }}>
          <DatePicker
            value={dayjs(selectedStartDate)}
            onChange={handleStartDateChange}
          />
        </div>
        <div style={{ width: "50%", marginTop: "0px" }}>
          <DatePicker
            value={dayjs(selectedEndDate)}
            onChange={handleEndDateChange}
          />
        </div>
      </DemoContainer>
    </LocalizationProvider>
  );
}
