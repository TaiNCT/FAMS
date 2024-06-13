import Sidebar from "../../components/layouts/Sidebar";
import Navbar from "../../components/layouts/Navbar/index";
import EmailTemplateList from "../../components/partial/EmailInformRemind/EmailTable/EmailTemplateList";
import Footer from "../../components/layouts/Footer";
// import EmailTemplateList from "components/partial/EmailInformRemind/EmailTable/EmailTemplateList";

import { createContext, useState } from "react";

export const emailContext = createContext(null);

export function EmailConfigurationPage() {
	const [refresh, setRefresh] = useState(0);

	return (
		<emailContext.Provider
			value={{
				refresh: refresh,
				setRefresh: setRefresh,
			}}
		>
			<div className="font-inter h-screen flex flex-col disable-scrollbar">
				<Navbar />
				<div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
					<Sidebar />
					<div className="flex-1 overflow-y-scroll">
						<EmailTemplateList />
					</div>
				</div>
				<Footer />
			</div>
		</emailContext.Provider>
	);
}
