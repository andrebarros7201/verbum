import type { IPostSimple } from "../../../interfaces/IPostSimple.ts";
import { ButtonLink } from "../../ui/ButtonLink.tsx";

type Props = {
  post: IPostSimple;
};

const PostListItem = ({ post }: Props) => {
  return (
    <div
      className={
        "max-w-sm flex flex-col gap-4 p-4 bg-white rounded border-2 border-amber-600"
      }
    >
      <h3 className={"capitalize"}>{post.title}</h3>
      <p>{post.votes}</p>
      <p>Comments: {post.commentsCount}</p>
      <ButtonLink href={`/post/${post.id}`} label={"Go To"} />
    </div>
  );
};

export { PostListItem };
