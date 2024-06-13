import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import SendEmailPopupForm from "../../components/partial/EmailInformRemind/SendEmailPopupForm";
import ViewListStudent from "../../components/partial/ReservationManagement/ViewListStudent";
import { useState, createContext } from "react";
import styles from "../../pages/ReservedListPage/style.module.scss"

export const reserveContext = createContext();

export default function ReservedListPage() {
	const [open, setopen] = useState(false);

	return (
		<reserveContext.Provider value={{ setopen: setopen }}>
			<div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
				<Navbar />
				<div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
					<Sidebar />
					<div className="flex-1 overflow-y-scroll">
						<div className={styles.header}>
							<p>Reserve List</p>
						</div>
						<ViewListStudent />
					</div>
				</div>
				<Footer />
			</div>
		</reserveContext.Provider>
	);
}
