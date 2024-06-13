import { FunctionComponent } from "react";
import styles from "./style.module.scss";
import React from "react";
import asmIcon from "../../../../assets/LogoStManagement/asignment-lab.png";
import conceptIcon from "../../../../assets/LogoStManagement/concept-lecture.png";
import examIcon from "../../../../assets/LogoStManagement/exam.png";
import seminarIcon from "../../../../assets/LogoStManagement/seminar-workshop.png";
import guideIcon from "../../../../assets/LogoStManagement/guide-review.png";
import moreIcon from "../../../../assets/LogoStManagement/morehorizontal.png";

const ClassHeader: FunctionComponent = () => {
  return (
    <div className={styles.classHeader}>
      <h2 className={styles.class}>Class</h2>
      <div className={styles.programName}>
        <div className={styles.checkbox}>
          <h1 className={styles.fresherDevelopOperation}>
            Fresher Develop Operation
          </h1>
          <button className={styles.chip}>
            <div className={styles.inactive}>Planning</div>
          </button>
        </div>
        <button className={styles.moreHorizontal} value="...">
          <img src={moreIcon} alt="" />
        </button>
      </div>
      <b className={styles.hcm22FrDevops01}>HCM22_FR_DevOps_01</b>
      <img className={styles.classHeaderChild} alt="" src="/line-11.svg" />
      <div className={styles.group}>
        <div className={styles.arrowforwardios}>
          <div className={styles.lastpage}>
            <div className={styles.rowsPerPage}>31</div>
            <div className={styles.days}>days</div>
          </div>
          <div className={styles.hoursWrapper}>
            <i className={styles.hours}>(97 hours)</i>
          </div>
        </div>
        <div className={styles.wrapper}>
          <div className={styles.div}>|</div>
        </div>
        <div className={styles.asignmentlabParent}>
          <img
            className={styles.asignmentlabIcon}
            loading="eager"
            alt=""
            src={asmIcon}
          />
          <img
            className={styles.conceptlectureIcon}
            loading="eager"
            alt=""
            src={conceptIcon}
          />
          <img className={styles.examIcon} alt="" src={examIcon} />
          <img
            className={styles.seminarworkshopIcon}
            alt=""
            src={seminarIcon}
          />
          <img className={styles.guidereviewIcon} alt="" src={guideIcon} />
        </div>
      </div>
    </div>
  );
};

export default ClassHeader;
