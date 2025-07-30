import type { FormEvent, ReactNode } from "react";

type Props = {
  children: ReactNode[];
  onSubmit: (e: FormEvent) => void;
};

const Form = ({ children, onSubmit }: Props) => {
  return (
    <form
      onSubmit={onSubmit}
      className={"flex flex-col gap-4 p-4 rounded w-full"}
    >
      {children}
    </form>
  );
};

export { Form };
