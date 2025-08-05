import clsx from "clsx";

type Props = {
  label: string;
  type?: "submit" | "button";
  variant: "primary" | "secondary" | "danger";
  onClick?: () => void;
  isDisabled?: boolean;
};

const Button = ({
  label,
  variant,
  onClick,
  type = "button",
  isDisabled = false,
}: Props) => {
  return (
    <button
      disabled={isDisabled}
      type={type}
      className={clsx(
        `text-white px-4 py-2 rounded-md hover:shadow-xl transition-all duration-300`,
        {
          "bg-amber-600": variant === "primary",
          "bg-gray-400": variant === "secondary",
          "bg-red-600": variant === "danger",
          "cursor-not-allowed": isDisabled,
          "cursor-pointer": !isDisabled,
        },
      )}
      onClick={onClick}
    >
      {label}
    </button>
  );
};

export { Button };
