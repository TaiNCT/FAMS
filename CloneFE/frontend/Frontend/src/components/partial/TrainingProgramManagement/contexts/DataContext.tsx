import {createContext, useContext} from 'react';
import { TrainingProgram } from '../TrainingProgramList/models/TrainingProgram.model';

export type GlobalContent = {
    isSidebarOpen: boolean
    setIsSidebarOpen:(b: boolean) => void,
    trainingProgramData: TrainingProgram,
    setTrainingProgramData:(tp: TrainingProgram) => void,
    refreshData: () => void
}

export const MyGlobalContext = createContext<GlobalContent>({
    isSidebarOpen: true,
    setIsSidebarOpen: () => {},
    trainingProgramData: null!,
    setTrainingProgramData: () => {},
    refreshData: () => {}
});

export type TrainingProgramData  = {
    setData: (c: TrainingProgram[]) => void
    setTotalPages: (c: number) => void
    setCurrentPage: (c: number) => void
    setRowsPerPageOption: (c: number[]) => void
    currentPage: number
    rowsPerPage: number
}

export const TrainingProgramDataContext = createContext<TrainingProgramData>({
    setData: () => {},
    setTotalPages: () => {},
    setCurrentPage: () => {},
    setRowsPerPageOption: () => {},
    currentPage: 1,
    rowsPerPage: 5,
})

export const useGlobalContext = () => useContext(MyGlobalContext); 
export const useTrainingProgramDataContext = () => useContext(TrainingProgramDataContext); 
