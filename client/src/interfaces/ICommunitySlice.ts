import type { ICommunitySimple } from "./ICommunitySimple.ts";

interface ICommunitySlice {
  communities: ICommunitySimple[];
  isLoading: boolean;
  filteredCommunities: ICommunitySimple[];
}

export type { ICommunitySlice };
