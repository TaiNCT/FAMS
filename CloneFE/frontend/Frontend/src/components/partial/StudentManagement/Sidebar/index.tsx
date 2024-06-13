import { FunctionComponent } from "react";
import styles from "./style.module.scss";
import React from "react";

const Sidebar: FunctionComponent = () => {
  return (
    <div className={styles.sidebar}>
      <div className={styles.menuWrapper}>
        <img
          className={styles.menuIcon}
          loading="eager"
          alt=""
          src="/menu.svg"
        />
      </div>
      <div className={styles.home}>
        <img
          className={styles.homeIcon}
          loading="eager"
          alt=""
          src="/home.svg"
        />
        <div className={styles.home1}>Home</div>
      </div>
      <div className={styles.syllabus}>
        <div className={styles.syllabus1}>
          <div className={styles.groupParent}>
            <img
              className={styles.groupIcon}
              loading="eager"
              alt=""
              src="/group.svg"
            />
            <div className={styles.students}>Students</div>
          </div>
          <img
            className={styles.naviOpenIcon}
            loading="eager"
            alt=""
            src="/navi-open@2x.png"
          />
        </div>
        <div className={styles.viewSyllabus}>
          <img className={styles.arrowForwardIosIcon} alt="" />
          <div className={styles.studentList}>Student list</div>
        </div>
        <div className={styles.viewSyllabus1}>
          <img className={styles.arrowForwardIosIcon1} alt="" />
          <input
            className={styles.reserveList}
            placeholder="Reserve list"
            type="text"
          />
        </div>
      </div>
      <div className={styles.syllabus2}>
        <div className={styles.syllabus3}>
          <div className={styles.bookOpenParent}>
            <div className={styles.bookOpen}>
              <img
                className={styles.filterBarIcon}
                alt=""
                src="/filter-bar.svg"
              />
              <img
                className={styles.searchInputIcon}
                alt=""
                src="/vector-1.svg"
              />
            </div>
            <div className={styles.syllabus4}>Syllabus</div>
          </div>
          <img
            className={styles.naviOpenIcon1}
            alt=""
            src="/navi-open@2x.png"
          />
        </div>
        <div className={styles.viewSyllabus2}>
          <img className={styles.arrowForwardIosIcon2} alt="" />
          <div className={styles.viewSyllabus3}>View syllabus</div>
        </div>
        <div className={styles.createSyllabus}>
          <img className={styles.arrowForwardIosIcon3} alt="" />
          <div className={styles.createSyllabus1}>Create syllabus</div>
        </div>
      </div>
      <div className={styles.trainingModule}>
        <div className={styles.trainingModule1}>
          <div className={styles.biotechParent}>
            <img
              className={styles.biotechIcon}
              loading="eager"
              alt=""
              src="/biotech.svg"
            />
            <div className={styles.trainingProgram}>Training program</div>
          </div>
          <img
            className={styles.naviOpenIcon2}
            alt=""
            src="/navi-open@2x.png"
          />
        </div>
        <div className={styles.viewModule}>
          <img className={styles.arrowForwardIosIcon4} alt="" />
          <div className={styles.viewProgram}>View program</div>
        </div>
        <div className={styles.createModule}>
          <img className={styles.arrowForwardIosIcon5} alt="" />
          <div className={styles.createProgram}>Create program</div>
        </div>
      </div>
      <div className={styles.classMaster}>
        <div className={styles.class}>
          <div className={styles.schoolParent}>
            <img className={styles.schoolIcon} alt="" src="/school.svg" />
            <div className={styles.class1}>Class</div>
          </div>
          <img
            className={styles.naviOpenIcon3}
            alt=""
            src="/navi-open-3@2x.png"
          />
        </div>
        <div className={styles.viewClass}>
          <img className={styles.arrowForwardIosIcon6} alt="" />
          <div className={styles.viewClass1}>View class</div>
        </div>
        <div className={styles.createClass}>
          <img className={styles.arrowForwardIosIcon7} alt="" />
          <div className={styles.createClass1}>Create class</div>
        </div>
      </div>
      <div className={styles.trainingCalendar}>
        <div className={styles.calendarTodayParent}>
          <img
            className={styles.calendarTodayIcon}
            alt=""
            src="/calendar-today.svg"
          />
          <div className={styles.trainingCalendar1}>Training calendar</div>
        </div>
        <img
          className={styles.naviOpenIcon4}
          alt=""
          src="/navi-open-4@2x.png"
        />
      </div>
      <div className={styles.userManagement}>
        <div className={styles.users}>
          <div className={styles.roleParent}>
            <img
              className={styles.roleIcon}
              loading="eager"
              alt=""
              src="/role.svg"
            />
            <div className={styles.userManagement1}>User management</div>
          </div>
          <img
            className={styles.naviOpenIcon5}
            alt=""
            src="/navi-open@2x.png"
          />
        </div>
        <div className={styles.userList}>
          <img className={styles.arrowForwardIosIcon8} alt="" />
          <div className={styles.userList1}>User list</div>
        </div>
        <div className={styles.userPermission}>
          <img className={styles.arrowForwardIosIcon9} alt="" />
          <div className={styles.userPermission1}>User permission</div>
        </div>
      </div>
      <div className={styles.learningMaterials}>
        <div className={styles.folderParent}>
          <img
            className={styles.folderIcon}
            loading="eager"
            alt=""
            src="/folder.svg"
          />
          <div className={styles.learningMaterials1}>Learning materials</div>
        </div>
        <img
          className={styles.naviOpenIcon6}
          alt=""
          src="/navi-open-4@2x.png"
        />
      </div>
      <div className={styles.setting}>
        <div className={styles.setting1}>
          <div className={styles.settingsParent}>
            <img
              className={styles.settingsIcon}
              loading="eager"
              alt=""
              src="/settings.svg"
            />
            <div className={styles.setting2}>Setting</div>
          </div>
          <img
            className={styles.naviOpenIcon7}
            alt=""
            src="/navi-open@2x.png"
          />
        </div>
        <div className={styles.calendar}>
          <img className={styles.arrowForwardIosIcon10} alt="" />
          <div className={styles.calendar1}>Calendar</div>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
