import { create } from "zustand";
const useRoleStore = create((set) => {
    return {
        roles: [],
        setRole: (roles) => set({roles}),
    };
});

export default useRoleStore;