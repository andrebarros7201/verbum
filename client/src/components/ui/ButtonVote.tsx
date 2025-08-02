import { clsx } from "clsx";
import { useDispatch } from "react-redux";
import type { RootDispatch } from "../../redux/store";
import { votePost, voteComment } from "../../redux/slices/currentPostSlice";
import { setNotification } from "../../redux/slices/notificationSlice";

type Props = {
  type: "post" | "comment";
  value: -1 | 1;
  id: number;
};

const ButtonVote = ({ type, value, id }: Props) => {
  const dispatch = useDispatch<RootDispatch>();
  async function onClick() {
    try {
      if (type === "post") {
        await dispatch(votePost({ id, value })).unwrap();
      } else {
        await dispatch(voteComment({ id, value }));
      }
    } catch (error: any) {
      dispatch(setNotification(error.notification));
    }
  }

  return (
    <button
      className={clsx(
        "px-2 py-1 rounded text-white hover:shadow-xl cursor-pointer",
        {
          "bg-blue-400": value === -1,
          "bg-amber-600": value === 1,
        },
      )}
      onClick={onClick}
    >
      {value}
    </button>
  );
};
export { ButtonVote };
