import type { ICommunitySimple } from "./ICommunitySimple.ts";

interface ICommunitySlice {
  communities: ICommunitySimple[];
  isLoading: boolean;
}

export type { ICommunitySlice };
