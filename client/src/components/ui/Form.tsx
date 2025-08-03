import type { FormEvent, ReactNode } from "react";

type Props = {
  children: ReactNode[];
  onSubmit: (e: FormEvent) => void;
  title: string;
};

const Form = ({ children, onSubmit, title }: Props) => {
  return (
    <form
      onSubmit={onSubmit}
      className={
        "flex flex-col gap-4 p-4 rounded w-full justify-center items-start"
      }
    >
      <h3 className={"font-bold text-xl text-amber-600"}>{title}</h3>
      {children}
    </form>
  );
};

export { Form };
