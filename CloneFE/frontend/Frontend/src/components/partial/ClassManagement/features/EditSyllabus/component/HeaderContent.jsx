import { Button } from "@mui/material";
import React from "react";
import FormatDate from "../../../Utils/FormatDateInFigma";

export default function HeaderContent({ programPicked, className })
{
  return (
    <div>
      <div
        style={{
          letterSpacing: "6px",
          fontSize: "x-large",
          fontWeight: "bold",
        }}
      >
        {className}
      </div>
      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <div
          style={{
            display: "flex",
            gap: "10px",
            alignItems: "center",
          }}
        >
          <div
            style={{
              fontSize: "xx-large",
              fontWeight: "bolder",
              letterSpacing: "10px",
            }}
          >
            {programPicked.trainingProgramCode}
          </div>
          <div
            style={{
              background: programPicked.status === 'active' ? '#1F2937' : '#8b8b8b',
              color: "white",
              borderRadius: "14px",
              padding: "2px 10px 2px 10px",
            }}
          >
            {programPicked.status}
          </div>
        </div>
        <div>
          <Button sx={{ color: "#2d3748" }}>
            <span style={{ fontWeight: "900", fontSize: "50px" }}>...</span>
          </Button>
        </div>
      </div>
      <hr style={{ border: "1px solid #2d3748" }} />
      <div style={{ marginTop: "10px" }}>
        <span style={{ fontWeight: "bold", fontSize: "x-large" }}>{programPicked.days}</span> days
        ({programPicked.hours} hours)
      </div>
      <div style={{ marginBottom: "12px" }}>
        Modified on {FormatDate(programPicked.updatedDate)} by {programPicked.updatedBy}
      </div>
      <hr style={{ border: "1px solid #2d3748" }} />
    </div>
  );
}
