import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { deleteComment } from "../../redux/slices/currentPostSlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import { Button } from "../ui/Button.tsx";

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
    <Button
      onClick={handleDeleteComment}
      label={"Delete Comment"}
      variant={"secondary"}
      size={"small"}
    />
  );
};

export { ButtonDeleteComment };
