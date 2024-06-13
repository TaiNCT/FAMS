import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface trainingProgramState {
  trainingPrograms: any[];
}

const initialState: trainingProgramState = {
    trainingPrograms: [],
};

export const trainingProgramSlice = createSlice({
  name: "trainingPrograms",
  initialState,
  reducers: {
    setTrainingPrograms: (state, action: PayloadAction<any>) => {
      state.trainingPrograms = action.payload;
    },
  },
});

export const { setTrainingPrograms } = trainingProgramSlice.actions;
export default trainingProgramSlice.reducer;
