import * as React from 'react';
import Calendar from 'react-calendar';
import '../DateTimePicker/Calendar.css'

export default function CalendarPickDay({ value, onChange })
{
	const [selectedDate, setSelectedDate] = React.useState(value);

	const handleDateChange = (value) =>
	{
		setSelectedDate(value);
		onChange(value);
	};

	return (
		<Calendar
			onChange={handleDateChange}
			value={selectedDate}
			tileClassName={({ date, view }) =>
				view === 'month' && date.getTime() === selectedDate.getTime() ? 'react-calendar__tile--active' : ''}
			prev2Label={null}
			next2Label={null}
		/>
	);
}
