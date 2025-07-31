import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../redux/store.ts";
import { Button } from "../ui/Button.tsx";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { userLogout } from "../../redux/slices/userSlice.ts";

const ButtonLogout = () => {
  const dispatch = useDispatch<AppDispatch>();

  async function handleLogout() {
    try {
      const response = await dispatch(userLogout()).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e: any) {
      const { notification } = e;
      dispatch(setNotification(notification));
    }
  }

  return (
    <Button label={"Logout"} variant={"secondary"} onClick={handleLogout} />
  );
};

export { ButtonLogout };
