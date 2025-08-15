import type { IUser } from "./IUser.ts";
import type { IPostSimple } from "./IPostSimple.ts";
import type { ICommunityMember } from "./ICommunityMember.ts";

interface ICommunityComplete {
  id: number;
  name: string;
  description: string;
  owner: IUser;
  posts: IPostSimple[];
  members: ICommunityMember[];
  membersCount: number;
  postsCount: number;
  isAdmin: boolean;
  isMember: boolean;
  isOwner: boolean;
}

export type { ICommunityComplete };
