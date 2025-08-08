import { type FormEvent, useRef, useState } from "react";
import { Button } from "../ui/Button.tsx";
import { Modal } from "../ui/Modal.tsx";
import { Form } from "../ui/Form.tsx";
import { FormInput } from "../ui/FormInput.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { updateCommunity } from "../../redux/slices/currentCommunitySlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

type Props = {
  id: number;
  name: string;
  description: string;
};
const ButtonUpdateCommunity = ({ id, name, description }: Props) => {
  const dispatch = useDispatch<RootDispatch>();
  const nameRef = useRef<HTMLInputElement>(null);
  const descriptionRef = useRef<HTMLInputElement>(null);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    const newName = nameRef.current?.value;
    const newDescription = descriptionRef.current?.value;
    if (!newName || !newDescription) {
      dispatch(
        setNotification({
          type: "error",
          message: "Please fill all fields",
        }),
      );
      return;
    }
    try {
      const response = await dispatch(
        updateCommunity({ id, name: newName!, description: newDescription! }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      setIsModalOpen(false);
      nameRef.current!.value = "";
      descriptionRef.current!.value = "";
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      const { notification } = error;
      dispatch(setNotification(notification));
    }
  }

  return (
    <>
      <Button
        label={"Update Community"}
        variant={"primary"}
        size={"small"}
        onClick={() => setIsModalOpen(true)}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <Form onSubmit={handleSubmit} title={"Update Community"}>
            <FormInput
              type={"text"}
              name={"name"}
              label={"Name"}
              min={3}
              ref={nameRef}
              value={name}
            />
            <FormInput
              type={"text"}
              name={"description"}
              ref={descriptionRef}
              label={"Description"}
              min={3}
              value={description}
            />
            <Button label={"Update"} variant={"primary"} type={"submit"} />
          </Form>
        </Modal>
      )}
    </>
  );
};

export { ButtonUpdateCommunity };
