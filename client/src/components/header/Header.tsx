import { HeaderLogo } from "./HeaderLogo.tsx";
import { MobileHeader } from "./MobileHeader.tsx";
import { DesktopHeader } from "./DesktopHeader.tsx";

const Header = () => {
  return (
    <div
      className={
        "w-full border-b-2 border-b-sky-600 py-2 flex gap-4 justify-between items-center"
      }
    >
      <HeaderLogo />
      <DesktopHeader />
      <MobileHeader />
    </div>
  );
};

export { Header };
