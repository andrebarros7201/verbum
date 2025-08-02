import type { ICommentSimple } from "../../interfaces/ICommentSimple";
import { ButtonVote } from "../ui/ButtonVote";

type Props = {
  comment: ICommentSimple;
};

const CommentItem = ({ comment }: Props) => {
  return (
    <main
      className={
        "w-full flex flex-col gap-4 items-start justify-start bg-gray-300 p-4 roudned border-2 border-amber-600"
      }
    >
      <div className="w-full flex gap-4 items-center justify-start">
        <p>{comment.author.username}</p>
        <p>{comment.createdAt.split("T")[0]}</p>
        <ButtonVote value={-1} type="comment" />
        <p>{comment.votes}</p>
        <ButtonVote value={1} type="comment" />
      </div>
      <p className={"w-full text-md text-left font-bold"}>{comment.text}</p>
    </main>
  );
};

export { CommentItem };
