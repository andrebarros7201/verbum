interface ICommunitySimple {
  id: number;
  name: string;
  description: string;
  userId: number;
  membersCount: number;
  isMember: boolean;
  isOwner: boolean;
}

export type { ICommunitySimple };
