import { configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./slices/userSlice.ts";
import { notificationSlice } from "./slices/notificationSlice.ts";
import { communitySlice } from "./slices/communitySlice.ts";
import { currentCommunitySlice } from "./slices/currentCommunitySlice.ts";
import { currentPostSlice } from "./slices/currentPostSlice.ts";

const store = configureStore({
  reducer: {
    user: userSlice.reducer,
    notification: notificationSlice.reducer,
    community: communitySlice.reducer,
    currentCommunity: currentCommunitySlice.reducer,
    currentPost: currentPostSlice.reducer,
  },
});

export { store };

export type RootState = ReturnType<typeof store.getState>;
export type RootDispatch = typeof store.dispatch;
