import ClassHeader from "../ClassHeader";
import SyllabusTab from "../Syllabus_tab";
import ActivityLogTable from "./components/Table";
// import styles from "./style.module.scss";

export default function ActivityLogList() {
  return (
    <div>
      <ClassHeader />
      <SyllabusTab />
      <ActivityLogTable />
    </div>
  );
}
