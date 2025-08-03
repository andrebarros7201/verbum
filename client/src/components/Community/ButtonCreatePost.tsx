import { Button } from "../ui/Button.tsx";
import { Modal } from "../ui/Modal.tsx";
import { FormInput } from "../ui/FormInput.tsx";
import { type FormEvent, useRef, useState } from "react";
import { Form } from "../ui/Form.tsx";
import { FormTextArea } from "../ui/FormTextArea.tsx";
import type { IReturnNotification } from "../../interfaces/IReturnNotification.ts";
import type { RootDispatch } from "../../redux/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { setNotification } from "../../redux/slices/notificationSlice.ts";
import { createPost } from "../../redux/slices/currentCommunitySlice.ts";

const ButtonCreatePost = () => {
  const { community } = useSelector((state: any) => state.currentCommunity);
  const dispatch = useDispatch<RootDispatch>();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const titleRef = useRef<HTMLInputElement>(null);
  const textRef = useRef<HTMLTextAreaElement>(null);

  async function handleCreatePost(e: FormEvent) {
    e.preventDefault();
    const title = titleRef.current?.value;
    const text = textRef.current?.value;
    if (!title || !text) {
      return;
    }

    try {
      const response = await dispatch(createPost({ title, text })).unwrap();
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
        isDisabled={!community.isMember}
        label={"Create Post"}
        variant={"primary"}
        onClick={() => {
          setIsModalOpen(true);
        }}
      />
      {isModalOpen && (
        <Modal onClose={() => setIsModalOpen(false)}>
          <Form onSubmit={handleCreatePost} title={"Create Post"}>
            <FormInput
              type={"text"}
              name={"title"}
              label={"Title"}
              min={3}
              ref={titleRef}
            />
            <FormTextArea name={"text"} label={"text"} min={3} ref={textRef} />
            <Button label={"Create"} variant={"primary"} type={"submit"} />
          </Form>
        </Modal>
      )}
    </>
  );
};
export { ButtonCreatePost };
