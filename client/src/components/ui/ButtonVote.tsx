import { clsx } from "clsx";

type Props = {
  type: "post" | "comment";
  value: -1 | 1;
  id: number;
};

const ButtonVote = ({ type, value, id }: Props) => {
  async function onClick() {}

  return (
    <button
      className={clsx(
        "px-2 py-1 rounded text-white hover:shadow-xl cursor-pointer",
        {
          "bg-blue-400": value === -1,
          "bg-amber-600": value === 1,
        },
      )}
      onClick={onClick}
    >
      {value}
    </button>
  );
};
export { ButtonVote };
