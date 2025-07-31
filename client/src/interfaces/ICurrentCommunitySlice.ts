import type { ICommunityComplete } from "./ICommunityComplete.ts";

interface ICurrentCommunitySlice {
  isLoading: boolean;
  community: ICommunityComplete | null;
}

export type { ICurrentCommunitySlice };
