import React from "react";
import logoTrainer from "../../../../../../assets/LogoStManagement/concept-lecture.png";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import logoFsu from "../../../../../../assets/LogoStManagement/supplier.png";
import HomeWorkOutlinedIcon from "@mui/icons-material/HomeWorkOutlined";
import style from "../../../assert/css/Time.module.scss";
import { TimePicker, Alert } from "antd";
import dayjs from "dayjs";
import customParseFormat from "dayjs/plugin/customParseFormat";
import { Select, Space, Form } from "antd";
import { FsuListApi, GetUserBasic } from "../../ClassList/api/ListApi";
import FormatTime from "../../../Utils/FormatTime";
import AccessAlarmIcon from "@mui/icons-material/AccessAlarm";

export default function GeneralForm({
  timeFrom,
  timeTo,
  setTimeFrom,
  setTimeTo,
  Admin,
  setAdmin,
  FSU,
  setFsu,
  errorsValidate,
  setErrorsValidate
}) {
  const [listAdmin, setListAdmin] = React.useState([]);
  const [listFsu, setListFsu] = React.useState([]);
  dayjs.extend(customParseFormat);
  const format = "HH:mm";

  const handleChange = (value) => {
  };

  const handleChangeAdmin = (value) => {
    setAdmin(value);
    const storedValues =
      JSON.parse(localStorage.getItem("selectedValues")) || {};
    storedValues.Admin = value;
    localStorage.setItem("selectedValues", JSON.stringify(storedValues));
    const newAdmin = {...errorsValidate};
    newAdmin.admin = "";
    setErrorsValidate(newAdmin);
  };

  const handleChangeFsu = (value) => {
    setFsu(value);
    const storedValues =
      JSON.parse(localStorage.getItem("selectedValues")) || {};
    storedValues.FSU = value;
    localStorage.setItem("selectedValues", JSON.stringify(storedValues));
    const newFsu = {...errorsValidate};
    newFsu.fsuId = "";
    setErrorsValidate(newFsu);
  };

  React.useEffect(() => {
    const fetchApiData = async () => {
      const listBasicUser = await GetUserBasic();
      setListAdmin(listBasicUser);
    };
    fetchApiData();
  }, []);

  React.useEffect(() => {
    const fetchApiData = async () => {
      const listFsu = await FsuListApi();
      setListFsu(listFsu);
    };
    fetchApiData();
  }, []);

  const options = listAdmin?.map((user) => ({
    label: user.fullName,
    value: user.userId,
  }));
  const FsuList = listFsu?.map((user) => ({
    label: user.name,
    value: user.fsuId,
  }));

  React.useEffect(() => {
    const storedValues =
      JSON.parse(localStorage.getItem("selectedValues")) || {};
    if (storedValues.Admin) {
      setAdmin(storedValues.Admin);
    }
    if (storedValues.FSU) {
      setFsu(storedValues.FSU);
    }
  }, [Admin, FSU]);

  return (
    <div className={style.background}>
      <div style={{ width: "100%" }} className={style.container}>
        <div className={style.item}>
          <div style={{ display: "flex", gap: "10px" }}>
            <AccessAlarmIcon />
            <div>
              <strong>Time</strong>
            </div>
          </div>
          <div className={style.TimePicker}>
            <div>from</div>
            <div>
              <TimePicker
                size="middle"
                style={{ width: "80px" }}
                format={format}
                changeOnScroll
                needConfirm={false}
                placeholder="--:--"
                value={timeFrom}
                onChange={(newValue) => setTimeFrom(newValue)}
                status={!timeFrom && "error"}
                disabledTime={() => ({
                  disabledHours: () => {
                    return [
                      ...Array(8).keys(),
                      ...Array.from({ length: 2 }, (_, i) => i + 23),
                    ];
                  },
                  disabledMinutes: () => {
                    return Array.from({ length: 60 }, (_, i) => i).filter(
                      (i) => i % 30 !== 0
                    );
                  },
                })}
                hideDisabledOptions
              />
            </div>
            <div>to</div>
            <div>
              <TimePicker
                size="middle"
                style={{ width: "80px" }}
                format={format}
                changeOnScroll
                needConfirm={false}
                placeholder="--:--"
                value={timeTo}
                onChange={(newValue) => setTimeTo(newValue)}
                status={!timeTo && "error"}
                disabledTime={() => ({
                  disabledHours: () => {
                    return [
                      ...Array(8).keys(),
                      ...Array.from({ length: 2 }, (_, i) => i + 23),
                    ];
                  },
                  disabledMinutes: () => {
                    return Array.from({ length: 60 }, (_, i) => i).filter(
                      (i) => i % 30 !== 0
                    );
                  },
                })}
                hideDisabledOptions
              />
            </div>
          </div>
        </div>
        {!timeFrom && <><span style={{color: "red"}}>Start time is required</span><br /></>}
        {!timeTo && <><span style={{color: "red"}}>End time is required</span><br /></>}
        {errorsValidate.timeFrom && <><span style={{color: "red"}}>{errorsValidate.timeFrom}</span><br /></>} 
        <div className={style.item}>
          <div style={{ display: "flex", gap: "10px" }}>
            <HomeWorkOutlinedIcon style={{ color: "#e0dfdf" }} />
            <div>
              <strong style={{ color: "gray" }}>Location</strong>
            </div>
          </div>
        </div>
        <div className={style.item}>
          <div style={{ display: "flex", gap: "10px" }}>
            <img width={20} src={logoTrainer} />
            <div>
              <strong style={{ color: "gray" }}>Trainer</strong>
            </div>
          </div>
        </div>
        <div className={style.item}>
          <div style={{ display: "flex", gap: "10px" }}>
            <StarBorderIcon />
            <div>
              <strong>Admin</strong>
            </div>
          </div>
          <div className={style.TimePicker}>
            <Space wrap>
              <Select
                placeholder="select"
                style={{
                  width: 185,
                }}
                onChange={handleChangeAdmin}
                options={options}
                value={Admin}
              />
            </Space>
          </div>
        </div>
        {errorsValidate.admin && <span style={{color: "red"}}>{errorsValidate.admin}</span>}
        <div className={style.item}>
          <div style={{ display: "flex", gap: "10px" }}>
            <img width={20} src={logoFsu} />
            <div>
              <strong>FSU</strong>
            </div>
          </div>
          <div className={style.TimePicker}>
            <Space wrap>
              <Select
                placeholder="select"
                style={{
                  width: 185,
                }}
                onChange={handleChangeFsu}
                options={FsuList}
                value={FSU}
              />
            </Space>
          </div>
        </div>
        {errorsValidate.fsuId && <span style={{color: "red"}}>{errorsValidate.fsuId}</span>}
        <div
          style={{
            marginBottom: "15px",
            marginTop: "15px",
            display: "flex",
            alignItems: "center",
            justifyContent: "end",
          }}
        >
          <Space wrap>
            <Select
              placeholder="gmail"
              style={{
                width: 185,
              }}
              onChange={handleChange}
              options={[
                {
                  value: "jack",
                  label: "Tinh@gmail.com",
                },
                {
                  value: "lucy",
                  label: "Tuan@gmail.com",
                },
              ]}
            />
          </Space>
        </div>
        <hr style={{ height: "1px", background: "black", border: "0" }}></hr>
        <div style={{ color: "gray", margin: "10px 0px 10px 0px" }}>
          <div style={{ marginBottom: "8px", marginTop: "8px" }}>Created</div>
          <div style={{ marginBottom: "8px", marginTop: "8px" }}>Review</div>
          <div>Approve</div>
        </div>
      </div>
    </div>
  );
}
