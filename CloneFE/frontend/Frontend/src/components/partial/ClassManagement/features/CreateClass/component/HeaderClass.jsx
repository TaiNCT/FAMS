import React from "react";

function HeaderClass()
{
  return (
    <div style={{ background: "#2d3748" }}>
      {/* <div style={{ display: "flex", height: "55px", alignItems: "center" }}>
        <h2
          style={{
            fontSize: "24px",
            paddingLeft: "15px",
            color: "white",
            letterSpacing: "5px",
          }}
        >
          Class
        </h2>
      </div> */}
      <div className="h-16 justify-start items-center gap-[15px] flex">
        <div className="text-4xl text-white font-semibold font-['Inter'] leading-9 tracking-[4.80px] pl-4 tracking-widest">
          Class
        </div>
      </div>
    </div>
  );
}

export default HeaderClass;
