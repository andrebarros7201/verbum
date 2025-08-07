import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../../redux/store.ts";
import { Button } from "../ui/Button.tsx";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import { useLocation, useNavigate } from "react-router-dom";
import { deletePost } from "../../redux/slices/currentPostSlice.ts";
import { removePost as userRemovePost } from "../../redux/slices/userSlice.ts";
import { removePost as currentCommunityRemovePost } from "../../redux/slices/currentCommunitySlice.ts";
import { useEffect } from "react";

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

  useEffect(() => {
    console.log(location.pathname);
    console.log(communityId);
  }, []);

  async function handleDeletePost() {
    try {
      const response = await dispatch(deletePost({ id })).unwrap();
      if (location.pathname.includes("profile")) {
        // If inside the profile page
        dispatch(userRemovePost(id));
      } else if (location.pathname.includes("community")) {
        // If inside the current community
        dispatch(currentCommunityRemovePost(id));
      } else {
        // If inside the post page, go back to its community page
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
