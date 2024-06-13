import React from "react";
import { useNavigate } from "react-router-dom";
import style from "./style.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";

const NotFound = () => {
  const navigate = useNavigate(); // Hook to utilize navigation functionality

  return (
    <div className={style.notfound}>
      <div className="not-found-content">
        <FontAwesomeIcon icon={faExclamationTriangle} size="10x" />{" "}
        <h2
          style={{
            fontSize: "2.5rem",
            color: "#CC0000",
            margin: "20px 0",
            fontWeight: "600",
          }}
        >
          404 - Page Not Found
        </h2>
        <button className={style.button} onClick={() => navigate("/")}>
          Back to Home
        </button>{" "}
      </div>
    </div>
  );
};

export default NotFound;
