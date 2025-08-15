import { useNavigate } from "react-router-dom";
import type { ICommunitySimple } from "../interfaces/ICommunitySimple";
import { Button } from "./ui/Button";

type Props = {
  community: ICommunitySimple;
};

const ListFilterCommunityItem = ({ community }: Props) => {
  const navigate = useNavigate();
  function handleClick() {
    navigate(`/community/${community.id}`);
  }
  return (
    <div className={"w-full flex justify-between items-center "}>
      <h3>{community.name}</h3>
      <Button
        label="Go To"
        size="small"
        variant="primary"
        onClick={handleClick}
      />
    </div>
  );
};

export { ListFilterCommunityItem };
