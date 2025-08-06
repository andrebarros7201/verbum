import type { ICommentSimple } from "../../interfaces/ICommentSimple";
import { ButtonVote } from "../ui/ButtonVote";
import type { RootState } from "../../redux/store.ts";
import { useSelector } from "react-redux";
import { ButtonDeleteComment } from "./ButtonDeleteComment.tsx";

type Props = {
  comment: ICommentSimple;
};

const CommentItem = ({ comment }: Props) => {
  const { user } = useSelector((state: RootState) => state.user);
  return (
    <main
      className={
        "w-full flex flex-col gap-4 items-start justify-start bg-white shadow-2xl p-4 rounded border-2 border-sky-600"
      }
    >
      <div className="w-full flex flex-wrap gap-4 items-center justify-start font-semibold">
        <div className={"flex gap-2 items-center"}>
          <p>{comment.author.username}</p>
          <p>@</p>
          <p>{comment.createdAt.split("T")[0]}</p>
        </div>
        <div className={"flex gap-2 items-center"}>
          <ButtonVote value={-1} type="comment" id={comment.id} />
          <p>{comment.votes}</p>
          <ButtonVote value={1} type="comment" id={comment.id} />
        </div>
        {comment.author.id == user?.id && (
          <ButtonDeleteComment commentId={comment.id} />
        )}
      </div>
      <p className={"w-full text-md text-left font-bold"}>{comment.text}</p>
    </main>
  );
};

export { CommentItem };
