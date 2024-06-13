import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import style from "./style.module.scss";
import logoImg from "../../../assets/LogoStManagement/logo.png";
import uniGateLogoImg from "../../../assets/LogoStManagement/image-2.png";
import { Bounce, toast } from "react-toastify";

export default function Navbar() {
	const navigate = useNavigate();
	const location = useLocation();
	const [avatarUrl, setAvatarUrl] = useState("");
	const token = localStorage.getItem("token");
	const username = localStorage.getItem("username");
	const pathIsNotLogin = location.pathname !== "/login";
	const pathRecover = location.pathname === "/recover-password";

	useEffect(() => {
		if (!token && pathIsNotLogin && !pathRecover) {
			navigate("/login");
		} else if (token && username) {
			setAvatarUrl(`https://api.dicebear.com/8.x/pixel-art/svg?seed=${encodeURIComponent(username)}`);
		}
	}, [navigate, location.pathname, username]);

	const handleLogout = () => {
		localStorage.removeItem("token");
		localStorage.removeItem("refreshToken");
		localStorage.removeItem("username");
		localStorage.clear();
		toast.info("Logged out", {
			position: "top-right",
			autoClose: 3000,
			hideProgressBar: false,
			closeOnClick: true,
			pauseOnHover: true,
			draggable: true,
			progress: undefined,
			theme: "light",
			transition: Bounce,
		});
		navigate("/login");
	};

	return (
		<div className={style.main}>
			<img src={logoImg} alt="WorldWise logo" />
			<div>
				<div>
					<img src={uniGateLogoImg} alt="uniGate logo" className={style.logo} />
					<span>uniGate</span>
				</div>

				{token && (
					<div className={style.userContainer}>
						<img src={avatarUrl} alt="User avatar" className={`${style.logo} ${style.avatar}`} onClick={() => navigate("/profile")} style={{ cursor: "pointer" }} />
						<div className={style.dropdownMenu}>
							<button onClick={() => navigate("/profile")} style={{ cursor: "pointer" }}>
								View Profile
							</button>
							<button style={{ cursor: "pointer" }} onClick={handleLogout}>
								Log out
							</button>
						</div>
					</div>
				)}
			</div>
		</div>
	);
}
