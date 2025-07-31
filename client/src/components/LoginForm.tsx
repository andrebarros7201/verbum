import { Form } from "./ui/Form.tsx";
import { FormInput } from "./ui/Input.tsx";
import { Button } from "./ui/Button.tsx";
import { type FormEvent, useRef } from "react";
import { userLogin } from "../redux/slices/userSlice.ts";
import { useDispatch } from "react-redux";
import type { RootDispatch } from "../redux/store.ts";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";

const LoginForm = () => {
  const dispatch = useDispatch<RootDispatch>();
  const usernameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);

  async function onSubmit(e: FormEvent) {
    e.preventDefault();
    const username = usernameRef.current?.value;
    const password = passwordRef.current?.value;

    if (!username || !password) {
      return;
    }

    try {
      const response = await dispatch(
        userLogin({ username, password }),
      ).unwrap();
      const { notification } = response;
      dispatch(setNotification(notification));
    } catch (e) {
      const err = e as { notification: IReturnNotification };
      const { notification } = err;
      dispatch(setNotification(notification));
    }
  }

  return (
    <main className={"w-full max-w-2xl flex justify-center"}>
      <Form onSubmit={onSubmit}>
        <FormInput
          type={"text"}
          name={"username"}
          label={"Username"}
          min={3}
          ref={usernameRef}
        />
        <FormInput
          type={"password"}
          name={"password"}
          label={"Password"}
          min={6}
          ref={passwordRef}
        />
        <Button label={"Login"} variant={"primary"} type={"submit"} />
      </Form>
    </main>
  );
};

export { LoginForm };
