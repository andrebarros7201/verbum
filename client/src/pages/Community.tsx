import { useParams } from "react-router-dom";
import type { RootDispatch, RootState } from "../redux/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import { fetchCurrentCommunity } from "../redux/slices/currentCommunitySlice.ts";
import { PostList } from "../components/Community/post/PostList.tsx";

const Community = () => {
  const { community } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  const dispatch = useDispatch<RootDispatch>();
  const { id } = useParams();

  useEffect(() => {
    const fetch = async () => {
      try {
        await dispatch(fetchCurrentCommunity({ id: parseInt(id!) })).unwrap();
      } catch (e) {
        const err = e as { notification: IReturnNotification };
        dispatch(
          setNotification({
            type: err.notification.type,
            message: err.notification.message,
          }),
        );
      }
    };

    fetch();
  }, []);

  return (
    <div className={"w-full flex flex-1"}>
      {community && community.posts && <PostList posts={community.posts} />}
    </div>
  );
};

export { Community };
