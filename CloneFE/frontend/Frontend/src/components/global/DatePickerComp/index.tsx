// @ts-nocheck
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { Dayjs } from "dayjs";
import { RefObject, useRef } from "react";
import { useEffect } from "react";

interface IDatePicker {
  tag: string | null;
  value: Dayjs | null;
  ref_?: RefObject<Dayjs>;
  disabled: boolean;
  resetError?: () => void;
}

function DatePickerComp({
  disabled = false,
  ref_ = useRef<Dayjs | null>(null),
  tag = "",
  value = null,
  resetError = null,
}: IDatePicker) {
  useEffect(() => {
    ref_.current = value;
  }, [value]);

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DatePicker
        disabled={disabled}
        value={value}
        label={tag}
        onChange={(v) => {
          resetError();
          ref_.current = v;
        }}
        sx={{ width: "100%" }}
      />
    </LocalizationProvider>
  );
}

export { DatePickerComp };
