import { Button } from "./Button.tsx";
import { Link } from "react-router-dom";

type Props = {
  href: string;
  label: string;
  variant?: "primary" | "secondary";
  size?: "small" | "medium" | "large";
};

const ButtonLink = ({
  href,
  label,
  variant = "primary",
  size = "medium",
}: Props) => {
  return (
    <Link to={href}>
      <Button label={label} variant={variant} size={size} />
    </Link>
  );
};

export { ButtonLink };
