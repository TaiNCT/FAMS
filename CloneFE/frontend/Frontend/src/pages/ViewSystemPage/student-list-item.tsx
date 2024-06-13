import React, { useEffect, useState } from "react";
import Popover from "@mui/material/Popover";
import OptionPopUp from "./OptionPopUp/option-pop-up";
import dayjs from "dayjs";
import { studentApi } from "../../config/axios.ts";
import "./student-list-item.css"

type StudentProp = {
    Student: any;
    displayedColumns: string[];
    onCheckboxChange: (studentId: string, checked: boolean) => void;
    selectedIds: string[];
}

const StudentListItem: React.FC<StudentProp> = ({
    Student,
    displayedColumns,
    onCheckboxChange,
    selectedIds
}) => {
    const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(null);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);
    const id = open ? "simple-popover" : undefined;

    const [Major, setMajor] = useState<string>("");
    const fetchBlogData = async () => {
        try {
            const response = await studentApi.get(`/GetMajor/${Student.studentInfoDTO.majorId}`);
            const majorName = response.data.result[0]?.name || '';
            setMajor(majorName);
        } catch (error) {
            console.error("Error fetching major data:", error);
        }
    };
    useEffect(() => {
        fetchBlogData();
    }, [Student]);

    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>, studentId: string) => {
        onCheckboxChange(studentId, event.target.checked);
    };

    return (
        <>
            <tr>
                <td>
                    <input
                        type="checkbox"
                        onChange={(event) => handleCheckboxChange(event, Student.studentInfoDTO.studentId)}
                        checked={selectedIds.includes(Student.studentInfoDTO.studentId)}
                    />
                </td>
                {displayedColumns.includes("Full name") && (
                    <td>{Student.studentInfoDTO.fullName}</td>
                )}
                {displayedColumns.includes("Date of birth") && (
                    <td>{new Date(Student.studentInfoDTO.dob).toLocaleDateString()}</td>
                )}
                {displayedColumns.includes("Gender") && (
                    <td>{Student.studentInfoDTO.gender}</td>
                )}
                {displayedColumns.includes("Phone") && (
                    <td>{Student.studentInfoDTO.phone}</td>
                )}
                {displayedColumns.includes("Email") && (
                    <td>{Student.studentInfoDTO.email}</td>
                )}
                {displayedColumns.includes("University") && (
                    <td>{Student.studentInfoDTO.university}</td>
                )}
                {displayedColumns.includes("Major") && (
                    <td>{Major}</td>
                )}
                {displayedColumns.includes("Graduation Time") && (
                    <td>{dayjs(Student.studentInfoDTO.graduatedDate).format("DD/MM/YYYY")}</td>
                )}
                {displayedColumns.includes("GPA") && (
                    <td>{Student.studentInfoDTO.gpa}</td>
                )}
                {displayedColumns.includes("Address") && (
                    <td>{Student.studentInfoDTO.address}</td>
                )}
                {displayedColumns.includes("RECer") && (
                    <td>{Student.studentInfoDTO.recer}</td>
                )}
                {displayedColumns.includes("Status") && (
                    <td className="status-column"><span className={`${Student.studentInfoDTO.status}`}>{Student.studentInfoDTO.status}</span></td>
                )}
                <td>
                    <button aria-describedby={id} onClick={handleClick}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                            <path d="M13 15C14.1046 15 15 14.1046 15 13C15 11.8954 14.1046 11 13 11C11.8954 11 11 11.8954 11 13C11 14.1046 11.8954 15 13 15Z" fill="#2D3748" />
                            <path d="M20 15C21.1046 15 22 14.1046 22 13C22 11.8954 21.1046 11 20 11C18.8954 11 18 11.8954 18 13C18 14.1046 18.8954 15 20 15Z" fill="#2D3748" />
                            <path d="M6 15C7.10457 15 8 14.1046 8 13C8 11.8954 7.10457 11 6 11C4.89543 11 4 11.8954 4 13C4 14.1046 4.89543 15 6 15Z" fill="#2D3748" />
                        </svg>
                    </button>
                    <Popover
                        id={id}
                        open={open}
                        anchorEl={anchorEl}
                        onClose={handleClose}
                        anchorOrigin={{
                            vertical: 'bottom',
                            horizontal: 'left',
                        }}
                    >
                        <OptionPopUp Student={Student} />
                    </Popover>
                </td>
            </tr>
        </>
    );
};

export default StudentListItem;
