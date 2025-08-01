import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { ICommunitySimple } from "../../interfaces/ICommunitySimple.ts";
import axios, { AxiosError } from "axios";
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

const joinCommunity = createAsyncThunk<
  { notification: IReturnNotification; id: number },
  { id: number },
  { rejectValue: { notification: IReturnNotification } }
>("community/join", async ({ id }, { rejectWithValue }) => {
  try {
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/community/${id}/join`,
      {},
      {
        withCredentials: true,
      },
    );
    return {
      notification: { type: "success", message: response.data },
      id,
    };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";

    return rejectWithValue({
      notification: { type: "error", message },
    });
  }
});

const leaveCommunity = createAsyncThunk<
  { notification: IReturnNotification; id: number },
  { id: number },
  { rejectValue: { notification: IReturnNotification } }
>("community/leave", async ({ id }, { rejectWithValue }) => {
  try {
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/community/${id}/leave`,
      {},
      {
        withCredentials: true,
      },
    );
    return { notification: { type: "success", message: response.data }, id };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";

    return rejectWithValue({
      notification: { type: "error", message },
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
      })

      // Join Community
      .addCase(joinCommunity.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(joinCommunity.fulfilled, (state, action) => {
        state.isLoading = false;
        const communityIndex = state.communities.findIndex(
          (x) => x.id === action.payload.id,
        );

        state.communities[communityIndex].isMember = true;
        state.communities[communityIndex].membersCount++;
      })
      .addCase(joinCommunity.rejected, (state) => {
        state.isLoading = false;
      })

      // Leave Community
      .addCase(leaveCommunity.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(leaveCommunity.fulfilled, (state, action) => {
        state.isLoading = false;
        const communityIndex = state.communities.findIndex(
          (x) => x.id === action.payload.id,
        );

        state.communities[communityIndex].isMember = false;
        state.communities[communityIndex].membersCount--;
      })
      .addCase(leaveCommunity.rejected, (state) => {
        state.isLoading = false;
      });
  },
});

export { communitySlice, fetchAllCommunities, leaveCommunity, joinCommunity };
