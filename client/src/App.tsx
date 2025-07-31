import "./App.css";
import { Outlet } from "react-router-dom";
import { Notification } from "./components/ui/Notification.tsx";
import { useEffect } from "react";
import type { RootDispatch } from "./redux/store.ts";
import { useDispatch } from "react-redux";
import { userVerify } from "./redux/slices/userSlice.ts";
import { Header } from "./components/header/Header.tsx";

function App() {
  const dispatch = useDispatch<RootDispatch>();

  useEffect(() => {
    dispatch(userVerify());
  }, [dispatch]);

  return (
    <>
      <Header />
      <Notification />
      <div className={"w-full flex-1 flex"}>
        <Outlet />
      </div>
    </>
  );
}

export default App;
