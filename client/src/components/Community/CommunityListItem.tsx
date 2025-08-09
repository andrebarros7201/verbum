import type { ICommunitySimple } from "../../interfaces/ICommunitySimple.ts";
import { ButtonLink } from "../ui/ButtonLink.tsx";
import { ButtonCommunityLeave } from "./ButtonCommunityLeave.tsx";
import { ButtonCommunityJoin } from "./ButtonCommunityJoin.tsx";
import type { RootState } from "../../redux/store.ts";
import { useSelector } from "react-redux";

type Props = {
  community: ICommunitySimple;
};

const CommunityListItem = ({ community }: Props) => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
  return (
    <div
      className={
        "w-full w-full border-2 border-sky-600 flex flex-col gap-4 items-center justify-start bg-white p-4 rounded shadow-2xl"
      }
    >
      <h3 className={"font-bold text-xl"}>{community.name}</h3>
      <p>{community.description}</p>
      <p>
        {community.membersCount === 0
          ? "No Members"
          : community.membersCount === 1
            ? "1 Member"
            : `${community.membersCount} Members`}
      </p>
      <div className={"flex gap-4 justify-center items-center w-full"}>
        <ButtonLink
          href={`/community/${community.id}`}
          label={"Go To"}
          size={"small"}
        />
        {isAuthenticated &&
          (community.isMember ? (
            <ButtonCommunityLeave id={community.id} />
          ) : (
            <ButtonCommunityJoin id={community.id} />
          ))}
      </div>
    </div>
  );
};

export { CommunityListItem };
