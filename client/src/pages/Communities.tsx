import type { RootDispatch, RootState } from "../redux/store.ts";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import {
  fetchAllCommunities,
  setCommunityFilterList,
} from "../redux/slices/communitySlice.ts";
import { ButtonCreateCommunity } from "../components/Community/ButtonCreateCommunity.tsx";
import { List } from "../components/List.tsx";
import { ListFilter } from "../components/ListFilter.tsx";

const Communities = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
  const { communities } = useSelector((state: RootState) => state.community);
  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    try {
      dispatch(fetchAllCommunities()).unwrap();
      dispatch(setCommunityFilterList());
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      dispatch(setNotification(err.notification));
    }
  }, []);

  return (
    <div
      className={"w-full flex-1 flex gap-4 flex-col justify-start items-start"}
    >
      <div className={"w-full flex gap-4 items-center justify-start"}>
        {isAuthenticated && <ButtonCreateCommunity />}
        <ListFilter type={"community"} />
      </div>
      <List list={communities} type={"community"} />
    </div>
  );
};

export { Communities };
