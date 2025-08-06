import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { ButtonLink } from "../ui/ButtonLink.tsx";
import { HeaderLogo } from "./HeaderLogo.tsx";
import { ButtonLogout } from "./ButtonLogout.tsx";
import { HeaderLink } from "./HeaderLink.tsx";

const Header = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
  return (
    <div
      className={
        "w-full border-b-2 border-b-sky-600 py-2 flex gap-4 justify-between"
      }
    >
      <div className={"flex gap-4 items-center justify-start"}>
        <HeaderLogo />
        <HeaderLink href={"/communities"} label={"Communities"} />
      </div>
      <div className={"flex gap-4"}>
        {isAuthenticated ? (
          <ButtonLogout />
        ) : (
          <>
            <ButtonLink href={"/register"} label={"Register"} />
            <ButtonLink href={"/login"} label={"Login"} variant={"primary"} />
          </>
        )}
      </div>
    </div>
  );
};

export { Header };
