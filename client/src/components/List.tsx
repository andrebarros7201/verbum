import type { IPostSimple } from "../interfaces/IPostSimple.ts";
import type { ICommunitySimple } from "../interfaces/ICommunitySimple.ts";
import { CommunityListItem } from "./Community/CommunityListItem.tsx";
import { PostListItem } from "./Community/post/PostListItem.tsx";

type Props =
  | { list: IPostSimple[]; type: "post" }
  | { list: ICommunitySimple[]; type: "community" };

// A single list that can handle both post and community items
const List = ({ list, type }: Props) => {
  return (
    <div
      className={
        "w-full grid grid-flow-row grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4"
      }
    >
      {type === "post"
        ? list.map((item) => <PostListItem post={item} key={item.id} />)
        : list.map((item) => (
            <CommunityListItem community={item} key={item.id} />
          ))}
    </div>
  );
};

export { List };
