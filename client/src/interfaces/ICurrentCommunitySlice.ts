import type { ICommunityComplete } from "./ICommunityComplete.ts";
import type { ICommunityMember } from "./ICommunityMember.ts";
import type { IPostSimple } from "./IPostSimple.ts";

interface ICurrentCommunitySlice {
  isLoading: boolean;
  community: ICommunityComplete | null;
  filteredPosts: IPostSimple[];
  filteredMembers: ICommunityMember[];
}

export type { ICurrentCommunitySlice };
