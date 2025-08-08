import { Button } from "../ui/Button.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { leaveCommunity } from "../../redux/slices/communitySlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { useLocation } from "react-router-dom";
import { removeCommunity } from "../../redux/slices/userSlice.ts";

type Props = {
  id: number;
};
const ButtonCommunityLeave = ({ id }: Props) => {
  const dispatch = useDispatch<RootDispatch>();
  const location = useLocation();

  async function handleLeave() {
    try {
      const response = await dispatch(leaveCommunity({ id })).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      if (location.pathname.includes("profile")) {
        dispatch(removeCommunity(id));
      }
    } catch (e: any) {
      const { notification } = e;
      dispatch(setNotification(notification));
    }
  }

  return (
    <Button
      label={"Leave"}
      variant={"primary"}
      size={"small"}
      onClick={handleLeave}
    />
  );
};

export { ButtonCommunityLeave };
