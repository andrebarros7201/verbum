import { useState } from "react";
import { Modal } from "../ui/Modal.tsx";
import { Button } from "../ui/Button.tsx";

const ButtonCreateCommunity = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <>
      <Button
        label={"Create Community"}
        variant={"primary"}
        onClick={() => setIsModalOpen(true)}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <div className={"p-4 bg-white"}></div>
        </Modal>
      )}
    </>
  );
};
export { ButtonCreateCommunity };
