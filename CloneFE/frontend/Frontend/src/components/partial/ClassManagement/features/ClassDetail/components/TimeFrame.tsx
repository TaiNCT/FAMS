import React from "react";
import Collapse from "../../../../../global/Collapse";
import Month from "../../../../../global/Month";
import { learningDates } from "../data/data";
import CalendarTodayIcon from "@/assets/icons/nav-menu-icons/CalendarTodayIcon";
import { useAppSelector } from "@/hooks/useRedux";
import { formatDate } from "../../../../../../utils/DateUtils";

const TimeFrame = ({ ...props }: React.HTMLAttributes<HTMLDivElement>) => {
const data = useAppSelector((state) => state.class.data);
  if (!data) return;
  return (
    <Collapse
      icon={<CalendarTodayIcon />}
      title="Time frame"
      description={`${formatDate(new Date(data.startDate))} to ${formatDate(new Date(data.endDate))} `}
      {...props}
    >
      <Month learningDates={learningDates} startDate={new Date(data.startDate)} endDate={new Date(data.endDate)}/>
    </Collapse>
  );
};

export default TimeFrame;
