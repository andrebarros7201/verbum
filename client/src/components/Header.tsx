import { ButtonLink } from "./ui/ButtonLink";

const Header = () => {
  return (
    <div
      className={
        "w-full border-b-2 border-b-amber-600 py-4 flex gap-4 justify-between"
      }
    >
      <h2 className={"italic font-bold text-2xl"}>Verbum</h2>
      <div className={"flex gap-4"}>
        <ButtonLink href={"/register"} label={"Register"} />
        <ButtonLink href={"/login"} label={"Login"} variant={"primary"} />
      </div>
    </div>
  );
};

export { Header };
