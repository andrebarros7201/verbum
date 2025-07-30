import type { IUserSlice } from "../../interfaces/IUserSlice.ts";
import { createSlice } from "@reduxjs/toolkit";

const initialState: IUserSlice = {
  isAuthenticated: false,
  user: null,
  isLoading: false,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {},
  // extraReducers: (builder) => {},
});

export { userSlice };
