import { Link, useLocation } from "react-router-dom";
import clsx from "clsx";

type Props = {
  label: string;
  href: string;
};

const HeaderLink = ({ label, href }: Props) => {
  const location = useLocation();
  const active = location.pathname === href;
  return (
    <Link
      to={href}
      className={clsx(
        "border-b-2 border-transparent text-amber-600 cursor-pointer inline-block text-lg font-bold px-2",
        {
          " border-b-amber-600": active,
        },
      )}
    >
      {label}
    </Link>
  );
};

export { HeaderLink };
