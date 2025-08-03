import type { RootDispatch, RootState } from "../redux/store.ts";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { CommunityList } from "../components/Community/CommunityList.tsx";
import { fetchAllCommunities } from "../redux/slices/communitySlice.ts";
import { ButtonCreateCommunity } from "../components/Community/ButtonCreateCommunity.tsx";

const Communities = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
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
    <div className={"w-full flex-1 flex flex-col justify-start items-start"}>
      {isAuthenticated && <ButtonCreateCommunity />}
      <CommunityList />
    </div>
  );
};

export { Communities };
