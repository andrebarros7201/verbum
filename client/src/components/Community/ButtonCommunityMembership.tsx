import type { RootDispatch, RootState } from "../../redux/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "../ui/Button.tsx";
import { toggleMembership } from "../../redux/slices/currentCommunitySlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

const ButtonCommunityMembership = () => {
  const { community } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  const dispatch = useDispatch<RootDispatch>();

  async function handleToggleCommunityMembership() {
    try {
      const response = await dispatch(
        toggleMembership({ id: community!.id, isMember: community!.isMember }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    <Button
      label={community!.isMember ? "Leave Community" : "Join Community"}
      variant={"primary"}
      onClick={handleToggleCommunityMembership}
    />
  );
};
export { ButtonCommunityMembership };
