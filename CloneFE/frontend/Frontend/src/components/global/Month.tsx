import { useState } from "react";
import {
  daysOfWeek,
  monthAbbreviation,
  getDaysInMonth,
  compareDate,
  isDateBetween,
  isMondayWednesdayFriday,
} from "@/utils/DateUtils";
import ArrowBackIosIcon from "@/assets/icons/navigator-icons/ArrowBackIosIcon";
import ArrowForwardIcon from "@/assets/icons/navigator-icons/ArrowForwardIcon";
import HomeWorkIcon from "@/assets/icons/other-icons/HomeWorkIcon";
import LectureIcon from "@/assets/icons/delivery-types-icons/LectureIcon";
import GradeIcon from "@/assets/icons/indicator-icons/GradeIcon";
import { Popover } from "antd";

type MonthCalendarPropsType = {
  year: number;
  month: number;
  learningDates?: learningDate[];
  onPrev?: () => void;
  onNext?: () => void;
  startDate: Date;
  endDate: Date;
};

type User = {
  id: number;
  name: string;
  profileURL: string;
};

type learningDate = {
  id: number;
  date: Date;
  className: string;
  dateOrder: string;
  unitIndex: string;
  unitTitle: string;
  location: string;
  trainer: User;
  admin: User;
};

const MonthCalendar = ({
  year,
  month,
  onPrev,
  onNext,
  learningDates = [],
  startDate,
  endDate,
}: MonthCalendarPropsType) => {
  const firstDateOfMonth = new Date(year, month - 1, 1);
  const prevDayElements = [];
  for (let i = 0; i < firstDateOfMonth.getDay(); i++) {
    prevDayElements.push(
      <div key={`prev-month-calendar-${month}/${year}/${i}`}></div>
    );
  }
  return (
    <div className="p-5 flex flex-col justify-center items-center">
      <div className="flex mb-4 justify-between w-full">
        <div>
          {onPrev && (
            <button onClick={onPrev} type="button">
              <ArrowBackIosIcon />
            </button>
          )}
        </div>
        <div className="font-bold w-[220px] border-b pb-4 text-center text-lg">
          <span>{monthAbbreviation[month - 1]} </span>
          <span>{year}</span>
        </div>
        <div>
          {onNext && (
            <button onClick={onNext}  type="button">
              <ArrowForwardIcon />
            </button>
          )}
        </div>
      </div>

      <div className="grid grid-cols-7 gap-x-6 gap-y-4 uppercase ">
        {daysOfWeek.map((dayOfWeek) => (
          <div
            className="text-center text-sm"
            key={`${month}-day-of-week-${dayOfWeek}`}
          >
            {dayOfWeek}
          </div>
        ))}
        {prevDayElements}
        {getDaysInMonth(year, month).map((day) => {
          const currentDate = new Date(year, month - 1, day);
          const today = new Date();
          const isActive: boolean =
            learningDates.length > 0 &&
            learningDates.some((learningDate) =>
              compareDate(learningDate.date, currentDate)
            );
          const isBlue: boolean = isDateBetween(currentDate, startDate, endDate) && isMondayWednesdayFriday(currentDate);

          let dateClassName = "";
          if (isBlue) {
            dateClassName += "bg-blue-700 text-white";
          } else if (isActive) {
            dateClassName += "bg-black text-white";
          }

          if (isActive) {
            const dayInfo = learningDates.find((learningDate) =>
              compareDate(learningDate.date, currentDate)
            ) as learningDate;
            // className="bg-primary text-white normal-case p-3"
            const content = (
              <div>
                <div style={{ fontWeight: "bold", marginBottom: "0.5rem" }}>
                  {dayInfo.className}
                </div>
                <div>{dayInfo.dateOrder}</div>
                <div style={{ marginBottom: "0.5rem" }}>
                  {dayInfo.unitIndex}:{" "}
                  <span style={{ fontWeight: "bold" }}>
                    {dayInfo.unitTitle}
                  </span>
                </div>
                <div
                  style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(5, minmax(0, 1fr))",
                    rowGap: "0.25rem",
                  }}
                >
                  <div
                    style={{
                      gridColumn: "span 2 / span 2",
                      display: "flex",
                      alignItems: "center",
                      gap: "0.5rem",
                    }}
                  >
                    <HomeWorkIcon style={{ width: "16px" }} />
                    Location
                  </div>
                  <div
                    style={{
                      gridColumn: "span 3 / span 3",
                      display: "flex",
                      alignItems: "center",
                    }}
                  >
                    {dayInfo.location}
                  </div>
                  <div
                    style={{
                      gridColumn: "span 2 / span 2",
                      display: "flex",
                      alignItems: "center",
                      gap: "0.5rem",
                    }}
                  >
                    <LectureIcon style={{ width: "16px" }} />
                    Trainer
                  </div>
                  <div
                    style={{
                      gridColumn: "span 3 / span 3",
                      textDecoration: "underline",
                      display: "flex",
                      alignItems: "center",
                    }}
                  >
                    <a href={dayInfo.trainer.profileURL}>
                      {dayInfo.trainer.name}
                    </a>
                  </div>
                  <div
                    style={{
                      gridColumn: "span 2 / span 2",
                      display: "flex",
                      alignItems: "center",
                      gap: "0.5rem",
                    }}
                  >
                    <GradeIcon style={{ width: "16px" }} />
                    Admin
                  </div>
                  <div
                    style={{
                      gridColumn: "span 3 / span 3",
                      textDecoration: "underline",
                      display: "flex",
                      alignItems: "center",
                    }}
                  >
                    <a href={dayInfo.admin.profileURL}>{dayInfo.admin.name}</a>
                  </div>
                </div>
              </div>
            );
            // key={`month-calendar-${year}/${month}/${day}`}
            return (
              <Popover
                content={content}
                key={`month-calendar-${year}/${month}/${day}`}
                trigger="hover"
              >
                <div
                  className={`font-bold rounded-full aspect-square flex justify-center items-center ${dateClassName}`}
                >
                  {day}
                </div>
              </Popover>
            );
          } else {
            return (
              <div
                key={`month-calendar-${year}/${month}/${day}`}
                className={`font-bold rounded-full aspect-square flex justify-center items-center ${dateClassName}`}
              >
                {day}
              </div>
            );
          }
        })}
      </div>
    </div>
  );
};

type MonthPropsType = {
  learningDates?: learningDate[];
  setDate?: () => Date;
  startDate: Date;
  endDate: Date;
};

const Month = ({ learningDates = [], startDate, endDate }: MonthPropsType) => {
  const currentDate = new Date();
  const [displayingMonth, setDisplayingMonth] = useState<number>(
    currentDate.getMonth() + 1
  );
  const [displayingYear, setDisplayingYear] = useState<number>(
    currentDate.getFullYear()
  );
  const nextMonth = displayingMonth !== 12 ? displayingMonth + 1 : 1;
  const nextMonthYear =
    displayingMonth !== 12 ? displayingYear : displayingYear + 1;
  return (
    <div className="shadow-[5px_10px_20px_rgba(0,0,0,0.2)] p-3 flex gap-5 rounded-lg items-start justify-center select-none">
      <MonthCalendar
        year={displayingYear}
        month={displayingMonth}
        learningDates={learningDates}
        onPrev={() => {
          setDisplayingMonth((prev) => {
            if (prev === 1) {
              setDisplayingYear(displayingYear - 1);
              return 12;
            }
            return prev - 1;
          });
        }}
        startDate={startDate}
        endDate={endDate}
      />
      <MonthCalendar
        year={nextMonthYear}
        month={nextMonth}
        learningDates={learningDates}
        onNext={() => {
          setDisplayingMonth((prev) => {
            if (prev === 12) {
              setDisplayingYear(displayingYear + 1);
              return 1;
            }
            return prev + 1;
          });
        }}
        startDate={startDate}
        endDate={endDate}
      />
    </div>
  );
};

export default Month;
