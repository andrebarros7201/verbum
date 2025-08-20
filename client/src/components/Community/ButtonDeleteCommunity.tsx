import { useDispatch } from "react-redux";
import { Button } from "../ui/Button";
import type { RootDispatch } from "../../redux/store";
import { deleteCommunity } from "../../redux/slices/currentCommunitySlice";
import { setNotification } from "../../redux/slices/notificationSlice";
import type { IReturnNotification } from "../../interfaces/IReturnNotification";
import { useNavigate } from "react-router-dom";

const ButtonDeleteCommunity = () => {
  const dispatch = useDispatch<RootDispatch>();
  const navigate = useNavigate();

  async function handleClick() {
    try {
      const response = await dispatch(deleteCommunity()).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      navigate("/communities");
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      dispatch(setNotification(error.notification));
    }
  }
  return (
    <Button
      variant={"secondary"}
      label={"Delete Community"}
      size={"small"}
      onClick={handleClick}
    />
  );
};
export { ButtonDeleteCommunity };
