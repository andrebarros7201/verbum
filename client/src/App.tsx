import "./App.css";
import { Outlet } from "react-router-dom";
import { Header } from "./components/Header.tsx";
import { ReduxProvider } from "./components/ReduxProvider.tsx";
import { Notification } from "./components/ui/Notification.tsx";

function App() {
  return (
    <>
      <ReduxProvider>
        <Header />
        <Notification />
        <div className={"w-full flex-1 border-2 border-red-500"}>
          <Outlet />
        </div>
      </ReduxProvider>
    </>
  );
}

export default App;
