import App from "./App";
import { Register } from "./pages/Register.tsx";
import { Login } from "./pages/Login.tsx";
import { Communities } from "./pages/Communities.tsx";
import { Post } from "./pages/Post.tsx";
import { Community } from "./pages/Community.tsx";
import { Profile } from "./pages/Profile.tsx";

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
      {
        path: "/communities",
        element: <Communities />,
      },
      {
        path: "/community/:id",
        element: <Community />,
      },
      {
        path: "/post/:id",
        element: <Post />,
      },
      {
        path: "/profile",
        element: <Profile />,
      },
    ],
  },
];

export { routes };
