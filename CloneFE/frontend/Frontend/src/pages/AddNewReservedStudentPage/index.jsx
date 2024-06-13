import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar"
import Footer from "../../components/layouts/Footer";
import styles from "../../pages/AddNewReservedStudentPage/style.module.scss"

import AddNewStudentReserve from "../../components/partial/ReservationManagement/AddNewStudentReserve"
export default function AddNewReservedClassPage() {
  return (
    <div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
      <Navbar />
      <div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
        <Sidebar />
        <div className="flex-1 overflow-y-scroll">
          <div className={styles.header}>
            <p>Add New Reserved Student</p>
          </div>
          <AddNewStudentReserve />
        </div>
      </div>
      <Footer />
    </div>
  );
}
