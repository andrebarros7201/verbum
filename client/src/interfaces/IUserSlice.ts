import type { IUser } from "./IUser.ts";
import type { ICommunitySimple } from "./ICommunitySimple.ts";
import type { IPostSimple } from "./IPostSimple.ts";
import type { ICommentSimple } from "./ICommentSimple.ts";

interface IUserSlice {
  isAuthenticated: boolean;
  user: IUser | null;
  isLoading: boolean;
  communities: ICommunitySimple[];
  posts: IPostSimple[];
  comments: ICommentSimple[];
}

export type { IUserSlice };
