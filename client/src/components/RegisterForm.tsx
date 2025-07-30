import { Form } from "./ui/Form.tsx";
import { FormInput } from "./ui/Input.tsx";
import { Button } from "./ui/Button.tsx";
import type { FormEvent } from "react";

const RegisterForm = () => {
  function onSubmit(e: FormEvent) {
    e.preventDefault();
    console.log("submit");
  }

  return (
    <main className={"w-full max-w-2xl flex justify-center"}>
      <Form onSubmit={(e: FormEvent) => onSubmit(e)}>
        <FormInput type={"text"} name={"username"} label={"Username"} min={3} />
        <FormInput
          type={"password"}
          name={"password"}
          label={"Password"}
          min={6}
        />
        <Button label={"Register"} variant={"primary"} type={"submit"} />
      </Form>
    </main>
  );
};

export { RegisterForm };
