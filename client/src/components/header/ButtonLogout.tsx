import { useDispatch } from "react-redux";
import type { RootDispatch } from "../../redux/store.ts";
import { Button } from "../ui/Button.tsx";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { userLogout } from "../../redux/slices/userSlice.ts";
import { useNavigate } from "react-router-dom";

const ButtonLogout = () => {
  const dispatch = useDispatch<RootDispatch>();
  const navigate = useNavigate();

  async function handleLogout() {
    try {
      const response = await dispatch(userLogout()).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      navigate("/login");
    } catch (e: any) {
      const { notification } = e;
      dispatch(setNotification(notification));
    }
  }

  return (
    <Button
      size={"small"}
      label={"Logout"}
      variant={"secondary"}
      onClick={handleLogout}
    />
  );
};

export { ButtonLogout };
