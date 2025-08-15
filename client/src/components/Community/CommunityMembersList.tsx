import type { RootState } from "../../redux/store.ts";
import { useSelector } from "react-redux";
import { useState } from "react";
import { Button } from "../ui/Button.tsx";
import { Modal } from "../ui/Modal.tsx";
import { MemberListItem } from "./MemberListItem.tsx";

const CommunityMembersList = () => {
  const { community } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  return (
    <>
      {(community!.isAdmin || community!.isMember) && (
        <Button
          label={"Members"}
          variant={"primary"}
          size={"small"}
          onClick={() => setIsModalOpen(true)}
        />
      )}
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <div
            className={
              "flex flex-col gap-4 p-4 items-start justify-start w-full "
            }
          >
            <h2 className={"text-2xl font-bold mb-4"}>Members</h2>
            {community!.members.map((member) => (
              <MemberListItem
                id={member.id}
                username={member.username}
                isAdmin={member.isAdmin}
              />
            ))}
          </div>
        </Modal>
      )}
    </>
  );
};
export { CommunityMembersList };
