import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import style from "../../../../assert/css/AttendeeCalendarWeek.module.css";
import { AttendeeTypeList } from "../../../ClassList/api/ListApi";
import GlobalLoading from "../../../../../../global/GlobalLoading";

export default function TableCalendarWeek({ data, setData, value, startOfWeek, endOfWeek })
{
    const [attendeeTypes, setAttendeeTypes] = React.useState([]);
    const [classIDs, setClassIDs] = React.useState([]);
    const [isLoading, setIsLoading] = React.useState(true);
    React.useEffect(() =>
    {
        AttendeeTypeList()
            .then(data => setAttendeeTypes(data))
            .catch(error => console.error('Error:', error));
    }, []);
    React.useEffect(() => {
      const timer = setTimeout(() => {
        setIsLoading(false);
      }, 600);
  
      return () => clearTimeout(timer);
    }, []);
    return (
        <>
        <GlobalLoading isLoading={(isLoading )}/>
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} aria-label="simple table"
                size="medium" style={style.table}>
                <TableHead>
                    <TableRow>
                        <TableCell
                            colSpan={14}
                            style={{
                                padding: "10px",
                                color: "white",
                                textAlign: "left",
                                background: "#2d3748",
                                borderRadius: "10px",
                            }}
                        >
                            Morning (8:00 - 12:00)
                        </TableCell>
                    </TableRow>
                </TableHead>
                {startOfWeek && endOfWeek && data.length > 0 && (
                    <TableBody sx={{
                        "&:last-child td, &:last-child tr": {
                            borderBottom: 1,
                            borderColor: "#2d3748",
                        },
                    }}>
                        <TableRow >
                            {['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'].map((day) =>
                            {

                                if (day === 'Sunday' || day === 'Saturday')
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                        </React.Fragment>
                                    );
                                }


                                const dayData = data.find((item) => item.dayOfWeek === day);

                                if (!dayData)
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                            <TableCell sx={{ width: "4px" }}>
                                                <span>|</span>
                                            </TableCell>
                                        </React.Fragment>
                                    );
                                }

                                const classesForDay = dayData.classOfWeek.filter((classItem) =>
                                {
                                    const startHour = classItem.startTime ? parseInt(classItem.startTime.split(':')[0]) : null;
                                    const startsAfterEight = startHour >= 8;

                                    return startsAfterEight;
                                }).sort((a, b) =>
                                {
                                    // Convert start times to Date objects
                                    const aStartTime = new Date(`1970-01-01T${a.startTime}Z`);
                                    const bStartTime = new Date(`1970-01-01T${b.startTime}Z`);

                                    // Compare the start times
                                    return aStartTime - bStartTime;
                                });

                                return (
                                    <React.Fragment key={day}>
                                        <TableCell sx={{ verticalAlign: "top", width: "14.29%" }} align="left">
                                            <div style={{ display: "flex", flexDirection: "column", width: "15%" }}>
                                                {classesForDay.map((classItem, i) =>
                                                {
                                                    const attendeeType = attendeeTypes.find(type => classItem.attendeeLevelId === type.attendeeTypeId);
                                                    let styleName = '';
                                                    if (attendeeType)
                                                    {
                                                        switch (attendeeType.attendeeTypeName)
                                                        {
                                                            case 'Intern':
                                                                styleName = style.intern;
                                                                break;
                                                            case 'Fresher':
                                                                styleName = style.fresher;
                                                                break;
                                                            case 'Online fee-fresher':
                                                                styleName = style.onlineFresher;
                                                                break;
                                                            case 'Offline fee-fresher':
                                                                styleName = style.offFresher;
                                                                break;
                                                            default:
                                                                styleName = '';
                                                        }
                                                    }
                                                    return (
                                                        <React.Fragment key={i}>
                                                            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", width: "100%" }}>
                                                                <div className={styleName} style={{ marginBottom: "10px" }}>
                                                                    {classItem.classCode}
                                                                </div>
                                                                <div style={{ marginLeft: "10px" }}>
                                                                    <span>|</span>
                                                                </div>
                                                            </div>
                                                        </React.Fragment>
                                                    );
                                                })}
                                            </div>
                                        </TableCell>
                                    </React.Fragment>
                                );
                            })}
                        </TableRow>
                    </TableBody>
                )}
            </Table>

            <Table sx={{ minWidth: 700 }} aria-label="simple table"
                size="medium" style={style.table}>
                <TableHead>
                    <TableRow>
                        <TableCell
                            colSpan={14}
                            style={{
                                padding: "10px",
                                color: "white",
                                textAlign: "left",
                                background: "#2d3748",
                                borderRadius: "10px",
                            }}
                        >
                            Noon (13:00 - 17:00)
                        </TableCell>
                    </TableRow>
                </TableHead>
                {startOfWeek && endOfWeek && data.length > 0 && (
                    <TableBody sx={{
                        "&:last-child td, &:last-child tr": {
                            borderBottom: 1,
                            borderColor: "#2d3748",
                        },
                    }}>
                        <TableRow>
                            {['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'].map((day) =>
                            {
                                if (day === 'Sunday' || day === 'Saturday')
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                            <TableCell sx={{ width: "4px" }}>
                                                <span>|</span>
                                            </TableCell>
                                        </React.Fragment>
                                    );
                                }


                                const dayData = data.find((item) => item.dayOfWeek === day);

                                if (!dayData)
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                            <TableCell sx={{ width: "4px" }}>
                                                <span>|</span>
                                            </TableCell>
                                        </React.Fragment>
                                    );
                                }

                                const classesForDay = dayData.classOfWeek.filter((classItem) =>
                                {
                                    const startHour = classItem.startTime ? parseInt(classItem.startTime.split(':')[0]) : null;
                                    const startsAfter13 = startHour >= 13;

                                    return startsAfter13;
                                }).sort((a, b) =>
                                {
                                    // Convert start times to Date objects
                                    const aStartTime = new Date(`1970-01-01T${a.startTime}Z`);
                                    const bStartTime = new Date(`1970-01-01T${b.startTime}Z`);

                                    // Compare the start times
                                    return aStartTime - bStartTime;
                                });

                                return (
                                    <React.Fragment key={day}>
                                        <TableCell sx={{ verticalAlign: "top", width: "14.29%" }} align="left">
                                            <div style={{ display: "flex", flexDirection: "column" }}>
                                                {classesForDay.map((classItem, i) =>
                                                {
                                                    const attendeeType = attendeeTypes.find(type => classItem.attendeeLevelId === type.attendeeTypeId);
                                                    let styleName = '';
                                                    if (attendeeType)
                                                    {
                                                        switch (attendeeType.attendeeTypeName)
                                                        {
                                                            case 'Intern':
                                                                styleName = style.test;
                                                                break;
                                                            case 'Fresher':
                                                                styleName = style.fresher;
                                                                break;
                                                            case 'Online fee-fresher':
                                                                styleName = style.onlineFresher;
                                                                break;
                                                            case 'Offline fee-fresher':
                                                                styleName = style.offFresher;
                                                                break;
                                                            default:
                                                                styleName = '';
                                                        }
                                                    }
                                                    return (
                                                        <React.Fragment key={i}>
                                                            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", width: "100%" }}>
                                                                <div className={styleName} style={{ marginBottom: "10px" }}>
                                                                    {classItem.classCode}
                                                                </div>
                                                                <div>
                                                                    <span>|</span>
                                                                </div>
                                                            </div>
                                                        </React.Fragment>
                                                    );
                                                })}
                                            </div>
                                        </TableCell>
                                    </React.Fragment>
                                );
                            })}
                        </TableRow>
                    </TableBody>
                )}
            </Table>

            <Table sx={{ minWidth: 700 }} aria-label="simple table"
                size="medium" style={style.table}>
                <TableHead>
                    <TableRow>
                        <TableCell
                            colSpan={14}
                            style={{
                                padding: "10px",
                                color: "white",
                                textAlign: "left",
                                background: "#2d3748",
                                borderRadius: "10px",
                            }}
                        >
                            Night (18:00 - 22:00)
                        </TableCell>
                    </TableRow>
                </TableHead>
                {startOfWeek && endOfWeek && data.length > 0 && (
                    <TableBody sx={{
                        "&:last-child td, &:last-child tr": {
                            borderBottom: 1,
                            borderColor: "#2d3748",
                        },
                    }}>
                        <TableRow>
                            {['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'].map((day) =>
                            {
                                if (day === 'Sunday' || day === 'Saturday')
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                            <TableCell sx={{ width: "4px" }}>
                                                <span>|</span>
                                            </TableCell>
                                        </React.Fragment>
                                    );
                                }


                                const dayData = data.find((item) => item.dayOfWeek === day);

                                if (!dayData)
                                {
                                    return (
                                        <React.Fragment key={day}>
                                            <TableCell sx={{ width: "14.29%" }} align="left"></TableCell>
                                            <TableCell sx={{ width: "4px" }}>
                                                <span>|</span>
                                            </TableCell>
                                        </React.Fragment>
                                    );
                                }

                                // Filter the classes for the current day and time
                                const classesForDay = dayData.classOfWeek.filter((classItem) =>
                                {
                                    // Check if the class is between 18 
                                    const startHour = classItem.startTime ? parseInt(classItem.startTime.split(':')[0]) : null;
                                    const startsAfter18 = startHour >= 18;

                                    return startsAfter18;
                                }).sort((a, b) =>
                                {
                                    // Convert start times to Date objects
                                    const aStartTime = new Date(`1970-01-01T${a.startTime}Z`);
                                    const bStartTime = new Date(`1970-01-01T${b.startTime}Z`);

                                    // Compare the start times
                                    return aStartTime - bStartTime;
                                });

                                return (
                                    <React.Fragment key={day}>
                                        <TableCell sx={{ verticalAlign: "top", width: "14.29%" }} align="left">
                                            <div style={{ display: "flex", flexDirection: "column" }}>
                                                {classesForDay.map((classItem, i) =>
                                                {
                                                    const attendeeType = attendeeTypes.find(type => classItem.attendeeLevelId === type.attendeeTypeId);
                                                    let styleName = '';
                                                    if (attendeeType)
                                                    {
                                                        switch (attendeeType.attendeeTypeName)
                                                        {
                                                            case 'Intern':
                                                                styleName = style.test;
                                                                break;
                                                            case 'Fresher':
                                                                styleName = style.fresher;
                                                                break;
                                                            case 'Online fee-fresher':
                                                                styleName = style.onlineFresher;
                                                                break;
                                                            case 'Offline fee-fresher':
                                                                styleName = style.offFresher;
                                                                break;
                                                            default:
                                                                styleName = '';
                                                        }
                                                    }
                                                    return (
                                                        <React.Fragment key={i}>
                                                            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", width: "100%" }}>
                                                                <div className={styleName} style={{ marginBottom: "10px" }}>
                                                                    {classItem.classCode}
                                                                </div>
                                                                <div style={{ marginLeft: "10px" }}>
                                                                    <span>|</span>
                                                                </div>
                                                            </div>
                                                        </React.Fragment>
                                                    );
                                                })}
                                            </div>
                                        </TableCell>
                                    </React.Fragment>
                                );
                            })}
                        </TableRow>
                    </TableBody>
                )}
            </Table>
        </TableContainer>
        </>
    )
}