/*
     A very simple global store, which can be used for state management
     across different contexts, pages and components. For now it's just an example but it will get complicated sooner or later

     Reference Zustand to learn more : https://github.com/pmndrs/zustand
*/

import { create } from "zustand";

const store = create((set) => ({
  isreset: 0,
  reset: () => set((state) => ({ isreset: state.isreset + 1 })),
  data: null,
  increase_global_data: () => set((state) => ({ data: state.data + 1 })),
  reset_global_data: () => set({ data: null }),
}));

/*
  Typescript version :
  -----
  import create from 'zustand';

  interface StoreState {
    data: number | null;
    increase_global_data: () => void;
    reset_global_data: () => void;
  }

  const store = create<StoreState>((set) => ({
    data: null,
    increase_global_data: () => set((state) => ({ data: state.data !== null ? state.data + 1 : 1 })),
    reset_global_data: () => set({ data: null }),
  }));

  -----
*/
