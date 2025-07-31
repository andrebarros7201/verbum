import { configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./slices/userSlice.ts";
import { notificationSlice } from "./slices/notificationSlice.ts";
import { communitySlice } from "./slices/CommunitySlice.ts";

const store = configureStore({
  reducer: {
    user: userSlice.reducer,
    notification: notificationSlice.reducer,
    community: communitySlice.reducer,
  },
});

export { store };

export type RootState = ReturnType<typeof store.getState>;
export type RootDispatch = typeof store.dispatch;
