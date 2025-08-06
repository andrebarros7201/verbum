import { Button } from "../ui/Button.tsx";
import { joinCommunity } from "../../redux/slices/communitySlice.ts";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { setNotification } from "../../redux/slices/notificationSlice.ts";

type Props = {
  id: number;
};

const ButtonCommunityJoin = ({ id }: Props) => {
  const dispatch = useDispatch<RootDispatch>();

  async function handleJoin() {
    try {
      const response = await dispatch(joinCommunity({ id })).unwrap();
      dispatch(setNotification(response.notification));
    } catch (e: any) {
      const { notification } = e;
      dispatch(setNotification(notification));
    }
  }

  return (
    <Button
      label={"Join"}
      variant={"primary"}
      size={"small"}
      onClick={handleJoin}
    />
  );
};

export { ButtonCommunityJoin };
