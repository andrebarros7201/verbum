import clsx from "clsx";

type Props = {
  label: string;
  type?: "submit" | "button";
  variant: "primary" | "secondary";
  onClick?: () => void;
  isDisabled?: boolean;
  size?: "small" | "medium" | "large";
};

const Button = ({
  label,
  variant,
  onClick,
  type = "button",
  isDisabled = false,
  size = "medium",
}: Props) => {
  return (
    <button
      disabled={isDisabled}
      type={type}
      className={clsx(
        `text-white font-semibold text-sm rounded-md border-2 border-transparent hover:shadow-xl transition-all duration-300`,
        {
          "bg-sky-600 hover:bg-transparent hover:border-sky-600 hover:text-sky-600":
            variant === "primary",
          "bg-red-700 hover:bg-transparent hover:border-red-700 hover:text-red-700":
            variant === "secondary",
          "px-3 py-2": size === "small",
          "px-5 py-3": size === "medium",
          "px-7 py-4": size === "large",
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
