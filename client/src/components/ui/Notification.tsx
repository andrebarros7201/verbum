import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../../redux/store.ts";
import clsx from "clsx";
import { clearNotification } from "../../redux/slices/notificationSlice.ts";

const Notification = () => {
  const { isVisible, type, message } = useSelector(
    (state: RootState) => state.notification,
  );
  const dispatch = useDispatch<RootDispatch>();

  function handleClose() {
    dispatch(clearNotification());
  }

  return (
    isVisible && (
      <div
        className={clsx(
          "max-w-lg w-full p-4 absolute top-4 border-2 flex flex-col items-start gap-4 text-white rounded z-100",
          {
            " border-red-500 bg-red-700": type === "error",
            " border-green-500 bg-green-700": type === "success",
          },
        )}
      >
        <div className={"w-full flex justify-between items-center"}>
          <h3 className={"capitalize"}>{type}</h3>
          <button
            className={"p-2 font-bold cursor-pointer"}
            onClick={handleClose}
          >
            X
          </button>
        </div>
        <p className={"w-full capitalize text-left"}>{message}</p>
      </div>
    )
  );
};

export { Notification };
