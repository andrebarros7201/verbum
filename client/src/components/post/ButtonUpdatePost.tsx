import { type FormEvent, useRef, useState } from "react";
import { Button } from "../ui/Button";
import { Modal } from "../ui/Modal.tsx";
import { Form } from "../ui/Form.tsx";
import { FormInput } from "../ui/FormInput.tsx";
import { FormTextArea } from "../ui/FormTextArea.tsx";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch } from "react-redux";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { updatePost } from "../../redux/slices/currentPostSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";

type Props = {
  postId: number;
  title: string;
  text: string;
};

const ButtonUpdatePost = ({ postId, title, text }: Props) => {
  const dispatch = useDispatch<RootDispatch>();
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const titleRef = useRef<HTMLInputElement>(null);
  const textRef = useRef<HTMLTextAreaElement>(null);

  async function handleUpdate(e: FormEvent) {
    e.preventDefault();

    const newTitle = titleRef.current?.value;
    const newText = textRef.current?.value;
    if (!title || !text) {
      dispatch(
        setNotification({
          type: "error",
          message: "Please fill all fields",
        }),
      );
    }

    try {
      const response = await dispatch(
        updatePost({ postId, title: newTitle!, text: newText! }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
      setIsModalOpen(false);
      titleRef.current!.value = "";
      textRef.current!.value = "";
    } catch (e) {
      const error = e as { notification: IReturnNotification };
      const { notification } = error;
      dispatch(setNotification(notification));
    }
  }

  return (
    <>
      <Button
        label={"Update"}
        variant={"primary"}
        size={"small"}
        onClick={() => setIsModalOpen(true)}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <Form onSubmit={handleUpdate}>
            <FormInput
              min={3}
              max={100}
              value={title}
              type={"text"}
              name={"title"}
              ref={titleRef}
              label={"Title"}
            />
            <FormTextArea
              min={3}
              max={500}
              name={"text"}
              value={text}
              ref={textRef}
              label={"Text"}
            />
            <Button label={"Update"} variant={"primary"} type={"submit"} />
          </Form>
        </Modal>
      )}
    </>
  );
};
export { ButtonUpdatePost };
