import { createSlice } from '@reduxjs/toolkit'

// Define a type for the slice state
interface SidebarState {
  isOpen: boolean
}

// Define the initial state using that type
const initialState: SidebarState = {
  isOpen: true,
}

export const sidebarSlice = createSlice({
  name: 'sidebar',
  // `createSlice` will infer the state type from the `initialState` argument
  initialState,
  reducers: {
    toggleSidebar: (state) => {
      state.isOpen = !state.isOpen
    },
  },
})

export const { toggleSidebar } = sidebarSlice.actions

export default sidebarSlice.reducer