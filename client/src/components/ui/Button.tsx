import clsx from "clsx";

type Props = {
  label: string;
  type?: "submit" | "button";
  variant: "primary" | "secondary";
  onClick?: () => void;
  isDisabled?: boolean;
  size?: "small" | "medium" | "large";
  selected?: boolean;
};

const Button = ({
  label,
  variant,
  onClick,
  type = "button",
  isDisabled = false,
  size = "medium",
  selected = false,
}: Props) => {
  return (
    <button
      disabled={isDisabled}
      type={type}
      className={clsx(
        `font-semibold text-sm rounded-md border-2  hover:shadow-xl transition-all duration-300`,
        {
          // Primary
          "bg-sky-600 border-transparent text-white hover:bg-transparent hover:border-sky-600 hover:text-sky-600":
            variant === "primary" && selected === false,
          "border-sky-600 text-sky-600":
            selected === true && variant === "primary",

          // Secondary
          "bg-red-700 border-transparent text-white hover:bg-transparent hover:border-red-700 hover:text-red-700":
            variant === "secondary" && !selected,
          "border-red-600 text-red-600": selected && variant === "secondary",

          // Sizes
          "px-3 py-2": size === "small",
          "px-5 py-3": size === "medium",
          "px-7 py-4": size === "large",

          // Is Disabled
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
