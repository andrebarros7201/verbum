import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { ICurrentCommunitySlice } from "../../interfaces/ICurrentCommunitySlice.ts";
import axios, { type AxiosError } from "axios";
import type { ICommunityComplete } from "../../interfaces/ICommunityComplete.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { IPostSimple } from "../../interfaces/IPostSimple.ts";
import type { RootState } from "../store.ts";

const initialState: ICurrentCommunitySlice = {
  isLoading: false,
  community: null,
};

const fetchCurrentCommunity = createAsyncThunk<
  { community: ICommunityComplete },
  { id: number },
  { rejectValue: { notification: IReturnNotification } }
>("currentCommunity/fetch", async ({ id }, { rejectWithValue }) => {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_SERVER_URL}/api/community/${id}`,
      {
        withCredentials: true,
      },
    );
    return { community: response.data };
  } catch (e) {
    return rejectWithValue({
      notification: { type: "error", message: "Failed to load community" },
    });
  }
});

const createPost = createAsyncThunk<
  { post: IPostSimple; notification: IReturnNotification },
  { title: string; text: string },
  { rejectValue: { notification: IReturnNotification }; state: RootState }
>(
  "currentCommunity/createPost",
  async ({ title, text }, { rejectWithValue, getState }) => {
    try {
      const state = getState();
      const { community } = state.currentCommunity;
      const response = await axios.post(
        `${import.meta.env.VITE_SERVER_URL}/api/post/`,
        { title, text, communityId: community!.id },
        {
          withCredentials: true,
        },
      );

      return {
        notification: {
          type: "success",
          message: "Post Created",
        },
        post: response.data,
      };
    } catch (e) {
      const error = e as AxiosError<string>;
      return rejectWithValue({
        notification: {
          type: "error",
          message: error.response?.data || "Failed to load community",
        },
      });
    }
  },
);
const toggleMembership = createAsyncThunk<
  { id: number; notification: IReturnNotification },
  { id: number; isMember: boolean },
  { rejectValue: { notification: IReturnNotification } }
>(
  "currentCommunity/toggleMembership",
  async ({ id, isMember }, { rejectWithValue }) => {
    try {
      const action = isMember ? "leave" : "join";
      await axios.post(
        `${import.meta.env.VITE_SERVER_URL}/api/community/${id}/${action}`,
        {},
        {
          withCredentials: true,
        },
      );
      return {
        id: id,
        notification: {
          type: "success",
          message: isMember ? "Left community" : "Joined community",
        },
      };
    } catch (e) {
      const error = e as AxiosError<string>;
      return rejectWithValue({
        notification: {
          type: "error",
          message: error.response?.data || "Failed to load community",
        },
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
      // Fetch Current Community
      .addCase(fetchCurrentCommunity.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchCurrentCommunity.fulfilled, (state, action) => {
        state.isLoading = false;
        state.community = action.payload.community;
      })
      .addCase(fetchCurrentCommunity.rejected, (state) => {
        state.isLoading = false;
        state.community = null;
      })
      // Create Post
      .addCase(createPost.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(createPost.fulfilled, (state, action) => {
        state.isLoading = false;
        state.community!.posts.push(action.payload.post);
      })
      .addCase(createPost.rejected, (state) => {
        state.isLoading = false;
      })
      // Toggle Membership
      .addCase(toggleMembership.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(toggleMembership.fulfilled, (state) => {
        state.isLoading = false;
        state.community!.isMember = !state.community!.isMember;
      })
      .addCase(toggleMembership.rejected, (state) => {
        state.isLoading = false;
      });
  },
});

export {
  currentCommunitySlice,
  fetchCurrentCommunity,
  toggleMembership,
  createPost,
};
