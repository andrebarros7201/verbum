import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { CommentItem } from "./CommentItem.tsx";
import { ButtonVote } from "../ui/ButtonVote.tsx";
import { ButtonAddComment } from "./ButtonAddComment.tsx";

const PostItem = () => {
  const { isLoading, post } = useSelector(
    (state: RootState) => state.currentPost,
  );
  const { isAuthenticated } = useSelector((state: RootState) => state.user);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    post && (
      <main
        className={
          "w-full flex-1 flex flex-col items-start justify-start gap-4 "
        }
      >
        <div className={"flex gap-4 items-center"}>
          <ButtonVote type="post" value={-1} id={post.id} />
          <p className="text-lg">{post.votes}</p>
          <ButtonVote type="post" value={1} id={post.id} />
          <h2 className={"font-bold text-3xl"}>{post.title}</h2>
        </div>
        <p>{post.text}</p>
        <h4>Comments {post.commentsCount}</h4>
        {isAuthenticated && <ButtonAddComment />}
        <div className={"w-full flex flex-col gap-4"}>
          {post.comments.map((comment) => (
            <CommentItem comment={comment} />
          ))}
        </div>
      </main>
    )
  );
};
export { PostItem };
