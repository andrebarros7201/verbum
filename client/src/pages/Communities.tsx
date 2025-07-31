import type { RootDispatch } from "../redux/store.ts";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { CommunityList } from "../components/Community/CommunityList.tsx";
import { fetchAllCommunities } from "../redux/slices/communitySlice.ts";

const Communities = () => {
  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    try {
      dispatch(fetchAllCommunities()).unwrap();
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      dispatch(setNotification(err.notification));
    }
  }, []);

  return (
    <div className={"flex flex-col justify-start items-start"}>
      <CommunityList />
    </div>
  );
};

export { Communities };
