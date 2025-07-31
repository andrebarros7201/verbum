import { Link } from "react-router-dom";

const HeaderLogo = () => {
  return (
    <Link to={"/"}>
      <h2
        className={
          "italic font-bold text-2xl border-2 px-4 cursor-pointer hover:text-amber-600 hover:border-amber-600 transition-all duration-300"
        }
      >
        Verbum
      </h2>
    </Link>
  );
};

export { HeaderLogo };
