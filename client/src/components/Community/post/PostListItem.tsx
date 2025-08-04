import type { IPostSimple } from "../../../interfaces/IPostSimple.ts";
import { ButtonLink } from "../../ui/ButtonLink.tsx";

type Props = {
  post: IPostSimple;
};

const PostListItem = ({ post }: Props) => {
  return (
    <div
      className={
        "w-full flex flex-col justify-start items-start gap-4 p-4 bg-white rounded border-2 border-amber-600 shadow-2xl hover:scale-105 transition-all duration-300"
      }
    >
      <h3 className={"capitalize font-bold text-xl"}>{post.title}</h3>
      <p>{post.votes}</p>
      <p>
        {post.commentsCount === 0
          ? "No Comments"
          : post.commentsCount === 1
            ? "1 Comment"
            : `${post.commentsCount} Comments`}
      </p>
      <ButtonLink href={`/post/${post.id}`} label={"Go To"} />
    </div>
  );
};

export { PostListItem };
