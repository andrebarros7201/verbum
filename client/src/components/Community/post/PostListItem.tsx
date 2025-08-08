import type { IPostSimple } from "../../../interfaces/IPostSimple.ts";
import { ButtonLink } from "../../ui/ButtonLink.tsx";
import { DropdownMenu } from "../../ui/DropdownMenu.tsx";
import { ButtonDeletePost } from "../../post/ButtonDeletePost.tsx";
import { useSelector } from "react-redux";
import type { RootState } from "../../../redux/store.ts";

type Props = {
  post: IPostSimple;
};

const PostListItem = ({ post }: Props) => {
  const { user } = useSelector((state: RootState) => state.user);

  return (
    <div
      className={
        "w-full flex flex-col justify-start items-start gap-4 p-4 bg-white rounded border-2 border-sky-600 shadow-2xl"
      }
    >
      <div className={"flex gap-4 items-center justify-between w-full"}>
        <h3 className={"capitalize font-bold text-xl"}>{post.title}</h3>
        {user?.id === post.user.id && (
          <DropdownMenu>
            <ButtonDeletePost id={post.id} communityId={post.communityId} />
          </DropdownMenu>
        )}
      </div>
      <p>{post.votes} Votes</p>
      <p>
        {post.commentsCount === 0
          ? "No Comments"
          : post.commentsCount === 1
            ? "1 Comment"
            : `${post.commentsCount} Comments`}
      </p>
      <ButtonLink size={"small"} href={`/post/${post.id}`} label={"Go To"} />
    </div>
  );
};

export { PostListItem };
