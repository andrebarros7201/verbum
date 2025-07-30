import { Button } from "./Button.tsx";

type Props = {
  href: string;
  label: string;
  variant?: "primary" | "secondary";
};

const ButtonLink = ({ href, label, variant = "primary" }: Props) => {
  return (
    <a href={href}>
      <Button label={label} variant={variant} />
    </a>
  );
};

export { ButtonLink };
