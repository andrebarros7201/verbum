import clsx from "clsx";

type Props = {
  label: string;
  type: "submit" | "button";
  variant: "primary" | "secondary";
  onClick?: () => void;
};

const Button = ({ label, variant, onClick, type = "button" }: Props) => {
  return (
    <button
      type={type}
      className={clsx(`text-white px-4 py-2 rounded-md cursor-pointer`, {
        "bg-amber-600": variant === "primary",
        "bg-gray-400": variant === "secondary",
      })}
      onClick={onClick}
    >
      {label}
    </button>
  );
};

export { Button };
