import clsx from "clsx";

type Props = {
  label: string;
  variant: "primary" | "secondary";
  onClick?: () => void;
};

const Button = ({ label, variant, onClick }: Props) => {
  return (
    <button
      className={clsx(`text-white px-4 py-2 rounded-md cursor-pointer`, {
        "bg-amber-600": variant === "primary",
        "bg-blue-600": variant === "secondary",
      })}
      onClick={onClick}
    >
      {label}
    </button>
  );
};

export { Button };
