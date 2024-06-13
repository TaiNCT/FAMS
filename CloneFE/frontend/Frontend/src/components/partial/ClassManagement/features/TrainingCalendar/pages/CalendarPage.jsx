import React, { useEffect, useState } from "react";
import TableCalendar from "../component/TableCalendar/TableCalendar";
import Header from "../../../features/TrainingCalendar/component/Header";
import Container from "../../ClassList/components/Container";
import Search_Input from "../../../components/Form-Control/Search_Input";
import ButtonUtil from "../../ClassList/components/Button";
import FilterPopOver from "../component/FilterOption/FilterPopOver";
import buttonStyle from "../../../assert/css/Button.module.scss";
import FilterListIcon from "@mui/icons-material/FilterList";
import Choosen from "../component/Choosen";
import ButtonChoose from "../component/ButtonChoose";
import Button from "@mui/material/Button";
import dayjs from "dayjs";
import FormatDate from "../../../Utils/FormatDate";
import CalendarPickDay from "../component/DateTimePicker/DateTimePicker_Day";
import TableCalendarWeek from "../component/TableCalendar/TableCandarWeek";
import WeekCalendar from "../component/WeekPicker/WeekCalendar";
import { GetClass, GetClassesByWeek } from "../../ClassList/api/ListApi";
import SearchKey from "../component/SearchKey";
import { debounce, set } from "lodash";
import FilterWeek from "../component/FilterOption/FilterWeek";
const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};
export default function CalendarPage() {
  const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
  const [value, setValue] = React.useState(new Date());
  const [data, setData] = React.useState([]);
  const [day, setDay] = React.useState(true);
  const [week, setWeek] = React.useState(false);
  const [weekValue, setWeekValue] = React.useState(null);
  const [startOfWeek, setStartOfWeek] = React.useState(null);
  const [endOfWeek, setEndOfWeek] = React.useState(null);
  const [classesByWeek, setClassesByWeek] = React.useState([]);
  const [totalItems, setTotalItems] = React.useState(0);
  const [search, setSearch] = React.useState(false);
  const [searchKey, setSearchKey] = useState([]);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [filterData, setFilteredData] = React.useState([]);
  const [isFiltered, setIsFiltered] = React.useState(false);
  const [filteredDayList, setFilteredDayList] = React.useState([]);
  const [filteredWeekList, setfilteredWeekList] = React.useState([]);

  const handleDayFilterData = (data) => {
    setTotalItems(data.data.totalCount);
    setIsFiltered(true);
    setSearch(false);
    setFilteredDayList(data.data.items);
  };

  const handleWeekFilterData = (data) => {
    setIsFiltered(true);
    setSearch(false);
    setfilteredWeekList(data.data.items);
  };

  const handleChoice = (data) => {
    if (data == 1) {
      setDay(true);
      setWeek(false);
      setValue(new Date());
      // Reset week filters
      setWeekValue(null);
      setStartOfWeek(null);
      setEndOfWeek(null);
      setClassesByWeek([]);
      //Reset week list
      setfilteredWeekList([]);
    }

    if (data == 2) {
      setWeek(true);
      setDay(false);
      // Reset day filters
      setValue(null);
      setData([]);
      setIsFiltered(false);
      //Reset day list
      setFilteredDayList([]);

      // Reset week filters
      setWeekValue(null);
      setStartOfWeek(null);
      setEndOfWeek(null);
      setClassesByWeek([]);
      //Reset week list
      setfilteredWeekList([]);
    }
  };

  const fetchData = async () => {
    const response = await GetClass();
    const filteredItems = response.filter(
      (item) =>
        item.startDate <= FormatDate(dayjs(value)) &&
        item.endDate >= FormatDate(dayjs(value))
    );
    setData(filteredItems);
    setFilteredData(response);
  };

  const fetchFilteredData = async () => {
    const filteredItems = filteredDayList.filter(
      (item) =>
        item.startDate <= FormatDate(dayjs(value)) &&
        item.endDate >= FormatDate(dayjs(value))
    );
    setData(filteredItems);
    setFilteredData(filteredItems);
  };

  useEffect(() => {
    if (!week) {
      if (!isFiltered) {
        fetchData();
      } else {
        fetchFilteredData();
      }
    }
  }, [value, isFiltered, filteredDayList]);

  useEffect(() => {
    if (searchKey.length > 0) {
      const searchKeyQueryString = searchKey
        .map((key) => `searchKeys=${encodeURIComponent(key)}`)
        .join("&");
      const finalUrl = `${baseUrl}/api/Class/calendar${
        searchKeyQueryString.length > 0 ? "?" + searchKeyQueryString : ""
      }`;

      fetch(finalUrl, config)
        .then((res) => res.json())
        .then((data) => {
          setData(data.data.items);
          setTotalItems(data.data.totalCount);
        });
    }
  }, [searchKey]);

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      const value = e.target.value.trim();
      if (
        value &&
        searchKey.length < 4 &&
        !searchKey.find((tag) => tag === value)
      ) {
        setSearchKey([...searchKey, value]);
      }
      e.target.value = "";
    }
  };

  const handleDelete = (i) => {
    const newTags = searchKey.filter((tag) => tag !== i);
    setSearchKey(newTags);
  };

  const fetchClasses = async (start, end) => {
    try {
      if (start && end) {
        try {
          const responseJson = await GetClassesByWeek(start, end);
          setClassesByWeek(responseJson.data.items);
        } catch (error) {
          console.error(error);
        }
      }
    } catch (error) {
      console.error(error);
    }
  };

  const fetchFilteredClasses = async (start, end) => {
    try {
      const filterItems = filteredWeekList;
      setfilteredWeekList(filterItems);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    if (startOfWeek && endOfWeek) {
      const formattedStartOfWeek = dayjs(startOfWeek).format("YYYY-MM-DD");
      const formattedEndOfWeek = dayjs(endOfWeek).format("YYYY-MM-DD");
      if (isFiltered) {
        fetchFilteredClasses(formattedStartOfWeek, formattedEndOfWeek);
      } else {
        fetchClasses(formattedStartOfWeek, formattedEndOfWeek);
      }
    }
  }, [startOfWeek, endOfWeek, isFiltered]);

  const handleSearch = debounce((e) => {
    let term = e.target.value;
    let termLower = term.toLowerCase();
    setSearch(true);
    if (term != "") {
      fetch(`${baseUrl}/api/Class`, config)
        .then((res) => res.json())
        .then((data) => {
          setData(
            data.data.items.filter(
              (item) =>
                item.className.toLowerCase().includes(termLower) ||
                item.classCode.toLowerCase().includes(termLower)
            )
          );
          setTotalItems(data.data.totalCount);
        });
    } else {
      fetch(`${baseUrl}/api/Class`, config)
        .then((res) => res.json())
        .then((data) => {
          setData(
            data.data.items.filter(
              (item) =>
                item.startDate <= FormatDate(dayjs(value)) &&
                item.endDate >= FormatDate(dayjs(value))
            )
          );
          setTotalItems(data.data.totalCount);
        });
    }
  }, 300);

  const resetDataMonth = () => {
    setData([]);
  };

  return (
    <div>
      <Header />
      <Container>
        <div style={{ display: "flex" }}>
          <Search_Input
            onSubmit={(event) => handleKeyDown(event)}
            onChange={handleSearch}
          />
          <ButtonUtil
            name="Filter"
            icon={<FilterListIcon />}
            style={buttonStyle}
            onClick={(event) => setAnchorEl(event.currentTarget)}
          />
          {day && (
            <FilterPopOver
              fetchData={fetchData}
              anchorEl={anchorEl}
              setAnchorEl={setAnchorEl}
              onFilteredData={handleDayFilterData}
              setIsFiltered={setIsFiltered}
              setFilteredList={setFilteredDayList}
            />
          )}

          {week && (
            <FilterWeek
              fetchData={fetchClasses}
              anchorEl={anchorEl}
              setAnchorEl={setAnchorEl}
              onFilteredData={handleWeekFilterData}
              setIsFiltered={setIsFiltered}
              setFilteredList={setfilteredWeekList}
              startOfWeek={startOfWeek}
              endOfWeek={endOfWeek}
            />
          )}
        </div>
        {searchKey.length > 0 && (
          <div
            style={{
              display: "flex",
              marginTop: "3px",
              padding: "20px 10px 10px 10px",
              gap: "10px",
            }}
          >
            {searchKey.map((item) => (
              <SearchKey keySearch={item} onDelete={handleDelete} />
            ))}
          </div>
        )}
        <Choosen>
          <div style={{ width: "80px" }}>
            <Button
              style={{
                color: day == true ? "white" : "black",
                backgroundColor: day == true ? "#2d3748" : "white",
                textTransform: "none",
                borderRadius: "15px",
                width: "100%",
              }}
              variant="text"
              onClick={() => handleChoice(1)}
            >
              Day
            </Button>
          </div>
          <div style={{ width: "90px" }}>
            <Button
              style={{
                color: week == true ? "white" : "black",
                backgroundColor: week == true ? "#2d3748" : "white",
                textTransform: "none",
                width: "100%",
                borderRadius: "15px",
              }}
              variant="text"
              onClick={() => handleChoice(2)}
            >
              Week
            </Button>
          </div>
        </Choosen>
        {week && (
          <WeekCalendar
            onChange={setWeekValue}
            onMonthChange={resetDataMonth}
            setStartOfWeek={setStartOfWeek}
            setEndOfWeek={setEndOfWeek}
            onWeekChange={(start, end) => {
              setStartOfWeek(start);
              setEndOfWeek(end);
            }}
            value={weekValue}
            week={week}
          />
        )}

        {day && <CalendarPickDay onChange={setValue} value={value} day={day} />}

        {day && (
          <TableCalendar
            data={isFiltered ? filterData : data}
            setData={setData}
            value={value}
            isFiltered={isFiltered}
            day={day}
          />
        )}

        {week && (
          <TableCalendarWeek
            data={isFiltered ? filteredWeekList : classesByWeek}
            setData={setData}
            value={weekValue}
            startOfWeek={startOfWeek}
            endOfWeek={endOfWeek}
          />
        )}
      </Container>
    </div>
  );
}
