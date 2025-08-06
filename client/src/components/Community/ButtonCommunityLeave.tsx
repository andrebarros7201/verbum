import { Button } from "../ui/Button.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { leaveCommunity } from "../../redux/slices/communitySlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";

type Props = {
  id: number;
};
const ButtonCommunityLeave = ({ id }: Props) => {
  const dispatch = useDispatch<RootDispatch>();

  async function handleLeave() {
    try {
      const response = await dispatch(leaveCommunity({ id })).unwrap();
      dispatch(setNotification(response.notification));
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
