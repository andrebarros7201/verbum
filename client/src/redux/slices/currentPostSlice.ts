import { createSlice } from "@reduxjs/toolkit";
import type { ICurrentPostSlice } from "../../interfaces/ICurrentPostSlice.ts";

const initialState: ICurrentPostSlice = {
  post: null,
  isLoading: false,
};

const currentPostSlice = createSlice({
  name: "currentPost",
  initialState,
  reducers: {
    clearCurrentPost: (state) => {
      state.post = null;
    },
  },
});

export { currentPostSlice };
