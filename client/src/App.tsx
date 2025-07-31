import "./App.css";
import { Outlet } from "react-router-dom";
import { Header } from "./components/Header.tsx";
import { Notification } from "./components/ui/Notification.tsx";
import { useEffect } from "react";
import type { AppDispatch } from "./redux/store.ts";
import { useDispatch } from "react-redux";
import { userVerify } from "./redux/slices/userSlice.ts";

function App() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    dispatch(userVerify());
  }, [dispatch]);

  return (
    <>
      <Header />
      <Notification />
      <div className={"w-full flex-1"}>
        <Outlet />
      </div>
    </>
  );
}

export default App;
