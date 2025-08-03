import type { ReactNode } from "react";

type Props = {
  children: ReactNode | ReactNode[];
  onClose: () => void;
};
const Modal = ({ children, onClose }: Props) => {
  return (
    <div
      onClick={onClose}
      className={
        "w-full h-full absolute top-0 left-0 bg-gray-600/60 backdrop-blur-sm flex-1 flex flex-col justify-start items-center md:justify-center"
      }
    >
      <div
        onClick={(e) => e.stopPropagation()}
        className={"border-2 border-amber-600 rounded bg-white"}
      >
        {children}
      </div>
    </div>
  );
};

export { Modal };
