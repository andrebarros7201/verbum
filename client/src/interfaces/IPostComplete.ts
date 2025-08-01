import type { IUser } from "./IUser.ts";
import type { ICommentSimple } from "./ICommentSimple.ts";
import type { ICommunitySimple } from "./ICommunitySimple.ts";

interface IPostComplete {
  id: number;
  title: string;
  text: string;
  user: IUser;
  votes: number;
  community: ICommunitySimple;
  commentsCount: number;
  comments: ICommentSimple[];
  createdAt: string;
  updatedAt?: string;
}

export type { IPostComplete };
