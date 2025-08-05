import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import type { ICurrentPostSlice } from "../../interfaces/ICurrentPostSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { IPostComplete } from "../../interfaces/IPostComplete.ts";
import axios, { type AxiosError } from "axios";
import type { ICommentSimple } from "../../interfaces/ICommentSimple.ts";
import type { RootState } from "../store.ts";

const initialState: ICurrentPostSlice = {
  post: null,
  isLoading: false,
};

const fetchCurrentPost = createAsyncThunk<
  { post: IPostComplete },
  { id: number },
  { rejectValue: { notification: IReturnNotification } }
>("currentPost/fetch", async ({ id }, { rejectWithValue }) => {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_SERVER_URL}/api/post/${id}`,
      {
        withCredentials: true,
      },
    );
    return { post: response.data };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";
    return rejectWithValue({
      notification: { type: "error", message },
    });
  }
});

const votePost = createAsyncThunk<
  { id: number; value: number; post: IPostComplete },
  { id: number; value: number },
  { rejectValue: { notification: IReturnNotification } }
>("post/vote", async ({ id, value }, { rejectWithValue }) => {
  try {
    const type = value === 1 ? "like" : "dislike";
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/post/${id}/${type}`,
      {},
      { withCredentials: true },
    );

    return { id, value, post: response.data };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";
    return rejectWithValue({
      notification: { type: "error", message },
    });
  }
});

const addComment = createAsyncThunk<
  { comment: ICommentSimple },
  { text: string },
  { rejectValue: { notification: IReturnNotification }; state: RootState }
>("post/AddComment", async ({ text }, { rejectWithValue, getState }) => {
  try {
    const state = getState();
    const { post } = state.currentPost;
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/${post!.id}/comment`,
      { text },
      { withCredentials: true },
    );

    return { comment: response.data };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";
    return rejectWithValue({
      notification: { type: "error", message },
    });
  }
});
const voteComment = createAsyncThunk<
  { id: number; value: number; comment: ICommentSimple },
  { id: number; value: number },
  { rejectValue: { notification: IReturnNotification } }
>("post/commentVote", async ({ id, value }, { rejectWithValue }) => {
  try {
    const type = value === 1 ? "like" : "dislike";
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/comment/${id}/${type}`,
      {},
      { withCredentials: true },
    );

    return { id, value, comment: response.data };
  } catch (e) {
    const error = e as AxiosError<string>;
    const message = error.response?.data || "Something went wrong";
    return rejectWithValue({
      notification: { type: "error", message },
    });
  }
});

const currentPostSlice = createSlice({
  name: "currentPost",
  initialState,
  reducers: {
    clearCurrentPost: (state) => {
      state.post = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCurrentPost.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchCurrentPost.fulfilled, (state, action) => {
        state.isLoading = false;
        state.post = action.payload.post;
      })
      .addCase(fetchCurrentPost.rejected, (state) => {
        state.isLoading = false;
        state.post = null;
      })
      // Add Comment
      //.addCase(addComment.pending, (state) => {})
      .addCase(addComment.fulfilled, (state, action) => {
        state.isLoading = false;
        state.post!.comments.push(action.payload.comment);
      })
      .addCase(addComment.rejected, (state) => {
        state.isLoading = false;
      })
      // Post vote
      //.addCase(votePost.pending, (state) => {})
      .addCase(votePost.fulfilled, (state, action) => {
        state.isLoading = false;
        state.post!.votes = action.payload.post.votes;
      })
      .addCase(votePost.rejected, (state) => {
        state.isLoading = false;
      })

      // Comment vote
      //.addCase(voteComment.pending, (state) => {})
      .addCase(voteComment.fulfilled, (state, action) => {
        state.isLoading = false;
        const commentIndex = state.post!.comments.findIndex(
          (c) => c.id === action.payload.id,
        );

        state.post!.comments[commentIndex] = action.payload.comment;
      })
      .addCase(voteComment.rejected, (state) => {
        state.isLoading = false;
      });
  },
});
const { clearCurrentPost } = currentPostSlice.actions;
export {
  currentPostSlice,
  addComment,
  votePost,
  voteComment,
  fetchCurrentPost,
  clearCurrentPost,
};
