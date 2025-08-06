import { useEffect } from "react";
import type { RootDispatch } from "../redux/store.ts";
import { useDispatch } from "react-redux";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { userMe } from "../redux/slices/userSlice.ts";

const Profile = () => {
  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    const fetchMe = async () => {
      try {
        await dispatch(userMe());
      } catch (e) {
        const err = e as { notification: IReturnNotification };
        dispatch(setNotification(err.notification));
      }
    };
    fetchMe();
  }, []);
  return <div className={"w-full border-2"}></div>;
};
export { Profile };
