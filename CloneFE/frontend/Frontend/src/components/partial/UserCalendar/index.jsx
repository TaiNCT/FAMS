import React, { useState, useEffect } from "react";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import axios from "axios";
import style from "./style.module.scss";
import { Bounce, toast } from "react-toastify";
import GlobalLoading from "../../global/GlobalLoading";

const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
const localizer = momentLocalizer(moment);

const UserCalendar = () => {
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const username = localStorage.getItem("username");

  useEffect(() => {
    const fetchCalendarData = async () => {
      try {
        setIsLoading(true);
        const response = await axios.get(
          `${backend_api}/api/get-user-calendar?username=${username}`
        );
        const calendarData = response.data.result;
        const formattedEvents = calendarData.map((event) => ({
          start: new Date(`${event.startDate} ${event.startTime}`),
          end: new Date(`${event.endDate} ${event.endTime}`),
          title: event.className,
          location: event.address,
          instructor: event.fsuName,
        }));
        setEvents(formattedEvents);
      } catch (error) {
        toast.error("There is error while getting data.", {
          position: "top-right",
          autoClose: 3000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
      }
      finally {
        setIsLoading(false);
      }
    };
    fetchCalendarData();
  }, [username]);

  const eventStyleGetter = (event, start, end, isSelected) => {
    const style = {
      backgroundColor: "#3174ad",
      borderRadius: "0px",
      opacity: 0.8,
      color: "white",
      border: "0px",
      display: "block",
    };
    return { style };
  };

  return (
    <div className={style.calendar}>
      <GlobalLoading isLoading={isLoading}/>
      <Calendar
        localizer={localizer}
        events={events}
        startAccessor="start"
        endAccessor="end"
        style={{ height: "2000px" }}
        eventPropGetter={eventStyleGetter}
        views={["month"]}
        components={{
          event: ({ event }) => (
            <div>
              <strong>{event.title}</strong>
              <p>Location: {event.location}</p>
              <p>Instructor: {event.instructor}</p>
            </div>
          ),
        }}
      />
    </div>
  );
};

export default UserCalendar;