import * as React from "react";
import Popover from "@mui/material/Popover";
import ButtonUtil from "../../../ClassList/components/Button";
import StyleSCss from "../../../../assert/css/FilterStyle.module.scss";
import FilterListIcon from "@mui/icons-material/FilterList";
import MultipleSelectChip from "../../../ClassList/components/SelectLocaion";
import BasicDateTimePicker from "../../../ClassList/components/DateTimePicker";
import style from "../../../../assert/css/DateTime.module.scss";
import CheckboxLabels from "../../../ClassList/components/Checkbox";
import BasicSelect from "../../../ClassList/components/BasicSelect";
import buttonStyle2 from "../../../../assert/css/Button2.module.scss";
import buttonStyle3 from "../../../../assert/css/Button3.module.scss";
import { fetchFilteredData, fetchFilteredDataWeek } from "../../../ClassList/api/FilterApi";
import { FsuListApi, TrainingProgramListAPI, LocationList } from "../../../ClassList/api/ListApi";

export default function FilterPopOver({ fetchData, anchorEl, setAnchorEl, onFilteredData, setIsFiltered, setFilteredList }) 
{
    const open = Boolean(anchorEl);
    const id = open ? "filter-popover" : undefined;
    const labels = ["Plaining", "Scheduled", "Opening"];
    const Attendeelabels = ["Intern", "Fresher", "Online fee-fresher", "Offline fee-fresher"];
    const SlotTimeLabel = ["Morning", "Noon", "Night", "Online"];
    const [fsuList, setFsuList] = React.useState([]);
    const [programlist, setProgramList] = React.useState([]);
    const [locationList, setlocationList] = React.useState([]);
    const [selectedFSU, setSelectedFSU] = React.useState(null);
    const [selectedProgram, setSelectedProgram] = React.useState('');
    const [selectedStartDate, setSelectedStartDate] = React.useState(null);
    const [selectedEndDate, setSelectedEndDate] = React.useState(null);
    const [selectedLocationIds, setselectedLocationIds] = React.useState([]);

    const [checkedBoxStatus, setCheckedBoxStatus] = React.useState(
        labels.reduce((acc, label) => ({ ...acc, [label]: false }), {})
    );
    const [checkedBoxAttendee, setcheckedBoxAttendee] = React.useState(
        Attendeelabels.reduce((acc, label) => ({ ...acc, [label]: false }), {})
    );

    const [checkedBoxTime, setcheckedBoxTime] = React.useState(
        SlotTimeLabel.reduce((acc, label) => ({ ...acc, [label]: false }), {})
    );

    const handleFilterSubmit = async () =>
    {
        // Ensure slotTimes is an array
        let slotTimes = Object.keys(checkedBoxTime).filter(key => checkedBoxTime[key]);
        if (typeof slotTimes === 'string')
        {
            slotTimes = JSON.parse(slotTimes);
        } else if (!slotTimes)
        {
            slotTimes = [];
        }

        const data = await fetchFilteredData(checkedBoxStatus, selectedFSU, selectedProgram, checkedBoxAttendee, selectedLocationIds, slotTimes, selectedStartDate, selectedEndDate);
        setIsFiltered(true);
        onFilteredData(data);
        handleClose();
        setFilteredList(data.data.items);
    };

    const fetchApiData = async () =>
    {
        const fsuData = await FsuListApi();
        const trainingProgrsmListData = await TrainingProgramListAPI();
        const locationData = await LocationList();

        setFsuList(fsuData);
        setProgramList(trainingProgrsmListData);
        setlocationList(locationData);
    }

    React.useEffect(() =>
    {
        if (open)
        {
            fetchApiData();
        }
    }, [open]);

    const handleClose = () =>
    {
        setAnchorEl(null);
    };

    const clearAll = () =>
    {
        setSelectedStartDate(null);
        setSelectedEndDate(null);
        setselectedLocationIds([]);
        setCheckedBoxStatus(labels.reduce((acc, label) => ({ ...acc, [label]: false }), {}));
        setcheckedBoxAttendee(Attendeelabels.reduce((acc, label) => ({ ...acc, [label]: false }), {}));
        setcheckedBoxTime(SlotTimeLabel.reduce((acc, label) => ({ ...acc, [label]: false }), {}));
        setSelectedFSU(null);
        setSelectedProgram('');

        //Fetch again
        fetchData();
        setIsFiltered(false);

        //close
        handleClose();
    };


    return (
        <React.Fragment>
            <Popover
                id={id}
                open={open}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: "bottom",
                    horizontal: "left",
                }}
                sx={{ ".MuiPaper-root": { borderRadius: "20px" } }}
            >
                <div className={StyleSCss.form_body}>
                    <form action="">
                        <section>
                            <div style={{ display: "flex" }}>
                                {/* <div style={{width: "100px", paddingBottom: "0px"}}>
                              <ButtonUtil name="Clear" icon={<FilterListIcon />} style={buttonStyle} onClick={handleClose}/>
                          </div> */}
                                <MultipleSelectChip selectedItems={selectedLocationIds} setSelectedItems={setselectedLocationIds} locationList={locationList} />
                                <div
                                    className="datetime_picker"
                                    style={{ display: "flex", marginLeft: "10px", width: "50%" }}
                                >
                                    <div className="fromDate" style={{ width: "100%" }}>
                                        <p className={StyleSCss.fromDate_title}
                                        >
                                            Class Time Frame
                                        </p>
                                        <div className={style.DateTimeContainer}>
                                            <BasicDateTimePicker
                                                selectedStartDate={selectedStartDate}
                                                setSelectedStartDate={setSelectedStartDate}
                                                selectedEndDate={selectedEndDate}
                                                setSelectedEndDate={setSelectedEndDate}
                                            />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>

                        <section style={{ display: "flex" }}>

                            <div style={{ display: "flex" }}>
                                <p className={StyleSCss.checkbox_title}>Class Time</p>
                                <div className={StyleSCss.checkbox_classtime}>
                                    <CheckboxLabels labels={SlotTimeLabel} checkedBox={checkedBoxTime} setCheckedBox={setcheckedBoxTime} />
                                </div>
                            </div>
                            <div style={{ display: "flex", marginLeft: "20px" }}>
                                <p className={StyleSCss.checkbox_title}>Status</p>
                                <div className={StyleSCss.checkbox_classtime}>
                                    <CheckboxLabels labels={labels} checkedBox={checkedBoxStatus} setCheckedBox={setCheckedBoxStatus} />
                                </div>
                            </div>

                            <div style={{ display: "flex", marginLeft: "20px" }}>
                                <p className={StyleSCss.checkbox_title}>Attendee</p>
                                <div className={StyleSCss.checkbox_classtime}>
                                    <CheckboxLabels labels={Attendeelabels} checkedBox={checkedBoxAttendee} setCheckedBox={setcheckedBoxAttendee} />
                                </div>
                            </div>
                        </section>

                        <section style={{ marginTop: "10px", display: "flex" }}>

                            <div style={{ display: "flex" }}>
                                <p className={StyleSCss.selectName} style={{ marginTop: "10px" }}>FSU</p>
                                <BasicSelect items={fsuList} selectedItem={selectedFSU} setSelectedItem={setSelectedFSU} valueKey="id" />
                            </div>

                            <div style={{ display: "flex", marginLeft: "120px" }}>
                                <p className={StyleSCss.selectName} style={{ marginTop: "10px" }}>Trainer</p>
                                <BasicSelect items={programlist} selectedItem={selectedProgram} setSelectedItem={setSelectedProgram} valueKey="trainingProgramCode" />
                            </div>

                        </section>

                        <section>
                            <div style={{ display: "flex", justifyContent: "end", alignContent: "center" }}>
                                <div style={{ marginRight: "20px" }}>
                                    <ButtonUtil name="Clear" style={buttonStyle2} onClick={clearAll} />
                                </div>
                                <div>
                                    <ButtonUtil name="Search" style={buttonStyle3} onClick={handleFilterSubmit} />
                                </div>
                            </div>
                        </section>
                    </form>
                </div>
            </Popover>
        </React.Fragment>
    );
}
