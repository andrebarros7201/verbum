import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { ButtonVote } from "../ui/ButtonVote.tsx";
import { ButtonAddComment } from "./ButtonAddComment.tsx";
import { ButtonDeletePost } from "./ButtonDeletePost.tsx";
import { CommentsList } from "./CommentsList.tsx";
import { ButtonUpdatePost } from "./ButtonUpdatePost.tsx";

const PostItem = () => {
  const { isLoading, post } = useSelector(
    (state: RootState) => state.currentPost
  );
  const { isAuthenticated, user } = useSelector(
    (state: RootState) => state.user
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
        <div className={"w-full flex gap-4 items-center flex-wrap"}>
          {user?.id === post.user.id && (
            <>
              <ButtonDeletePost id={post.id} communityId={post.community.id} />
              <ButtonUpdatePost
                postId={post.id}
                text={post.text}
                title={post.title}
              />
            </>
          )}
          <div className={"flex gap-4 items-center"}>
            <ButtonVote type="post" value={-1} id={post.id} />
            <p className="text-lg">{post.votes}</p>
            <ButtonVote type="post" value={1} id={post.id} />
          </div>
          <h3 className={"font-bold text-xl"}>{post.user.username}</h3>
        </div>
        <h2 className={"font-bold text-3xl"}>{post.title}</h2>
        <p className={"font-semibold text-left"}>{post.text}</p>
        <div className={"flex w-full gap-4 justify-start items-center"}>
          <h4 className={"font-semibold"}>Comments {post.commentsCount}</h4>
          {isAuthenticated && <ButtonAddComment />}
        </div>
        <CommentsList list={post.comments} />
      </main>
    )
  );
};
export { PostItem };
