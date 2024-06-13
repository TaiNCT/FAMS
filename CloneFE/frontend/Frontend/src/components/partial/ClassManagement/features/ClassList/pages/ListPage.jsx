import React, { useEffect, useState } from "react";
import Tittle from "../components/Tittle";
import Search_Input from "../../../components/Form-Control/Search_Input";
import TableList from "../components/TableList";
import ButtonUtil from "../components/Button";
import PageNavigation from "../components/PageNavigation";
import Container from "../components/Container";
import Navbar from "../components/Navbar";
import Main from "../components/Main";
import FilterListIcon from "@mui/icons-material/FilterList";
import ControlPointIcon from "@mui/icons-material/ControlPoint";
import style from "../../../assert/css/ListPage.module.scss";
import buttonStyle from "../../../assert/css/Button.module.scss";
import MockData from "../../ClassList/pages/Mock_data.json";
import FilterPopOver from "../components/FilterPopOver";
import { debounce, set } from "lodash";
import ShowItem from "../components/ShowItem";
import { useNavigate } from "react-router-dom";
import { baseUrl } from "../../../assert/config";
import { AttendeeTypeList, ClassListApi, GetFsuName, LocationList } from "../api/ListApi";
import GetAppIcon from '@mui/icons-material/GetApp';
import { CSVLink } from "react-csv";
import GetFsuByClass from "../../../Utils/GetFsuByClass";
import FormatDateInFigma from "../../../Utils/FormatDateInFigma";
function ListPage() {
  const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
  const config = {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };
  const [data, setData] = React.useState([]);
  const [search, setSearch] = React.useState(false);
  const [totalItems, setTotalItems] = React.useState(0);
  const [page, setPage] = React.useState(1);
  const [perPage, setPerPage] = React.useState(5);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [filterData, setFilteredData] = React.useState([]);
  const [isFiltered, setIsFiltered] = React.useState(false);
  const [renderKey, setRenderKey] = useState(0);
  const [dataExport,setDataExport] = useState([]);
  const [attendeeTypes, setAttendeeTypes] = React.useState([]);
  const [locationMapping, setLocationMapping] = React.useState({});
  const [fsuToName, setFsuToName] = React.useState({});
  useEffect(() => {
    AttendeeTypeList()
      .then((data) => setAttendeeTypes(data))
      .catch((error) => console.error("Error:", error));
  }, []);
  useEffect(() => {
    const fetchLocations = async () => {
      const locationList = await LocationList();

      // Create a mapping from IDs to names
      const tempLocationMap = {};
      locationList.forEach((location) => {
        tempLocationMap[location.locationId] = location.name;
      });

      setLocationMapping(tempLocationMap);
    };

    fetchLocations();
  }, []);
  useEffect(() => {
    const fetchFSU = async () => {
      const fsuName = await GetFsuName();
      const listFsuName = {};
      for (let id in fsuName) {
        listFsuName[id] = fsuName[id];
      }
      setFsuToName(listFsuName);
    };

    fetchFSU();
  }, []);
  const rerender = () => {
    setRenderKey(renderKey + 1);
  };
  let endIndex = page * perPage;
  const handleFilteredData = (data) => {
    setData(data.data.items);
    setFilteredData(data.data.items);
    setPage(1);
    setTotalItems(data.data.totalCount);
    setIsFiltered(true);
    setSearch(false);
  };

	const fetchData = async () =>
	{
		const classByUser = await ClassListApi();
		const res = await fetch(`${baseUrl}/api/Class`, config);
		const data1 = await res.json();
		const classIDs = classByUser.map(item => item.classId);
		let data2;
		if (classIDs.length > 0)
		{
			data2 = data1.data.items.filter((item) =>
				classIDs.includes(item.classId) && item.classStatus !== "Deactive" 
			);
		} else
		{
			data2 = data1.data.items.filter((item) => item.classStatus !== "Deactive");
		}
    const filteredData = data2.filter(item => item.createdDate);
    filteredData.sort((a, b) => new Date(b.createdDate) - new Date(a.createdDate));
		setData(filteredData);
		setFilteredData(filteredData);
		setTotalItems(filteredData.length);
	};
  useEffect(() => {
    if (!isFiltered && !search){
      fetchData();
    }
  }, [page, isFiltered, renderKey]);

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
          setTotalItems(data.data.items.filter(
            (item) =>
              item.className.toLowerCase().includes(termLower) ||
              item.classCode.toLowerCase().includes(termLower)
          ).length);
        });
    } else {
      fetchData();
      setSearch(false);
      setPage(1)
    }
    setIsFiltered(false);
  }, 300);

  const navigate = useNavigate();

  const TableHeader = [
    "ID",
    "Class",
    "ClassCode",
    "CreatedOn",
    "CreatedBy",
    "Duration",
    "Attendee",
    "Location",
    "FSU",
  ];
const offset = (page - 1) * perPage;

const getClassExport = (event,done) => {
	let result = [];
	if ((data && data.length > 0 && (!isFiltered && !search))) {
		result.push(["Class Name","Class Code","Created On","Created By","Duration","Attendee","Location","FSU"]);
		data.slice(offset, offset + perPage).map((item, index) => {
			let  arr = [];
			arr[0] = item.className
			arr[1] = item.classCode
			arr[2] = FormatDateInFigma(item.createdDate)
			arr[3] = item.createdBy
			arr[4] = item.duration
			arr[5] = Array.isArray(attendeeTypes) && attendeeTypes.map((type) => {
        if (item.attendeeLevelId === type.attendeeTypeId) {
          return type.attendeeTypeName;
        }
        return null;
      }).filter(Boolean);
			arr[6] = locationMapping[item.locationId]
			arr[7] = GetFsuByClass(fsuToName, item.fsuId)
			result.push(arr);
		})
		setDataExport(result)
		done();	
	}
  if ((data && data.length > 0 && (search || isFiltered))){
    result.push(["Class Name","Class Code","Created On","Created By","Duration","Attendee","Location","FSU"]);
		data.map((item, index) => {
			let  arr = [];
			arr[0] = item.className
			arr[1] = item.classCode
			arr[2] = FormatDateInFigma(item.createdDate)
			arr[3] = item.createdBy
			arr[4] = item.duration
			arr[5] = Array.isArray(attendeeTypes) && attendeeTypes.map((type) => {
        if (item.attendeeLevelId === type.attendeeTypeId) {
          return type.attendeeTypeName;
        }
        return null;
      }).filter(Boolean);
			arr[6] = locationMapping[item.locationId]
			arr[7] = GetFsuByClass(fsuToName, item.fsuId)
			result.push(arr);
		})
		setDataExport(result)
		done();	
  }
}
  return (
    <div className={style.container}>
      <Tittle />
      <Container>
        <Navbar>
          <div style={{ display: "flex" }}>
            <Search_Input
              onChange={(event) => handleSearch(event)}
              setSearch={setSearch}
            />
            <ButtonUtil
              name="Filter"
              icon={<FilterListIcon />}
              style={buttonStyle}
              onClick={(event) => setAnchorEl(event.currentTarget)}
            />
            <FilterPopOver
              fetchData={fetchData}
              anchorEl={anchorEl}
              setAnchorEl={setAnchorEl}
              onFilteredData={handleFilteredData}
              setIsFiltered={setIsFiltered}
            />
          </div>
          <div style={{display: "flex", alignItems: "center"}}>
            <ButtonUtil
              name="Add new"
              icon={<ControlPointIcon />}
              style={buttonStyle}
              onClick={() => navigate("/createclass")}
            />
          <CSVLink 
	          filename = {"classList.csv"}
	          classname = "btn btn-primary"
            style={{background: "#d45b13", padding: "6px", borderRadius: "8px", marginLeft: "10px"}}
	          data={dataExport}
	          asyncOnClick = {true}
	          onClick = {getClassExport}
          >
    	        <GetAppIcon style={{color: "white"}}/><span style={{color:"white"}}>Export</span>
          </CSVLink>
          </div>
        </Navbar>
        <Main>
          <TableList
            TableHeader={TableHeader}
            page={page}
            data={data}
            isSearch={search}
            setData={setData}
            itemsPerPage={perPage}
            isFiltered={isFiltered}
            rerender={rerender}
          />
        </Main>
        {data && data.length > 0 && (
          <div
            style={{
              display: "flex",
              alignContent: "space-betwwen",
              justifyContent: "flex-end",
            }}
          >
            <div style={{ marginTop: "15px" }}>
              <PageNavigation
                page={page}
                setPage={setPage}
                totalItems={totalItems}
                ItemsPerPage={perPage}
              />
            </div>
            <ShowItem perPage={perPage} setPerPage={setPerPage} />
            <div style={{ marginTop: "19px", marginRight: "30px" }}>
              {data.length > perPage &&
                `${(page - 1) * perPage + 1} - ${
                  endIndex > totalItems ? (endIndex = totalItems) : endIndex
                } of ${totalItems}`}
              {data.length < perPage &&
                `${(page - 1) * perPage + 1} - ${data.length} of ${totalItems}`}
            </div>
          </div>
        )}
      </Container>
    </div>
  );
}

export default ListPage;
