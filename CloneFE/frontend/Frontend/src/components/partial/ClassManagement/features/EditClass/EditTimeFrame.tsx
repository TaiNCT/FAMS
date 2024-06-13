import React from "react";
import Collapse from "@/components/global/Collapse";
import Month from "@/components/global/Month";
import { learningDates } from "./data/data";
import CalendarTodayIcon from "@/assets/icons/nav-menu-icons/CalendarTodayIcon";
import { useAppSelector } from "@/hooks/useRedux";
import { formatDate } from "@/utils/DateUtils";
import { DatePicker, Form } from "antd";
import dayjs from "dayjs";
const { RangePicker } = DatePicker;
const EditTimeFrame = ({ ...props }: React.HTMLAttributes<HTMLDivElement>) => {
  const data = useAppSelector((state) => state.class.data);
  if (!data) return;
  return (
    <Collapse
      icon={<CalendarTodayIcon />}
      title="Time frame"
      description={
        <Form.Item
          name="durationDate"
          noStyle
          initialValue={[
            dayjs(data.startDate, "YYYY/MM/DD"),
            dayjs(data.endDate, "YYYY/MM/DD"),
          ]}
        >
          <RangePicker format="DD/MM/YYYY"/>
        </Form.Item>
      }
      {...props}
    >
      <Month
        learningDates={learningDates}
        startDate={new Date(data.startDate)}
        endDate={new Date(data.endDate)}
      />
    </Collapse>
  );
};

export default EditTimeFrame;
