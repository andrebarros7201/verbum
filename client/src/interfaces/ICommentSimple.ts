import type { IUser } from "./IUser.ts";

interface ICommentSimple {
  id: number;
  text: string;
  author: IUser;
  votes: number;
  createdAt: string;
  updatedAt?: string;
}

export type { ICommentSimple };
