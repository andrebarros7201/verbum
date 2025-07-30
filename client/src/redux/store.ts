import { configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./slices/userSlice.ts";

const store = configureStore({
  reducer: { user: userSlice.reducer },
});

export { store };
