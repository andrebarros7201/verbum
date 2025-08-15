import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { ButtonUpdateCommunity } from "./ButtonUpdateCommunity.tsx";
import { ButtonCreatePost } from "./ButtonCreatePost.tsx";
import { ButtonCommunityMembership } from "./ButtonCommunityMembership.tsx";
import { CommunityMembersList } from "./CommunityMembersList.tsx";
import { ListFilter } from "../ListFilter.tsx";

const CommunityDesktopMenu = () => {
  const { user } = useSelector((state: RootState) => state.user);
  const { community } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  return (
    <div
      className={
        "hidden w-full flex-1 sm:flex items-center justify-start gap-4"
      }
    >
      {user?.id == community?.owner.id && (
        <ButtonUpdateCommunity
          id={community!.id}
          name={community!.name}
          description={community!.description}
        />
      )}
      {(community?.isAdmin || community?.isOwner) && (
        <ListFilter type="member" label={"Members"} />
      )}
      <ListFilter type="post" />
      <ButtonCreatePost />
      <ButtonCommunityMembership />
    </div>
  );
};

export { CommunityDesktopMenu };
