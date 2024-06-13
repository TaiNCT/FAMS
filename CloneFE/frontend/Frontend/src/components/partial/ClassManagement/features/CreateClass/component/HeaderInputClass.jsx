import { Button } from "@mui/material";
import React from "react";
import logo1 from "../../../../../../assets/LogoStManagement/asignment-lab.png";
import logo2 from "../../../../../../assets/LogoStManagement/concept-lecture.png";
import logo3 from "../../../../../../assets/LogoStManagement/exam.png";
import logo4 from "../../../../../../assets/LogoStManagement/seminar-workshop.png";
import logo5 from "../../../../../../assets/LogoStManagement/guide-review.png";
function HeaderInputClass({ className, programPicked }) {
  return (
    <div style={{ background: "#2d3748", color: "white" }}>
      <div style={{ padding: "0px 15px 0px 15px", letterSpacing: "5px" }}>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            marginBottom: "30px",
          }}
        >
          <div>
            <h2 style={{ fontSize: "24px" }}>Class</h2>
            <div style={{ fontSize: "30px" }}>
              {className}{" "}
              <span
                style={{
                  background: "#949494",
                  borderRadius: "10px",
                  padding: "4px",
                  fontSize: "14px",
                }}
              >
                Planing
              </span>
            </div>
          </div>
          <div>
            <Button sx={{ color: "white" }}>
              <span style={{ fontWeight: "900", fontSize: "50px" }}>...</span>
            </Button>
          </div>
        </div>
        <div style={{ width: "50%" }}>
          <hr></hr>
        </div>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            height: "60px",
            gap: "8px",
          }}
        >
          <span style={{ fontSize: "x-large" }}>
            {programPicked && programPicked.days}
          </span>
          days({programPicked && programPicked.hours}hour) |
          <img width={30} src={logo1} /> <img width={30} src={logo2} />
          <img width={30} src={logo3} />
          <img width={30} src={logo4} />
          <img width={30} src={logo5} />
        </div>
      </div>
    </div>
  );
}

export default HeaderInputClass;
