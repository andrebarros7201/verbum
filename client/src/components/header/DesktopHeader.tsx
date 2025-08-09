import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";
import { HeaderLink } from "./HeaderLink.tsx";
import { ButtonLink } from "../ui/ButtonLink.tsx";
import { ButtonLogout } from "./ButtonLogout.tsx";

const DesktopHeader = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);

  return (
    <div className={"hidden w-full sm:flex flex-row justify-between"}>
      <div className={"flex gap-4 items-center justify-start"}>
        <HeaderLink href={"/communities"} label={"Communities"} />
      </div>
      <div className={"flex gap-4 items-center"}>
        {isAuthenticated ? (
          <>
            <HeaderLink href={"/profile"} label={"My Profile"} />
            <ButtonLogout />
          </>
        ) : (
          <>
            <ButtonLink href={"/register"} label={"Register"} size={"small"} />
            <ButtonLink
              href={"/login"}
              label={"Login"}
              variant={"primary"}
              size={"small"}
            />
          </>
        )}
      </div>
    </div>
  );
};
export { DesktopHeader };
