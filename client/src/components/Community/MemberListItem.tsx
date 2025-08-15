import { Button } from "../ui/Button.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { toggleUserRole } from "../../redux/slices/currentCommunitySlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

type Props = {
  id: number;
  username: string;
  isAdmin: boolean;
};

const MemberListItem = ({ id, username, isAdmin }: Props) => {
  const dispatch = useDispatch<RootDispatch>();

  async function handlePromote() {
    try {
      const response = await dispatch(
        toggleUserRole({ targetUserId: id }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      const { notification } = error;
      dispatch(setNotification(notification));
    }
  }
  return (
    <div className={"flex gap-2 items-center justify-between w-full"} key={id}>
      <p className={"font-bold"}>{username}</p>
      <Button
        label={isAdmin ? "Demote" : "Promote"}
        variant={"secondary"}
        size={"small"}
        onClick={handlePromote}
      />
    </div>
  );
};

export { MemberListItem };
