import { create } from "zustand";

const useAuthStore = create((set) => {
	return {
		IsValidUser: true,
		enable: () =>
			set(() => {
				IsValidUser: true;
			}),
		disable: () =>
			set(() => {
				IsValidUser: false;
			}),
	};
});

export default useAuthStore;
