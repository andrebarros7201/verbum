import type { IUser } from "./IUser.ts";

interface IPostSimple {
  id: number;
  title: string;
  votes: number;
  commentsCount: number;
  createdAt: string;
  updatedAt?: string;
  user: IUser;
}

export type { IPostSimple };
