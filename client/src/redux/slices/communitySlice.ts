import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { ICommunitySimple } from "../../interfaces/ICommunitySimple.ts";
import axios from "axios";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { ICommunitySlice } from "../../interfaces/ICommunitySlice.ts";

const initialState: ICommunitySlice = {
  communities: [],
  isLoading: false,
};

const fetchAllCommunities = createAsyncThunk<
  {
    communities: ICommunitySimple[];
  },
  void,
  { rejectValue: { notification: IReturnNotification } }
>("community/fetchAll", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_SERVER_URL}/api/community`,
      {
        withCredentials: true,
      },
    );

    return { communities: response.data };
  } catch (e) {
    return rejectWithValue({
      notification: { type: "error", message: "Failed to load communities" },
    });
  }
});

const communitySlice = createSlice({
  name: "community",
  initialState,
  reducers: {
    clearCommunities: (state) => {
      state.communities = [];
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllCommunities.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchAllCommunities.fulfilled, (state, action) => {
        state.isLoading = false;
        state.communities = action.payload.communities;
      })
      .addCase(fetchAllCommunities.rejected, (state) => {
        state.isLoading = false;
        state.communities = [];
      });
  },
});

export { communitySlice, fetchAllCommunities };
