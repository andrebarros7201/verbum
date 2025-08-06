import { type FormEvent, useRef, useState } from "react";
import { Button } from "../ui/Button";
import { Form } from "../ui/Form.tsx";
import { FormTextArea } from "../ui/FormTextArea.tsx";
import { Modal } from "../ui/Modal.tsx";
import { useDispatch } from "react-redux";
import type { RootDispatch } from "../../redux/store.ts";
import { addComment } from "../../redux/slices/currentPostSlice.ts";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import { setNotification } from "../../redux/slices/notificationSlice.ts";

const ButtonAddComment = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const commentRef = useRef<HTMLTextAreaElement>(null);
  const dispatch = useDispatch<RootDispatch>();

  async function handleAddComment(e: FormEvent) {
    e.preventDefault();
    const comment = commentRef.current?.value;
    if (!comment) {
      return;
    }

    try {
      await dispatch(addComment({ text: comment })).unwrap();
      setIsModalOpen(false);
      commentRef.current!.value = "";
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    <>
      <Button
        label={"Add Comment"}
        onClick={() => setIsModalOpen(true)}
        variant={"primary"}
        size={"small"}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <Form onSubmit={handleAddComment}>
            <FormTextArea
              name={"comment"}
              label={"Comment"}
              ref={commentRef}
              min={3}
            />
            <Button label={"Add Comment"} variant={"primary"} type={"submit"} />
          </Form>
        </Modal>
      )}
    </>
  );
};

export { ButtonAddComment };
