import React, { useCallback, useEffect } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Action from "./Action";
import SortIcon from "@mui/icons-material/Sort";
import Modal from "../components/Modal";
import _sortBy from "lodash";
import style from "../../../assert/css/Attendee.module.scss";
import LoadingPage from "../../../components/Form-Control/Loading";
import SpliceDateUtil from "../../../Utils/SpliceDate";
import InventoryIcon from "@mui/icons-material/Inventory";
import NoDataPage from "./NoDataPage";
import {
  FsuListApi,
  GetFsuName,
  LocationList,
  AttendeeTypeList,
} from "../api/ListApi";
import GetFsuByClass from "../../../Utils/GetFsuByClass";
import FormatDateInFigma from "../../../Utils/FormatDateInFigma";
import { useNavigate } from "react-router-dom";
import Button from "@mui/material/Button";
import GlobalLoading from "../../../../../global/GlobalLoading";
export default function TableList({
  TableHeader,
  page,
  itemsPerPage,
  data,
  setData,
  isFiltered,
  isSearch,
  rerender,
}) {
  const [open, setOpen] = React.useState(false);
  const [a, setA] = React.useState([]);
  const navigate = useNavigate();
  // const [dataFill, setDataFill] = React.useState(data);
  const [sortOrder, setSortOrder] = React.useState("asc");
  const [locationMapping, setLocationMapping] = React.useState({});
  const [fsuToName, setFsuToName] = React.useState({});
  const [attendeeTypes, setAttendeeTypes] = React.useState([]);
  let idList = (page - 1) * itemsPerPage + 1;

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
  const offset = (page - 1) * itemsPerPage;

  const styleHeader = {
    gap: "6px",
    display: "flex",
    marginTop: "4px",
    witdh: "150px",
    justifyContent: "center"
  };

  const handleSort = useCallback(
    (item) => {
      if (item === "Class") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["className"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "ClassCode") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["classCode"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "CreatedOn") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["createdDate"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "CreatedBy") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["createdBy"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "Duration") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["duration"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "Attendee") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["attendeeLevelId"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "Location") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["locationId"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
      if (item === "FSU") {
        let order = sortOrder === "asc" ? "desc" : "asc";
        const data1 = _sortBy.orderBy(
          data.slice(offset, offset + itemsPerPage),
          ["fsuId"],
          [order]
        );
        setData((prev) => {
          return [
            ...prev.slice(0, offset),
            ...data1,
            ...prev.slice(offset + itemsPerPage),
          ];
        });
        setSortOrder(order);
      }
    },
    [setData, sortOrder, data, offset, itemsPerPage]
  );

  const styleLocation = {
    fontWeight: "700",
    width: "150px",
    overflow: "hidden",
    whiteSpace: "nowrap",
    textOverflow: "ellipsis",
  };
  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 700 }} aria-label="simple table" size="small">
          <TableHead style={{ backgroundColor: "#2d3748" }}>
            <TableRow>
              {TableHeader.map((row, index) => (
                <TableCell
                  style={{ color: "white", alignItems: "center" }}
                  align="left"
                  key={row}
                >
                  <div style={styleHeader}>
                    {row != "ID" && <span>{row}</span>}
                    {row === "ID" && (
                      <span style={{ marginBottom: "8px" }}>{row}</span>
                    )}
                    {row != "ID" && (
                      <span
                        style={{ cursor: "pointer" }}
                        onClick={() => handleSort(row)}
                      >
                        <SortIcon />
                      </span>
                    )}
                  </div>
                </TableCell>
              ))}
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          <TableBody
            sx={{
              "&:last-child td, &:last-child th": {
                borderBottom: 1,
                borderColor: "#2d3748",
              },
            }}
          >
            {!data && <NoDataPage />}
            {isFiltered && data && data.length === 0 && <NoDataPage />}
            {isSearch && data && data.length === 0 && <NoDataPage />}
            {!isSearch && !isFiltered && data && data.length === 0 && (
              <TableRow
                style={{ textAlign: "center" }}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell
                  style={{ fontWeight: "600" }}
                  component="th"
                  scope="row"
                  colSpan={8}
                >
                  <div
                    style={{
                      height: "100px",
                      display: "flex",
                      justifyContent: "center",
                      alignItems: "center",
                    }}
                  >
                    <GlobalLoading isLoading={(!isSearch && !isFiltered && data && data.length === 0)}/>
                  </div>
                </TableCell>
              </TableRow>
            )}
            {data &&
              data.slice(offset, offset + itemsPerPage).map((row, index) => {
                return (
                  <TableRow
                    style={{ textAlign: "center" }}
                    key={index}
                    sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  >
                    <TableCell
                      style={{ fontWeight: "600" }}
                      component="th"
                      scope="row"
                    >
                      {idList++}
                    </TableCell>
                    <TableCell
                      sx={{fontWeight: "600"}}
                      component="th"
                      scope="row"
                    >
                      <Button
                        sx={{width: "240px"}}
                        onClick={() => navigate(`/class-detail/${row.classId}`)}
                      >
                        <span>{row.className}</span>
                      </Button>
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "100px" }}
                      align="left"
                    >
                      {row.classCode}
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "100px" }}
                      align="left"
                    >
                      {FormatDateInFigma(row.createdDate)}
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "200px" }}
                      align="left"
                    >
                      {row.createdBy}
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "200px" }}
                      align="left"
                    >
                      {row.duration + " " + "days"}
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "200px" }}
                      align="left"
                    >
                      {Array.isArray(attendeeTypes) &&
                        attendeeTypes.map((type) => {
                          if (row.attendeeLevelId === type.attendeeTypeId) {
                            let styleName;
                            switch (type.attendeeTypeName) {
                              case "Intern":
                                styleName = style.intern;
                                break;
                              case "Fresher":
                                styleName = style.fresher;
                                break;
                              case "Online fee-fresher":
                                styleName = style.onlineFresher;
                                break;
                              case "Offline fee-fresher":
                                styleName = style.offFresher;
                                break;
                              default:
                                styleName = "";
                            }
                            return (
                              <div className={styleName} key={type.id}>
                                {type.attendeeTypeName}
                              </div>
                            );
                          }
                        })}
                    </TableCell>
                    <TableCell align="left">
                      <div style={styleLocation}>
                        {locationMapping[row.locationId]}
                      </div>
                    </TableCell>
                    <TableCell
                      style={{ fontWeight: "600", witdh: "50px" }}
                      align="left"
                    >
                      {GetFsuByClass(fsuToName, row.fsuId)}
                    </TableCell>
                    <TableCell align="center">
                      <Action
                        setOpen={setOpen}
                        row={row}
                        setA={setA}
                        setData={setData}
                        rerender={rerender}
                      />
                    </TableCell>
                  </TableRow>
                );
              })}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
