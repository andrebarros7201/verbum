import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { ICurrentCommunitySlice } from "../../interfaces/ICurrentCommunitySlice.ts";
import axios from "axios";

const initialState: ICurrentCommunitySlice = {
  isLoading: false,
  community: null,
};

const fetchCurrentCommunity = createAsyncThunk(
  "currentCommunity/fetch",
  async ({ id }: { id: number }, { rejectWithValue }) => {
    try {
      const response = await axios.get(
        `${import.meta.env.VITE_SERVER_URL}/api/community/${id}`,
        {
          withCredentials: true,
        },
      );
      return response.data;
    } catch (e) {
      return rejectWithValue({
        notification: { type: "error", message: "Failed to load community" },
      });
    }
  },
);

const currentCommunitySlice = createSlice({
  name: "currentCommunity",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCurrentCommunity.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchCurrentCommunity.fulfilled, (state, action) => {
        state.isLoading = false;
        state.community = action.payload;
      })
      .addCase(fetchCurrentCommunity.rejected, (state) => {
        state.isLoading = false;
        state.community = null;
      });
  },
});

export { currentCommunitySlice, fetchCurrentCommunity };
