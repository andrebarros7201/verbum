import type { ICommunitySimple } from "./ICommunitySimple.ts";

interface ICurrentCommunitySlice {
  isLoading: boolean;
  community: ICommunitySimple | null;
}

export type { ICurrentCommunitySlice };
