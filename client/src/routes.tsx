import App from "./App";
import { Register } from "./pages/Register.tsx";

const routes = [
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/register",
        element: <Register />,
      },
    ],
  },
];

export { routes };
