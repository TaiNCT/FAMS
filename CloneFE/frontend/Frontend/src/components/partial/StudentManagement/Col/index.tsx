import { FunctionComponent, useMemo, type CSSProperties } from "react";
import styles from "./style.module.scss";
import React from "react";

type ColType = {
  thPlaceholder?: string;
  fullName?: string;
  fullName1?: string;
  fullName2?: string;
  fullName3?: string;
  fullName4?: string;
  fullName5?: string;
  fullName6?: string;
  fullName7?: string;
  fullName8?: string;
  fullName9?: string;

  /** Style props */
  propWidth?: CSSProperties["width"];
  propMinWidth?: CSSProperties["minWidth"];
};

const Col: FunctionComponent<ColType> = ({
  thPlaceholder,
  fullName,
  fullName1,
  fullName2,
  fullName3,
  fullName4,
  fullName5,
  fullName6,
  fullName7,
  fullName8,
  fullName9,
  propWidth,
  propMinWidth,
}) => {
  const colStyle: CSSProperties = useMemo(() => {
    return {
      width: propWidth,
    };
  }, [propWidth]);

  const thStyle: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth,
    };
  }, [propMinWidth]);

  return (
    <div className={styles.col} style={colStyle}>
      <input
        className={styles.th}
        placeholder={thPlaceholder}
        type="text"
        style={thStyle}
      />
      <div className={styles.row}>
        <div className={styles.tr}>
          <b className={styles.fullName}>{fullName}</b>
        </div>
      </div>
      <div className={styles.row1}>
        <div className={styles.tr1}>
          <b className={styles.fullName1}>{fullName1}</b>
        </div>
      </div>
      <div className={styles.row2}>
        <div className={styles.tr2}>
          <b className={styles.fullName2}>{fullName2}</b>
        </div>
      </div>
      <div className={styles.row3}>
        <div className={styles.tr3}>
          <b className={styles.fullName3}>{fullName3}</b>
        </div>
      </div>
      <div className={styles.row4}>
        <div className={styles.tr4}>
          <b className={styles.fullName4}>{fullName4}</b>
        </div>
      </div>
      <div className={styles.row5}>
        <div className={styles.tr5}>
          <b className={styles.fullName5}>{fullName5}</b>
        </div>
      </div>
      <div className={styles.row6}>
        <div className={styles.tr6}>
          <b className={styles.fullName6}>{fullName6}</b>
        </div>
      </div>
      <div className={styles.row7}>
        <div className={styles.tr7}>
          <b className={styles.fullName7}>{fullName7}</b>
        </div>
      </div>
      <div className={styles.row8}>
        <div className={styles.tr8}>
          <b className={styles.fullName8}>{fullName8}</b>
        </div>
      </div>
      <div className={styles.row9}>
        <div className={styles.tr9}>
          <b className={styles.fullName9}>{fullName9}</b>
        </div>
      </div>
    </div>
  );
};

export default Col;
