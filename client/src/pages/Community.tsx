import { useParams } from "react-router-dom";
import type { RootDispatch } from "../redux/store.ts";
import { useDispatch } from "react-redux";
import { useEffect } from "react";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import { fetchCurrentCommunity } from "../redux/slices/currentCommunitySlice.ts";

const Community = () => {
  const dispatch = useDispatch<RootDispatch>();
  const { id } = useParams();

  useEffect(() => {
    try {
      dispatch(fetchCurrentCommunity({ id: parseInt(id!) }));
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      dispatch(setNotification(err.notification));
    }
  }, []);

  return <div></div>;
};

export { Community };
