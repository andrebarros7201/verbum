import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { CommentItem } from "./CommentItem.tsx";

const PostItem = () => {
  const { isLoading, post } = useSelector(
    (state: RootState) => state.currentPost,
  );

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
        <h2 className={"font-bold text-3xl"}>{post.title}</h2>
        <p className="text-lg">{post.votes}</p>
        <h4>Comments {post.commentsCount}</h4>
        <div className={"w-full flex flex-col gap-4 py-4"}>
          {post.comments.map((comment) => (
            <CommentItem comment={comment} />
          ))}
        </div>
      </main>
    )
  );
};
export { PostItem };
