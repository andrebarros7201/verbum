import { MobileMenu } from "../ui/MobileMenu";
import { ButtonCreatePost } from "./ButtonCreatePost.tsx";
import { ButtonCommunityMembership } from "./ButtonCommunityMembership.tsx";
import { ButtonUpdateCommunity } from "./ButtonUpdateCommunity.tsx";
import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { CommunityMembersList } from "./CommunityMembersList.tsx";
import { ListFilter } from "../ListFilter.tsx";

const CommunityMobileMenu = () => {
  const { user } = useSelector((state: RootState) => state.user);
  const { community } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  return (
    <MobileMenu>
      {user?.id == community?.owner.id && (
        <ButtonUpdateCommunity
          id={community!.id}
          name={community!.name}
          description={community!.description}
        />
      )}
      {(community?.isAdmin || community?.isOwner) && <CommunityMembersList />}
      <ListFilter type={"post"} />
      <ButtonCreatePost />
      <ButtonCommunityMembership />
    </MobileMenu>
  );
};

export { CommunityMobileMenu };
