import "./App.css";
import { Outlet } from "react-router-dom";
import { Header } from "./components/Header.tsx";
import { ReduxProvider } from "./components/ReduxProvider.tsx";

function App() {
  return (
    <>
      <ReduxProvider>
        <Header />
        <Outlet />
      </ReduxProvider>
    </>
  );
}

export default App;
