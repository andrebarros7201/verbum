import type { IUserSlice } from "../../interfaces/IUserSlice.ts";
import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import type { IUser } from "../../interfaces/IUser.ts";
import axios, { AxiosError } from "axios";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

const initialState: IUserSlice = {
  isAuthenticated: false,
  user: null,
  isLoading: false,
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
const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {},
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
        (state, action: PayloadAction<{ user: IUser }>) => {
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
      });
  },
});

export { userSlice, userLogin, userRegister, userLogout };
