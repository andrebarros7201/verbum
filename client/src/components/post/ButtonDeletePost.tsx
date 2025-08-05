import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../../redux/store.ts";
import { Button } from "../ui/Button.tsx";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import { useNavigate } from "react-router-dom";
import { deletePost } from "../../redux/slices/currentPostSlice.ts";

const ButtonDeletePost = () => {
  const dispatch = useDispatch<RootDispatch>();
  const navagate = useNavigate();
  const { post } = useSelector((state: RootState) => state.currentPost);
  const { user, isAuthenticated } = useSelector(
    (state: RootState) => state.user,
  );

  async function handleDeletePost() {
    try {
      const response = await dispatch(deletePost({ id: post!.id })).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      navagate("/communities");
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    isAuthenticated &&
    user!.id === post!.user.id && (
      <Button
        label={"Delete Post"}
        variant={"danger"}
        onClick={handleDeletePost}
      />
    )
  );
};

export { ButtonDeletePost };
