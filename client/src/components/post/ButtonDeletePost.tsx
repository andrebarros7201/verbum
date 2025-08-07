import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../../redux/store.ts";
import { Button } from "../ui/Button.tsx";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import { useLocation, useNavigate } from "react-router-dom";
import { deletePost } from "../../redux/slices/currentPostSlice.ts";
import { removePost } from "../../redux/slices/userSlice.ts";

type Props = {
  id: number;
  userId: number; // Author ID number
  communityId: number;
};

const ButtonDeletePost = ({ id, userId, communityId }: Props) => {
  const location = useLocation();
  const dispatch = useDispatch<RootDispatch>();
  const navigate = useNavigate();
  const { user, isAuthenticated } = useSelector(
    (state: RootState) => state.user,
  );

  async function handleDeletePost() {
    try {
      const response = await dispatch(deletePost({ id })).unwrap();
      if (location.pathname.includes("profile")) {
        dispatch(removePost(id));
      } else if (location.pathname.includes("community")) {
        navigate(`/community/${communityId}`);
      }
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    isAuthenticated &&
    user!.id === userId && (
      <Button
        size={"small"}
        label={"Delete Post"}
        variant={"secondary"}
        onClick={handleDeletePost}
      />
    )
  );
};

export { ButtonDeletePost };
