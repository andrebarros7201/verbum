import { useNavigate } from "react-router-dom";
import type { IPostSimple } from "../interfaces/IPostSimple";
import { Button } from "./ui/Button";

type Props = {
  post: IPostSimple;
};
const ListFilterPostItem = ({ post }: Props) => {
  const navigate = useNavigate();

  function handleClick() {
    navigate(`/post/${post.id}`);
  }
  return (
    <div className={"w-full flex items-center justify-between"}>
      <h3>{post.title}</h3>
      <Button label={"Go To"} variant={"primary"} onClick={handleClick} />
    </div>
  );
};

export { ListFilterPostItem };
