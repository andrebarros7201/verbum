import { Button } from "./Button.tsx";

type Props = {
  href: string;
  label: string;
};

const ButtonLink = ({ href, label }: Props) => {
  return (
    <a href={href}>
      <Button label={label} variant={"secondary"} />
    </a>
  );
};

export { ButtonLink };
