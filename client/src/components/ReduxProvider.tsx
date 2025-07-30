import { Provider } from "react-redux";
import { store } from "../redux/store";
import type { ReactNode } from "react";

type Props = {
  children: ReactNode[];
};

export const ReduxProvider = ({ children }: Props) => {
  return <Provider store={store}>{children}</Provider>;
};
