interface IUserSlice {
  isAuthenticated: boolean;
  user: { id: number; username: string } | null;
  isLoading: boolean;
}

export type { IUserSlice };
