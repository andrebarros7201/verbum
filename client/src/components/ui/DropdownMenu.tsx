import { type ReactNode } from "react";
import { useComponentVisible } from "../../hooks/useComponentVisible.tsx";
import threeDots from "../../assets/three-dots.svg";

type Props = {
  children: ReactNode | ReactNode[];
};
const DropdownMenu = ({ children }: Props) => {
  const { ref, isVisible, setIsVisible } =
    useComponentVisible<HTMLDivElement>();

  return (
    <div ref={ref} className={"relative inline-block"}>
      <button
        className={
          "cursor-pointer hover:bg-gray-300 transition-all duration-300 rounded p-2"
        }
        onClick={() => setIsVisible(!isVisible)}
      >
        <img src={threeDots} alt="three dots" width={20} />
      </button>
      {isVisible && (
        <div
          className={
            "absolute flex-1 top-0 right-8 bg-white flex flex-col justify-start z-100 rounded"
          }
        >
          {children}
        </div>
      )}
    </div>
  );
};

export { DropdownMenu };
