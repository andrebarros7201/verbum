import { useParams } from "react-router-dom";
import type { RootDispatch, RootState } from "../redux/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import { fetchCurrentCommunity } from "../redux/slices/currentCommunitySlice.ts";
import { CommunityMobileMenu } from "../components/Community/CommunityMobileMenu.tsx";
import { List } from "../components/List.tsx";
import { CommunityDesktopMenu } from "../components/Community/CommunityDesktopMenu.tsx";

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
    community && (
      <div
        className={
          "w-full flex flex-1 flex-col justify-start items-start gap-4"
        }
      >
        <div className={"flex w-full gap-4 justify-between items-center"}>
          <h3 className={"text-4xl font-bold text-sky-600"}>
            {community.name}
          </h3>
          <CommunityMobileMenu />
          <CommunityDesktopMenu />
        </div>
        <List list={community.posts} type={"post"} />
      </div>
    )
  );
};

export { Community };
