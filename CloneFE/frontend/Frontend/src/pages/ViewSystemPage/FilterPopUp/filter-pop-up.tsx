import React, { ChangeEvent, useState } from "react";
import "./filter-pop-up.css";
import { Checkbox } from "@mui/material";
import { DesktopDatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs, { Dayjs } from "dayjs";
import { useSearchParams } from "react-router-dom";

const FilterPopUp: React.FC = () => {

    const [search, setSearch] = useSearchParams();
    //Get filters value from url
    const filteredDob = dayjs(search.get("dob"), "M-D-YYYY");
    const filteredGender = search.get("gender")?.split(",") ?? [];
    const filteredStatus = search.get("status")?.split(",") ?? [];
    const [filters, setFilters] = useState({
        gender: filteredGender,
        dob: filteredDob,
        status: filteredStatus,
    });

    //Functions Handle events
    function handleChangeDate(newDate: Dayjs) {
        setFilters({
            ...filters,
            dob: newDate,
        });
    }

    type handleChangeEvent = (newchoiceArray: string[]) => void;

    function handleChangeGender(newGenderArray: string[]) {
        setFilters({
            ...filters,
            gender: newGenderArray,
        });
    }

    function handleChangeStatus(newStatusArray: string[]) {
        setFilters({
            ...filters,
            status: newStatusArray,
        });
    }

    function handleChangeCheckbox(
        choice: string[],
        setchoice: handleChangeEvent,
        event: ChangeEvent<HTMLInputElement>
    ) {
        if (event.target.checked) {
            setchoice([...choice, event.target.value]);
        } else {
            const newArray = choice.filter((value) => value !== event.target.value);
            setchoice(newArray);
        }
    }

    function handleApplyClick() {
        //Set dob to search params
        const formattedDate = filters.dob.isValid() ? filters.dob.format("YYYY-MM-DD") : "";
        search.set("dob", formattedDate);
        //Set gender to search params
        filters.gender.length > 0
            ? search.set("gender", filters.gender.join(","))
            : search.delete("gender");
        //Set status to serach params
        filters.status.length > 0
            ? search.set("status", filters.status.join(","))
            : search.delete("status");

        setSearch(search, { replace: true });
    }

    function handleResetClick() {
        search.delete("status");
        search.delete("gender");
        setSearch(search, { replace: true });
        setFilters({ dob: dayjs(null), gender: [], status: [] });
    }

    return (
        <div className="filter-pop-up-container">
            <h3>Date Of Birth</h3>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DesktopDatePicker
                    value={filters.dob}
                    onChange={(newDate) => handleChangeDate(newDate!)}
                />
            </LocalizationProvider>
            <div className="filter-pop-up-action">
                <div className="filter-pop-up-gender">
                    <h3>Gender</h3>
                    <div className="gender-check-box-container">
                        <Checkbox
                            onChange={(event) =>
                                handleChangeCheckbox(
                                    filters.gender,
                                    handleChangeGender,
                                    event
                                )
                            }
                            checked={filters.gender.includes("Male")}
                            value={"Male"}
                        />
                        <span>Male</span>
                        <Checkbox
                            onChange={(event) =>
                                handleChangeCheckbox(
                                    filters.gender,
                                    handleChangeGender,
                                    event
                                )
                            }
                            checked={filters.gender.includes("Female")}
                            value={"Female"}
                        />
                        <span>Female</span>
                    </div>
                </div>
                <div className="filter-pop-up-status">
                    <h3>Status</h3>
                    <div className="status-check-box-container">
                        <Checkbox
                            onChange={(event) =>
                                handleChangeCheckbox(
                                    filters.status,
                                    handleChangeStatus,
                                    event
                                )
                            }
                            checked={filters.status.includes("InActive")}
                            value={"InActive"}
                        />
                        <span>Inactive</span>
                        <Checkbox
                            onChange={(event) =>
                                handleChangeCheckbox(
                                    filters.status,
                                    handleChangeStatus,
                                    event
                                )
                            }
                            checked={filters.status.includes("Active")}
                            value={"Active"}
                        />
                        <span>Active</span>
                    </div>
                </div>
            </div>
            <div className="filter-pop-up-button">
                <button onClick={() => handleApplyClick()}>Apply</button>
                <button onClick={handleResetClick}>Reset</button>
            </div>
        </div>
    );
};

export default FilterPopUp;
