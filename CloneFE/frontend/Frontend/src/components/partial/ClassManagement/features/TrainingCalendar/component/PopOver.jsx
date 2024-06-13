import * as React from "react";
import Box from "@mui/material/Box";
import Popper from "@mui/material/Popper";
import CalculateDay from "../../../Utils/CalculateDay";
import dayjs from "dayjs";
import FormatDate from "../../../Utils/FormatDate";
import logo from "../../../../../../../src/assets/LogoStManagement/concept-lecture-d6D.png";
import logoLocation from "../../../../../../../src/assets/LogoStManagement/homework.png";
import logoAdmin from "../../../../../../../src/assets/LogoStManagement/grade.png";
import style from "../../../assert/css/AttendeeCalendar.module.scss";
import {
  GetClassInfoWithAdmin,
  GetClassInfoWithTrainer,
} from "../../ClassList/api/ListApi";

export default function ViewDetail({
  anchorEl,
  setAnchorEl,
  detail,
  viewData,
  value,
}) {
  const open = Boolean(anchorEl);
  const id = open ? "simple-popper" : undefined;
  const [classByTrainer, setClassByTrainer] = React.useState(null);
  const [classByAdmin, setClassByAdmin] = React.useState(null);
  React.useEffect(() => {
    if (detail) {
      const fetchClass = async () => {
        const classTrainer = await GetClassInfoWithTrainer(detail.classId);
        const classAdmin = await GetClassInfoWithAdmin(detail.classId);
        setClassByTrainer(classTrainer);
        setClassByAdmin(classAdmin);
      };
      fetchClass();
    }
  }, [detail]);
  
  return (
    <div>
      <Popper
        id={id}
        open={open}
        anchorEl={anchorEl}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "center",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "center",
        }}
      >
        <Box
          sx={{
            borderRadius: 1,
            p: 1,
            bgcolor: "#8eb1da",
            width: 230,
            height: "auto",
          }}
        >
          {detail &&
            CalculateDay(
              detail.startDate,
              detail.endDate,
              FormatDate(dayjs(value))
            )}
          <br />
          {viewData && (
            <div className={style.pictures}>
              <div className={style.pictures}>
                <img width={14} src={logo} />
                Location{" "}
              </div>
              <div>{viewData.location}</div>
              <br />
            </div>
          )}
          {classByTrainer && classByTrainer.users && (
            <div className={style.pictures}>
              <div className={style.pictures}>
                <img width={14} src={logoLocation} />
                Trainer{" "}
              </div>
              <div style={{ textDecoration: "underline", marginLeft: "20px" }}>
                {classByTrainer.users.map((row) => {
                  return <div>{row.fullName}</div>;
                })}
              </div>
            </div>
          )}
          {classByAdmin && classByAdmin.users && (
            <div className={style.pictures}>
              <div className={style.pictures}>
                <img width={14} src={logoAdmin} />
                Admin{" "}
              </div>
              <div style={{ textDecoration: "underline",  marginLeft: "10px"  }}>
                {classByAdmin.users.map((row) => {
                  return <div>{row.fullName}</div>;
                })}
              </div>
              <br />
            </div>
          )}
        </Box>
      </Popper>
    </div>
  );
}
