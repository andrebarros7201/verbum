import type { IUserSlice } from "../../interfaces/IUserSlice.ts";
import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import type { IUser } from "../../interfaces/IUser.ts";
import axios, { AxiosError } from "axios";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { ICommunitySimple } from "../../interfaces/ICommunitySimple.ts";
import type { ICommentSimple } from "../../interfaces/ICommentSimple.ts";
import type { IPostSimple } from "../../interfaces/IPostSimple.ts";

const initialState: IUserSlice = {
  isAuthenticated: false,
  user: null,
  isLoading: false,
  communities: [],
  posts: [],
  comments: [],
};

const userRegister = createAsyncThunk<
  { notification: IReturnNotification },
  { username: string; password: string },
  { rejectValue: { notification: IReturnNotification } }
>("user/login", async ({ username, password }, { rejectWithValue }) => {
  try {
    await axios.post(`${import.meta.env.VITE_SERVER_URL}/api/auth/register`, {
      username,
      password,
    });
    return {
      notification: { type: "success", message: "Registration successful" },
    };
  } catch (e) {
    const error = e as AxiosError;
    return rejectWithValue({
      notification: {
        type: "error",
        message: (error.response?.data as string) || "Registration failed",
      },
    });
  }
});

const userLogin = createAsyncThunk<
  { user: IUser; notification: IReturnNotification },
  { username: string; password: string },
  { rejectValue: { notification: IReturnNotification } }
>("user/login", async ({ username, password }, { rejectWithValue }) => {
  try {
    const response = await axios.post(
      `${import.meta.env.VITE_SERVER_URL}/api/auth/login`,
      {
        username,
        password,
      },
      {
        withCredentials: true,
      },
    );
    return {
      user: response.data,
      notification: { type: "success", message: "Login successful" },
    };
  } catch (e) {
    const error = e as AxiosError;
    return rejectWithValue({
      notification: {
        type: "error",
        message: (error.response?.data as string) || "Login failed",
      },
    });
  }
});

const userMe = createAsyncThunk<
  {
    posts: IPostSimple[];
    comments: ICommentSimple[];
    communities: ICommunitySimple[];
  },
  void,
  { rejectValue: { notification: IReturnNotification } }
>("user/Me", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_SERVER_URL}/api/user/me`,
      {
        withCredentials: true,
      },
    );
    return {
      posts: response.data.posts,
      comments: response.data.comments,
      communities: response.data.communities,
    };
  } catch (e) {
    return rejectWithValue({
      notification: {
        type: "error",
        message: "Something went wrong!",
      },
    });
  }
});

const userLogout = createAsyncThunk<
  { notification: IReturnNotification },
  void,
  { rejectValue: { notification: IReturnNotification } }
>("user/logout", async (_, { rejectWithValue }) => {
  try {
    await axios.get(`${import.meta.env.VITE_SERVER_URL}/api/auth/logout`, {
      withCredentials: true,
    });
    return {
      notification: { type: "success", message: "Logout successful" },
    };
  } catch (e) {
    return rejectWithValue({
      notification: {
        type: "error",
        message: "Logout failed",
      },
    });
  }
});

const userVerify = createAsyncThunk<
  { user: IUser },
  void,
  { rejectValue: boolean }
>("user/verify", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_SERVER_URL}/api/user/verify`,
      {
        withCredentials: true,
      },
    );
    return { user: response.data };
  } catch (e) {
    return rejectWithValue(false);
  }
});

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    // Removes a post from the posts list
    removePost: (state, action: PayloadAction<number>) => {
      const postIndex = state.posts.findIndex((x) => x.id === action.payload);
      state.posts.splice(postIndex, 1);
    },
    clearUserContent: (state) => {
      state.posts = [];
      state.comments = [];
      state.communities = [];
    },
    removeCommunity: (state, action: PayloadAction<number>) => {
      const communityIndex = state.communities.findIndex(
        (x) => x.id === action.payload,
      );
      state.communities.splice(communityIndex, 1);
    },
  },
  extraReducers: (builder) => {
    builder
      // USER LOGIN
      .addCase(userLogin.pending, (state) => {
        state.isLoading = true;
        state.isAuthenticated = false;
        state.user = null;
      })
      .addCase(
        userLogin.fulfilled,
        (
          state,
          action: PayloadAction<{
            user: IUser;
            notification: IReturnNotification;
          }>,
        ) => {
          state.user = action.payload.user;
          state.isAuthenticated = true;
          state.isLoading = false;
        },
      )
      .addCase(userLogin.rejected, (state) => {
        state.user = null;
        state.isLoading = false;
        state.isAuthenticated = false;
      })
      // USER LOGOUT
      .addCase(userLogout.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(userLogout.fulfilled, (state) => {
        state.isLoading = false;
        state.isAuthenticated = false;
        state.user = null;
      })
      .addCase(userLogout.rejected, (state) => {
        state.isLoading = false;
      })
      // USER VERIFY
      .addCase(userMe.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(userMe.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isAuthenticated = true;
        state.communities = action.payload.communities;
        state.posts = action.payload.posts;
        state.comments = action.payload.comments;
      })
      .addCase(userMe.rejected, (state) => {
        state.isLoading = false;
        state.isAuthenticated = false;
        state.user = null;
        state.communities = [];
        state.posts = [];
        state.comments = [];
      })
      // USER VERIFY
      .addCase(userVerify.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(userVerify.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isAuthenticated = true;
        state.user = action.payload.user;
      })
      .addCase(userVerify.rejected, (state) => {
        state.isLoading = false;
        state.isAuthenticated = false;
        state.user = null;
      });
  },
});

const { removePost, clearUserContent, removeCommunity } = userSlice.actions;
export {
  clearUserContent,
  userSlice,
  userLogin,
  userRegister,
  userMe,
  userLogout,
  userVerify,
  removePost,
  removeCommunity,
};
