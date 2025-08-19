import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import type { ICurrentCommunitySlice } from "../../interfaces/ICurrentCommunitySlice.ts";
import axios, { type AxiosError } from "axios";
import type { ICommunityComplete } from "../../interfaces/ICommunityComplete.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { IPostSimple } from "../../interfaces/IPostSimple.ts";
import type { RootState } from "../store.ts";

const initialState: ICurrentCommunitySlice = {
  isLoading: false,
  community: null,
  filteredMembers: [],
  filteredPosts: [],
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
      }
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
        }
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
  }
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
        }
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
  }
);

const updateCommunity = createAsyncThunk<
  { name: string; description: string; notification: IReturnNotification },
  { id: number; name: string; description: string },
  { rejectValue: { notification: IReturnNotification } }
>(
  "currentCommunity/update",
  async ({ id, name, description }, { rejectWithValue }) => {
    try {
      await axios.patch(
        `${import.meta.env.VITE_SERVER_URL}/api/community/${id}`,
        {
          name,
          description,
        },
        {
          withCredentials: true,
        }
      );

      return {
        name: name,
        description: description,
        notification: {
          type: "success",
          message: "Community updated",
        },
      };
    } catch (e) {
      if (axios.isAxiosError(e) && e.response?.status === 400) {
        const firstKey = Object.keys(e.response?.data)[0];
        const message = e.response?.data[firstKey] || "Something went wrong";
        return rejectWithValue({
          notification: { type: "error", message },
        });
      }
      const error = e as AxiosError<string>;
      const message = error.response?.data || "Something went wrong";
      return rejectWithValue({ notification: { type: "error", message } });
    }
  }
);

// Toggle User Role
const toggleUserRole = createAsyncThunk<
  { targetUserId: number; notification: IReturnNotification },
  { targetUserId: number },
  { rejectValue: { notification: IReturnNotification }; state: RootState }
>(
  "currentCommunity/toggleUserRole",
  async ({ targetUserId }, { rejectWithValue, getState }) => {
    try {
      const state = getState();
      const { community } = state.currentCommunity;
      await axios.patch(
        `${import.meta.env.VITE_SERVER_URL}/api/community/${
          community?.id
        }/role/${targetUserId}`,
        {},
        {
          withCredentials: true,
        }
      );
      return {
        targetUserId,
        notification: { type: "success", message: "User role updated" },
      };
    } catch (e) {
      const error = e as AxiosError<string>;
      return rejectWithValue({
        notification: {
          type: "error",
          message: error.response?.data || "Failed to update user role",
        },
      });
    }
  }
);
const currentCommunitySlice = createSlice({
  name: "currentCommunity",
  initialState,
  reducers: {
    removePost: (state, action) => {
      const postIndex = state.community!.posts.findIndex(
        (x) => x.id === action.payload.id
      );
      state.community!.posts.splice(postIndex, 1);
    },
    setFilterPosts: (state) => {
      state.filteredPosts = state.community?.posts ?? [];
    },
    setFilterMembers: (state) => {
      state.filteredMembers = state.community?.members ?? [];
    },
    filterPosts: (state, action: PayloadAction<{ searchText: string }>) => {
      const { searchText } = action.payload;
      state.filteredPosts = state.community!.posts.filter((x) =>
        x.title.toLowerCase().includes(searchText.toLowerCase())
      );
    },
    filterMembers: (state, action: PayloadAction<{ searchText: string }>) => {
      const { searchText } = action.payload;
      state.filteredMembers = state.community!.members.filter((x) =>
        x.username.toLowerCase().includes(searchText.toLowerCase())
      );
    },
  },
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
      // Update Current Community
      .addCase(updateCommunity.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(updateCommunity.fulfilled, (state, action) => {
        const { name, description } = action.payload;
        state.isLoading = false;
        state.community!.name = name;
        state.community!.description = description;
      })
      .addCase(updateCommunity.rejected, (state) => {
        state.isLoading = false;
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
      })
      // Toggle User Role
      .addCase(toggleUserRole.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(toggleUserRole.fulfilled, (state, action) => {
        state.isLoading = false;
        const { targetUserId } = action.payload;
        const userIndex = state.community!.members.findIndex(
          (x) => x.id === targetUserId
        );
        state.community!.members[userIndex].isAdmin =
          !state.community!.members[userIndex].isAdmin;
      })
      .addCase(toggleUserRole.rejected, (state) => {
        state.isLoading = false;
      });
  },
});
const {
  removePost,
  filterMembers,
  filterPosts,
  setFilterMembers,
  setFilterPosts,
} = currentCommunitySlice.actions;
export {
  currentCommunitySlice,
  removePost,
  setFilterMembers,
  setFilterPosts,
  filterMembers,
  filterPosts,
  updateCommunity,
  fetchCurrentCommunity,
  toggleMembership,
  toggleUserRole,
  createPost,
};
