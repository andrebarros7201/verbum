import App from "./App";
import { Register } from "./pages/Register.tsx";
import { Login } from "./pages/Login.tsx";

const routes = [
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/register",
        element: <Register />,
      },
      {
        path: "/login",
        element: <Login />,
      },
    ],
  },
];

export { routes };
