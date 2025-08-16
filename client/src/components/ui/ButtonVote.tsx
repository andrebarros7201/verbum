import { clsx } from "clsx";
import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../../redux/store";
import { voteComment, votePost } from "../../redux/slices/currentPostSlice";
import { setNotification } from "../../redux/slices/notificationSlice";
import type { IReturnNotification } from "../../interfaces/IReturnNotification";
import { useNavigate } from "react-router-dom";

type Props = {
  type: "post" | "comment";
  value: -1 | 1;
  id: number;
};

const ButtonVote = ({ type, value, id }: Props) => {
  const dispatch = useDispatch<RootDispatch>();
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
  const navigate = useNavigate();

  async function onClick() {
    if (!isAuthenticated) {
      navigate("/login");
      dispatch(
        setNotification({
          type: "error",
          message: "User must be logged in to vote!",
        })
      );
      return;
    }

    try {
      if (type === "post") {
        await dispatch(votePost({ id, value })).unwrap();
      } else {
        await dispatch(voteComment({ id, value }));
      }
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      const { notification } = error;
      dispatch(setNotification(notification));
    }
  }

  return (
    <button
      className={clsx(
        "px-2 py-1 rounded text-white hover:shadow-xl cursor-pointer",
        {
          "bg-amber-500": value === -1,
          "bg-sky-600": value === 1,
        }
      )}
      onClick={onClick}
    >
      {value}
    </button>
  );
};
export { ButtonVote };
