import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'

export interface syllabusDetailState {
  data: any
}

const initialState: syllabusDetailState = {
  data: 0,
}

export const syllabusDetailSlice = createSlice({
  name: 'syllabusDetail',
  initialState,
  reducers: {
    set: (state, action: PayloadAction<any>) => {
      state.data = action.payload
    },
  },
})

// Action creators are generated for each case reducer function
export const { set } = syllabusDetailSlice.actions

export default syllabusDetailSlice.reducer