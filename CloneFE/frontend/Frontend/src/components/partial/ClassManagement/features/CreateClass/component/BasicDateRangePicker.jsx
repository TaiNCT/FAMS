import * as React from "react";
import "react-date-range/dist/styles.css";
import { DatePicker, Space } from "antd";
import "react-date-range/dist/theme/default.css";
import dayjs from "dayjs";
import style from "../../../assert/css/DateRange.module.scss";

export default function BasicDateRangePicker({
  setStartDate,
  setEndDate,
  programPicked,
  setErrorsValidate
})
{
  const [state, setState] = React.useState();
  const { RangePicker } = DatePicker;

  const formatted = state?.map((item) =>
  {
    return dayjs(item).format("YYYY-MM-DD");
  });


  React.useEffect(() =>
  {
    setStartDate(formatted ? formatted[0] : null);
    setEndDate(formatted ? formatted[1] : null);
  }, [state, setStartDate, setEndDate]);

  const dateRender = (current) =>
  {
    if (formatted && formatted.length === 2)
    {
      const startDate = dayjs(formatted[0]);
      const endDate = dayjs(formatted[1]);

      if (
        (current.isAfter(startDate) || current.isSame(startDate, "day")) &&
        (current.isBefore(endDate) || current.isSame(endDate, "day")) &&
        (current.day() === 1 || current.day() === 3 || current.day() === 5)
      )
      {
        return (
          <div
            className="ant-picker-cell-inner"
            style={{
              border: "1px solid #1890ff",
              borderRadius: "50%",
              backgroundColor: "#2d3748",
              color: "white",
            }}
          >
            {current.date()}
          </div>
        );
      }
    }
    return <div className="ant-picker-cell-inner">{current.date()}</div>;
  };
  
  return (
    <div
      style={{
        background: "white",
      }}
    >
      <div >
        <RangePicker
          onFocus={(_, info) =>
          {
          }}
          onChange={(item) => setState(item)}
          cellRender={dateRender}
          variant="filled"
          disabled={Object.keys(programPicked).length > 0 ? false : true}
          allowEmpty
        />
      </div>
    </div>
  );
}
