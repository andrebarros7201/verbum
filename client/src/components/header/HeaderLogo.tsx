import { Link } from "react-router-dom";

const HeaderLogo = () => {
  return (
    <Link to={"/"}>
      <h2
        className={
          "italic font-bold text-2xl border-2 px-4 cursor-pointer hover:text-sky-600 hover:border-sky-600 transition-all duration-300"
        }
      >
        Verbum
      </h2>
    </Link>
  );
};

export { HeaderLogo };
