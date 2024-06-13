import axios from "axios";

axios.interceptors.request.use(
	function (config) {
		// Add interceptor to add extra Header
		config.headers["Authorization"] = `Bearer ${localStorage.getItem("token")}`;
		config.headers["RefreshToken"] = localStorage.getItem("refreshToken");
		return config;
	},
	function (error) {

		// document.dispatchEvent(
		// 	new CustomEvent("globalauthevent", {
		// 		detail: {
		// 			invalid_session: true,
		// 		},
		// 		bubbles: true,
		// 		cancelable: true,
		// 		composed: false,
		// 	})
		// );

		// Do something with request error
		return Promise.reject(error);
	}
);

export default axios;
