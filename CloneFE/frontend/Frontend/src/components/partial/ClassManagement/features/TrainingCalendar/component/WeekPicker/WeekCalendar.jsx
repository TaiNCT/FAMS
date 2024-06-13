import * as React from "react";
import Calendar from 'react-calendar';
import '../DateTimePicker/Calendar.css';

const getMonday = (d) =>
{
    const day = d.getDay();
    const diff = d.getDate() - day + (day === 0 ? -6 : 1);
    return new Date(d.setDate(diff));
};


const getSunday = (d) =>
{
    const day = d.getDay();
    const diff = d.getDate() - day + (day === 0 ? -6 : 7);
    return new Date(d.setDate(diff));
};

function isSameWeek(date1, date2)
{

    const sameWeek = getMonday(new Date(date1)).getTime() === getMonday(new Date(date2)).getTime();

    return sameWeek;
}

const WeekCalendar = ({ value, onChange, onWeekChange, onMonthChange, setStartOfWeek, setEndOfWeek }) =>
{
    const [activeStartDate, setActiveStartDate] = React.useState(new Date());

    const handleChange = (value) =>
    {
        if (value)
        {
            const startOfWeek = getMonday(new Date(value));
            const endOfWeek = getSunday(new Date(value));
            onChange(value);
            onWeekChange(startOfWeek, endOfWeek);
        } else
        {
            onChange(value);
            setStartOfWeek(null);
            setEndOfWeek(null);
        }
    };

    const handleActiveStartDateChange = ({ activeStartDate }) =>
    {
        setActiveStartDate(activeStartDate);
        onMonthChange();
        handleChange(null);
    };

    return (
        <Calendar onChange={handleChange}
            key={value ? 'week-view' : 'day-view'}
            onActiveStartDateChange={handleActiveStartDateChange}
            activeStartDate={activeStartDate}
            value={value}
            prev2Label={null}
            next2Label={null}
            tileClassName={({ date, view }) =>
            {
                if (view === 'month')
                {
                    if (isSameWeek(date, value))
                    {
                        if (date.getMonth() !== value.getMonth())
                        {
                            return 'highlight-red';
                        }
                        return 'highlight';
                    }
                    return value ? 'irrelevant' : null;
                }
                return null;
            }}
            tileDisabled={({ date, view }) =>
                view === 'month' && isSameWeek(date, value) && date.getMonth() === value.getMonth()
            }
            locale="en-US"
        />
    );
}

export default WeekCalendar

