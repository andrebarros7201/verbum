import { type FormEvent, useRef, useState } from "react";
import { Modal } from "../ui/Modal.tsx";
import { Button } from "../ui/Button.tsx";
import { Form } from "../ui/Form.tsx";
import { FormInput } from "../ui/FormInput.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { createCommunity } from "../../redux/slices/communitySlice.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

const ButtonCreateCommunity = () => {
  const dispatch = useDispatch<RootDispatch>();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const nameRef = useRef<HTMLInputElement>(null);
  const descriptionRef = useRef<HTMLInputElement>(null);

  async function handleCreateCommunity(e: FormEvent) {
    e.preventDefault();
    const name = nameRef.current?.value;
    const description = descriptionRef.current?.value;

    if (!name || !description) {
      return;
    }

    try {
      const response = await dispatch(
        createCommunity({ name, description }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      setIsModalOpen(false);
      nameRef.current!.value = "";
      descriptionRef.current!.value = "";
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    <>
      <Button
        label={"Create Community"}
        variant={"primary"}
        size={"small"}
        onClick={() => setIsModalOpen(true)}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <Form onSubmit={handleCreateCommunity} title={"Create Community"}>
            <FormInput
              type={"text"}
              name={"name"}
              label={"Name"}
              min={3}
              ref={nameRef}
            />
            <FormInput
              type={"text"}
              name={"description"}
              label={"Description"}
              min={3}
              ref={descriptionRef}
            />
            <Button label={"Create"} variant={"primary"} type={"submit"} />
          </Form>
        </Modal>
      )}
    </>
  );
};
export { ButtonCreateCommunity };
