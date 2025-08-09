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
        "w-full h-screen absolute top-0 left-0 p-4 bg-gray-600/60 backdrop-blur-sm flex-1 flex z-100 flex-col items-center justify-start md:justify-center"
      }
    >
      <div
        onClick={(e) => e.stopPropagation()}
        className={"border-2 border-sky-600 rounded bg-white max-w-2xl w-full"}
      >
        {children}
      </div>
    </div>
  );
};

export { Modal };
