import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface fsuState {
  fsus: any[];
}

const initialState: fsuState = {
    fsus: [],
};

export const fsuSllice = createSlice({
  name: "fsu",
  initialState,
  reducers: {
    setFsus: (state, action: PayloadAction<any>) => {
      state.fsus = action.payload;
    },
  },
});

export const { setFsus } = fsuSllice.actions;
export default fsuSllice.reducer;
