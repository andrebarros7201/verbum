import type { IUser } from "./IUser.ts";

interface IUserSlice {
  isAuthenticated: boolean;
  user: IUser | null;
  isLoading: boolean;
}

export type { IUserSlice };
