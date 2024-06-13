import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface UserState {
  admins: any[];
}

const initialState: UserState = {
  admins: [],
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setAdmins: (state, action: PayloadAction<any>) => {
      state.admins = action.payload;
    },
  },
});

export const { setAdmins } = userSlice.actions;
export default userSlice.reducer;
