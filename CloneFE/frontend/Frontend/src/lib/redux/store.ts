import { combineReducers, configureStore } from '@reduxjs/toolkit';
import classReducer from './classSlice';
import counterReducer from './counterSlice';
import sidebarReducer from './sidebarSlice';
import syllabusDetailReducer from './SyllabusDetailSlice';
import userReducer from './userSlice';
import fsuReducer from "./fsuSlice";
import trainingProgramReducer from './trainingProgramSlice';
const rootReducer = combineReducers({
  class: classReducer,
  counter: counterReducer,
  sidebar: sidebarReducer,
  syllabusDetail: syllabusDetailReducer,
  user: userReducer,
  fsu: fsuReducer,
  trainingPrograms: trainingProgramReducer,
});

export const store = configureStore({
  reducer: rootReducer,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
