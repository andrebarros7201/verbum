import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { INotification } from "../../interfaces/INotification.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

const initialState: INotification = {
  message: null,
  type: null,
  isVisible: false,
};

const notificationSlice = createSlice({
  name: "notification",
  initialState,
  reducers: {
    setNotification: (state, action: PayloadAction<IReturnNotification>) => {
      state.message = action.payload.message;
      state.type = action.payload.type;
      state.isVisible = true;
    },
    clearNotification: (state) => {
      state.message = null;
      state.type = null;
      state.isVisible = false;
    },
  },
});

const { setNotification, clearNotification } = notificationSlice.actions;
export { notificationSlice, setNotification, clearNotification };
