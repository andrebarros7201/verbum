import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { routes } from "./routes.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ReduxProvider } from "./components/ReduxProvider.tsx";

const router = createBrowserRouter(routes);

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ReduxProvider>
      <RouterProvider router={router} />
    </ReduxProvider>
  </StrictMode>,
);
