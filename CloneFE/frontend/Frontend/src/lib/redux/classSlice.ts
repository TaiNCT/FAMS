import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface ClassState {
  data: any;
  trainingProgram: any;
  SyllabusList: any[];
  attendeeTypes: any[];
}
const initialState: ClassState = {
  data: null,
  trainingProgram: null,
  SyllabusList: [],
  attendeeTypes: [],
};
export const classSlice = createSlice({
  name: "class",
  initialState,
  reducers: {
    set: (state, action: PayloadAction<any>) => {
      state.data = action.payload;
    },
    setTrainingProgram: (state, action: PayloadAction<any>) => {
      state.trainingProgram = action.payload;
    },
    setSyllabusList: (state, action: PayloadAction<any[]>) => {
      state.SyllabusList = action.payload;
    },
    setAttendeeTypes: (state, action: PayloadAction<any[]>) => {
      state.attendeeTypes = action.payload;
    },
  },
});
export const { set, setTrainingProgram, setSyllabusList, setAttendeeTypes } = classSlice.actions;
export default classSlice.reducer;
