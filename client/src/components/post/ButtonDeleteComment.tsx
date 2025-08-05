import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { deleteComment } from "../../redux/slices/currentPostSlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

type Props = {
  commentId: number;
};
const ButtonDeleteComment = ({ commentId }: Props) => {
  const dispatch = useDispatch<RootDispatch>();

  async function handleDeleteComment() {
    try {
      const response = await dispatch(
        deleteComment({ id: commentId }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      const { notification } = error;
      dispatch(setNotification(notification));
    }
  }

  return (
    <button
      className={"bg-red-600 text-white px-2 py-1 cursor-pointer rounded"}
      onClick={handleDeleteComment}
    >
      Delete
    </button>
  );
};

export { ButtonDeleteComment };
