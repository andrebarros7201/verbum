import type { IPostComplete } from "./IPostComplete.ts";

interface ICurrentPostSlice {
  post: IPostComplete | null;
  isLoading: boolean;
}

export type { ICurrentPostSlice };
