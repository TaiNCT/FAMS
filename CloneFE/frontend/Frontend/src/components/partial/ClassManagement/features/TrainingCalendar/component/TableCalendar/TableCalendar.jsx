import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import EmptyCell from "../EmptyCell";
import style from "../../../../assert/css/AttendeeCalendar.module.scss";
import ViewDetail from "../PopOver";
import Box from "@mui/material/Box";
import { GetClassInfoByCalendar } from "../../../ClassList/api/ListApi";
import CalculateDay from "../../../../Utils/CalculateDay";
import dayjs from "dayjs";
import FormatDate from "../../../../Utils/FormatDate";
import logo from "../../../../../../../assets/LogoStManagement/concept-lecture-d6D.png";
import logoLocation from "../../../../../../../assets/LogoStManagement/homework.png";
import logoAdmin from "../../../../../../../assets/LogoStManagement/grade.png";
import { AttendeeTypeList } from "../../../ClassList/api/ListApi";
import GlobalLoading from "../../../../../../global/GlobalLoading";
export default function TableCalendar({ data, setData, value, isFiltered }) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [item1, setItem1] = React.useState(false);
  const [item2, setItem2] = React.useState(false);
  const [item3, setItem3] = React.useState(false);
  const [item4, setItem4] = React.useState(false);
  const [attendeeTypes, setAttendeeTypes] = React.useState([]);

  const [viewData, setViewData] = React.useState(null);
  const [detail, setDetail] = React.useState();
  const handleClick = (event) => {
    setAnchorEl(anchorEl ? null : event.currentTarget);
    setItem1(false);
    setItem2(false);
    setItem3(false);
    setItem4(false);
  };
  const handleItem = (item) => {
    setDetail(item);
  };

  const handleItem1 = (event) => {
    setItem1(!item1);
    setItem2(false);
    setItem3(false);
    setItem4(false);
    setAnchorEl(null);
  };
  React.useEffect(() => {
    if (detail) {
      const fetchApiData = async () => {
        const a = await GetClassInfoByCalendar(detail.classId);
        setViewData(a);
      };
      fetchApiData();
    }
  }, [detail]);
  const handleItem2 = (event) => {
    setItem2(!item2);
    setItem1(false);
    setItem3(false);
    setItem4(false);
    setAnchorEl(null);
  };
  const handleItem3 = (event) => {
    setItem3(!item3);
    setItem2(false);
    setItem1(false);
    setItem4(false);
    setAnchorEl(null);
  };
  const handleItem4 = (event) => {
    setItem4(!item4);
    setItem2(false);
    setItem3(false);
    setItem1(false);
    setAnchorEl(null);
  };
  React.useEffect(() => {
    AttendeeTypeList()
      .then((data) => setAttendeeTypes(data))
      .catch((error) => console.error("Error:", error));
  }, []);

  return (
    <>
    <GlobalLoading isLoading={(data && data.length === 0)}/>
    <TableContainer component={Paper}>
      <Table
        style={style.table}
        sx={{ minWidth: 700 }}
        aria-label="simple table"
        size="medium"
      >
        <TableHead>
          <TableRow>
            <th
              colSpan={30}
              style={{
                padding: "10px",
                color: "white",
                textAlign: "left",
                background: "#2d3748",
                borderRadius: "10px",
              }}
            >
              Morning (8:00 - 12:00)
            </th>
          </TableRow>
        </TableHead>
        <TableBody
          sx={{
            "&:last-child td, &:last-child tr": {
              borderBottom: 1,
              borderColor: "#2d3748",
            },
          }}
        >
          <TableRow key={Math.random()}>
            <TableCell sx={{ width: "40px" }} align="left">
              8:00
            </TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "08:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
                  {Array.isArray(attendeeTypes) &&
                    attendeeTypes.map((type) => {
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Intern"
                      ) {
                        return (
                          <>
                            <div
                              className={style.intern}
                              onClick={(event) => {
                                handleItem1(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item1 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.fresher}
                              onClick={(event) => {
                                handleItem2(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item2 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Online fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.onlineFresher}
                              onClick={(event) => {
                                handleItem3(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item3 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Offline fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.offFresher}
                              onClick={(event) => {
                                handleItem4(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item4 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter(
              (a) => a.startTime === "08:0furtherFilteredItems V0:00"
            ).length <= 0 && <EmptyCell />}
          </TableRow>
          <TableRow>
            <TableCell align="left">8:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "08:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "08:30:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">9:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "09:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "09:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">9:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "09:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "09:30:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">10:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "10:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "10:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">10:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "10:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "10:30:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">11:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "11:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "11:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">11:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "11:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "11:30:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">12:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "12:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "12:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
        </TableBody>
      </Table>
      <Table sx={{ minWidth: 650 }} aria-label="simple table" size="medium">
        <TableHead>
          <TableRow>
            <th
              colSpan={30}
              style={{
                padding: "10px",
                color: "white",
                textAlign: "left",
                background: "#2d3748",
                borderRadius: "10px",
              }}
            >
              Noon (13:00 - 17:00)
            </th>
          </TableRow>
        </TableHead>
        <TableBody
          sx={{
            "&:last-child td, &:last-child tr": {
              borderBottom: 1,
              borderColor: "#2d3748",
            },
          }}
        >
          <TableRow key={Math.random()}>
            <TableCell sx={{ width: "40px" }} align="left">
              13:00
            </TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "13:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
                  {Array.isArray(attendeeTypes) &&
                    attendeeTypes.map((type) => {
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Intern"
                      ) {
                        return (
                          <>
                            <div
                              className={style.intern}
                              onClick={(event) => {
                                handleItem1(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item1 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.fresher}
                              onClick={(event) => {
                                handleItem2(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item2 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Online fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.onlineFresher}
                              onClick={(event) => {
                                handleItem3(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item3 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Offline fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.offFresher}
                              onClick={(event) => {
                                handleItem4(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item4 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "13:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">13:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "13:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">14:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "14:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">14:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "14:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">15:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "15:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">15:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "15:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">16:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "16:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">16:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "16:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">17:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "17:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
        </TableBody>
      </Table>
      <Table sx={{ minWidth: 650 }} aria-label="simple table" size="medium">
        <TableHead>
          <TableRow>
            <th
              colSpan={30}
              style={{
                padding: "10px",
                color: "white",
                textAlign: "left",
                background: "#2d3748",
                borderRadius: "10px",
              }}
            >
              Night (18:00 - 22:00)
            </th>
          </TableRow>
        </TableHead>
        <TableBody
          sx={{
            "&:last-child td, &:last-child tr": {
              borderBottom: 1,
              borderColor: "#2d3748",
            },
          }}
        >
          <TableRow key={Math.random()}>
            <TableCell sx={{ width: "40px" }} align="left">
              18:00
            </TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "18:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
                  {Array.isArray(attendeeTypes) &&
                    attendeeTypes.map((type) => {
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Intern"
                      ) {
                        return (
                          <>
                            <div
                              className={style.intern}
                              onClick={(event) => {
                                handleItem1(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item1 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.fresher}
                              onClick={(event) => {
                                handleItem2(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item2 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Online fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.onlineFresher}
                              onClick={(event) => {
                                handleItem3(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item3 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                      if (
                        row.attendeeLevelId === type.attendeeTypeId &&
                        type.attendeeTypeName === "Offline fee-fresher"
                      ) {
                        return (
                          <>
                            <div
                              className={style.offFresher}
                              onClick={(event) => {
                                handleItem4(event);
                                handleItem(row);
                              }}
                            >
                              {row.classCode} | {row.className}
                            </div>
                            <div>
                              {item4 && (
                                <Box
                                  sx={{
                                    borderRadius: 1,
                                    p: 1,
                                    bgcolor: "#8eb1da",
                                    width: 230,
                                    height: "auto",
                                  }}
                                >
                                  {CalculateDay(
                                    row.startDate,
                                    row.endDate,
                                    FormatDate(dayjs(value))
                                  )}
                                  <br />
                                  {viewData && viewData.location && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logo} />
                                        Location{" "}
                                      </div>
                                      <div>
                                        {viewData && viewData?.location}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  {viewData && viewData.users && (
                                    <div className={style.pictures}>
                                      <div className={style.pictures}>
                                        <img width={14} src={logoLocation} />
                                        Trainer{" "}
                                      </div>
                                      <div
                                        style={{
                                          textDecoration: "underline",
                                          marginLeft: "30px",
                                        }}
                                      >
                                        {viewData &&
                                          viewData.users &&
                                          viewData.users.map((row, index) => {
                                            if (
                                              row?.role?.roleName === "Trainer"
                                            ) {
                                              return (
                                                <React.Fragment key={row.id}>
                                                  {row.fullName}
                                                  {index <
                                                    viewData.users.length -
                                                      1 && <br />}
                                                </React.Fragment>
                                              );
                                            }
                                          })}
                                      </div>
                                      <br />
                                    </div>
                                  )}
                                  <div className={style.pictures}>
                                    <div className={style.pictures}>
                                      <img width={14} src={logoAdmin} />
                                      Admin{" "}
                                    </div>
                                    <div
                                      style={{
                                        textDecoration: "underline",
                                        marginLeft: "10px",
                                      }}
                                    >
                                      {viewData &&
                                        viewData.users &&
                                        viewData.users.map((row, index) => {
                                          if (
                                            row?.role?.roleName ===
                                              "Super Admin" ||
                                            row?.role?.roleName ===
                                              "Class Admin" ||
                                            row?.role?.roleName === "Admin"
                                          ) {
                                            return (
                                              <React.Fragment key={row.id}>
                                                {row.fullName}
                                                {index <
                                                  viewData.users.length - 1 && (
                                                  <br />
                                                )}
                                              </React.Fragment>
                                            );
                                          }
                                        })}
                                    </div>
                                    <br />
                                  </div>
                                </Box>
                              )}
                            </div>
                          </>
                        );
                      }
                    })}
                </TableCell>
              ))}
            {data.filter((a) => a.startTime === "18:00:00").length <= 0 && (
              <EmptyCell />
            )}
          </TableRow>
          <TableRow>
            <TableCell align="left">18:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "18:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">19:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "19:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">19:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "19:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">20:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "20:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">20:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "20:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">21:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "21:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">21:30</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "21:30:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
          <TableRow>
            <TableCell align="left">22:00</TableCell>
            <TableCell sx={{ width: "8px" }}>
              <span>|</span>
            </TableCell>
            {data
              .filter((a) => a.startTime === "22:00:00")
              .map((row, index) => (
                <TableCell key={index} align="left">
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
                          <div
                            className={styleName}
                            onClick={(event) => {
                              handleClick(event);
                              handleItem(row);
                            }}
                          >
                            {row.classCode} | {row.className}
                            <ViewDetail
                              anchorEl={anchorEl}
                              setAnchorEl={setAnchorEl}
                              detail={detail}
                              value={value}
                              viewData={viewData}
                            />
                          </div>
                        );
                      }
                    })}
                </TableCell>
              ))}
          </TableRow>
        </TableBody>
      </Table>
    </TableContainer>
    </>
  );
}
