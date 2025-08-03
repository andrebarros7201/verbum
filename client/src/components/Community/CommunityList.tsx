import type { RootState } from "../../redux/store.ts";
import { useSelector } from "react-redux";
import { CommunityListItem } from "./CommunityListItem.tsx";

const CommunityList = () => {
  const { communities } = useSelector((state: RootState) => state.community);

  return (
    <main
      className={"w-full flex justify-start items-start gap-4 py-4 flex-wrap "}
    >
      {communities &&
        communities.map((community) => (
          <CommunityListItem community={community} key={community.id} />
        ))}
    </main>
  );
};

export { CommunityList };
