import type { IUser } from "./IUser.ts";
import type { IPostSimple } from "./IPostSimple.ts";

interface ICommunityComplete {
  id: number;
  name: string;
  description: string;
  owner: IUser;
  posts: IPostSimple[];
  members: { username: string; id: number; isAdmin: boolean }[];
  membersCount: number;
  postsCount: number;
  isMember: boolean;
  isOwner: boolean;
}

export type { ICommunityComplete };
