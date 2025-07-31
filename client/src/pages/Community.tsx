import type { RootDispatch } from "../redux/store.ts";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchAllCommunities } from "../redux/slices/CommunitySlice.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";

const Community = () => {
  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    try {
      dispatch(fetchAllCommunities()).unwrap();
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      dispatch(setNotification(err.notification));
    }
  }, []);

  return <div>Community</div>;
};

export { Community };
