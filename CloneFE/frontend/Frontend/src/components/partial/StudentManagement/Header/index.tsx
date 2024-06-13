import { FunctionComponent } from "react";
import styles from "./style.module.scss";
import React from "react";

const Header: FunctionComponent = () => {
  return (
    <header className={styles.header}>
      <div className={styles.logoParent}>
        <img
          className={styles.logoIcon}
          loading="eager"
          alt=""
          src="/logo@2x.png"
        />
        <div className={styles.frameParent}>
          <button className={styles.frameWrapper}>
            <div className={styles.image2Parent}>
              <img className={styles.image2Icon} alt="" src="/image-2@2x.png" />
              <div className={styles.unigate}>uniGate</div>
            </div>
          </button>
          <div className={styles.frameGroup}>
            <img
              className={styles.frameChild}
              loading="eager"
              alt=""
              src="/group-8@2x.png"
            />
            <div className={styles.warriorTranParent}>
              <b className={styles.warriorTran}>Warrior Tran</b>
              <div className={styles.logOut}>Log out</div>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
