import { create } from "zustand";
const useRolePermissionStore = create((set) => {
    return {
        perms: [],
        setPerms: (perms) => set({perms}),
    };
});

export default useRolePermissionStore;