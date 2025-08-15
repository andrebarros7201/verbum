import { Button } from "../ui/Button.tsx";

type Props = {
  id: number;
  username: string;
  isAdmin: boolean;
};

const MemberListItem = ({ id, username, isAdmin }: Props) => {
  return (
    <div className={"flex gap-2 items-center justify-between w-full"} key={id}>
      <p className={"font-bold"}>{username}</p>
      <Button
        label={isAdmin ? "Demote" : "Promote"}
        variant={"secondary"}
        size={"small"}
      />
    </div>
  );
};

export { MemberListItem };
