import { PostItem } from "../components/post/PostItem.tsx";
import { useEffect } from "react";
import type { RootDispatch } from "../redux/store.ts";
import { useDispatch } from "react-redux";
import { fetchCurrentPost } from "../redux/slices/currentPostSlice.ts";
import { useParams } from "react-router-dom";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";

const Post = () => {
  const { id } = useParams();
  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    async function fetch() {
      try {
        await dispatch(fetchCurrentPost({ id: parseInt(id!) })).unwrap();
      } catch (e) {
        const err = e as { notification: IReturnNotification };
        dispatch(setNotification(err.notification));
      }
    }

    fetch();
  }, [dispatch, id]);

  return <PostItem />;
};

export { Post };
