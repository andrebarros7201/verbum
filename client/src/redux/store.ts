import { configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./slices/userSlice.ts";
import { notificationSlice } from "./slices/notificationSlice.ts";

const store = configureStore({
  reducer: { user: userSlice.reducer, notification: notificationSlice.reducer },
});

export { store };

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
