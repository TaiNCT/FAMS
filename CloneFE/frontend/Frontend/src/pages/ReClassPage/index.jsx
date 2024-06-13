import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar"
import Footer from "../../components/layouts/Footer";
import ReClass from "../../components/partial/ReservationManagement/ReClass"
import styles from "../../pages/ReClassPage/style.module.scss"
export default function AddNewReservedClassPage() {
  return (
    <div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
      <Navbar />
      <div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
        <Sidebar />
        <div className="flex-1 overflow-y-scroll">
          <div className={styles.header}>
            <p>Reserving details</p>
          </div>
          <ReClass />
        </div>
      </div>
      <Footer />
    </div>
  );
}
