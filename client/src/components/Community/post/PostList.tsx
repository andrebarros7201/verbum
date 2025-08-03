import type { IPostSimple } from "../../../interfaces/IPostSimple.ts";
import { PostListItem } from "./PostListItem.tsx";

type Props = {
  posts: IPostSimple[];
};

const PostList = ({ posts }: Props) => {
  return (
    <div
      className={"w-full gap-4 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3"}
    >
      {posts.map((post) => (
        <PostListItem post={post} key={post.id} />
      ))}
    </div>
  );
};

export { PostList };
