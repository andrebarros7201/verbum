import type { ICommentSimple } from "../../interfaces/ICommentSimple.ts";
import { CommentItem } from "./CommentItem.tsx";

type Props = {
  list: ICommentSimple[];
};
const CommentsList = ({ list }: Props) => {
  return (
    <div className={"w-full flex flex-col gap-4"}>
      {list.map((comment) => (
        <CommentItem comment={comment} key={comment.id} />
      ))}
    </div>
  );
};

export { CommentsList };
