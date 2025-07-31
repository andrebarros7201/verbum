import type { AppDispatch } from "../redux/store.ts";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchAllCommunities } from "../redux/slices/CommunitySlice.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";

const Community = () => {
  const dispatch = useDispatch<AppDispatch>();
  useEffect(() => {
    try {
      dispatch(fetchAllCommunities());
    } catch (e: any) {
      dispatch(setNotification(e.notification));
    }
  }, []);
  return <div>Community</div>;
};

export { Community };
