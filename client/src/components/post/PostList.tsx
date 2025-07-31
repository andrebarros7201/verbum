import type { IPostSimple } from "../../interfaces/IPostSimple.ts";
import { PostListItem } from "./PostListItem.tsx";

type Props = {
  posts: IPostSimple[];
};

const PostList = ({ posts }: Props) => {
  return (
    <div className={"w-full gap-4 flex-wrap grid-cols-3  flex-1"}>
      {posts.map((post) => (
        <PostListItem post={post} />
      ))}
    </div>
  );
};

export { PostList };
