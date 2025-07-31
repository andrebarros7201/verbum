import { Button } from "./Button.tsx";
import { Link } from "react-router-dom";

type Props = {
  href: string;
  label: string;
  variant?: "primary" | "secondary";
};

const ButtonLink = ({ href, label, variant = "primary" }: Props) => {
  return (
    <Link to={href}>
      <Button label={label} variant={variant} />
    </Link>
  );
};

export { ButtonLink };
