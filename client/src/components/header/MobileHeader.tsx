import menuSvg from "../../assets/menu.svg";
import { useState } from "react";
import { Button } from "../ui/Button.tsx";
import { HeaderLink } from "./HeaderLink.tsx";
import type { RootState } from "../../redux/store.ts";
import { useSelector } from "react-redux";
import { ButtonLogout } from "./ButtonLogout.tsx";

const MobileHeader = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.user);
  const [isOpen, setIsOpen] = useState(false);
  return (
    <div className={"flex flex-col sm:hidden"}>
      <button onClick={() => setIsOpen(true)}>
        <img src={menuSvg} alt={"menu icon"} width={32} />
      </button>
      {isOpen && (
        <div
          className={
            "h-screen w-screen backdrop-blur-md flex justify-end absolute top-0 right-0"
          }
          onClick={() => setIsOpen(false)}
        >
          <div
            className={
              "h-screen w-4/5 flex flex-col items-center justify-start border-2 border-sky-600 rounded-bl rounded-tl absolute top-0 right-0 bg-gray-200 p-8 gap-6"
            }
            onClick={(e) => e.stopPropagation()}
          >
            <Button
              label={"Close"}
              variant={"secondary"}
              size={"small"}
              onClick={() => setIsOpen(false)}
            />
            <HeaderLink label={"Communities"} href={"/communities"} />
            {isAuthenticated ? (
              <>
                <HeaderLink href={"/profile"} label={"Profile"} />
                <ButtonLogout />
              </>
            ) : (
              <>
                <HeaderLink href={"/register"} label={"Register"} />
                <HeaderLink href={"/login"} label={"Login"} />
              </>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export { MobileHeader };
