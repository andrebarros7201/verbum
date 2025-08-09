import { type ReactNode } from "react";
import { DropdownMenu } from "./DropdownMenu.tsx";

type Props = {
  children: ReactNode | ReactNode[];
};

const MobileMenu = ({ children }: Props) => {
  return (
    <div className={"block sm:hidden"}>
      <DropdownMenu>{children}</DropdownMenu>
    </div>
  );
};

export { MobileMenu };
