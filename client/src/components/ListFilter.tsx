import { useEffect, useState } from "react";
import { Button } from "./ui/Button.tsx";
import { Modal } from "./ui/Modal.tsx";
import { FormInput } from "./ui/FormInput.tsx";

type Props = {
  type: "community" | "post" | "member";
};

const ListFilter = ({ type }: Props) => {
  const [searchText, setSearchText] = useState("");
  const [isInputVisible, setIsInputVisible] = useState<boolean>(false);

  useEffect(() => {
    if (searchText?.length > 3) {
      console.log(searchText);
    }
  }, [searchText]);

  return (
    <div
      className={
        "w-full max-w-[200px] gap-4 flex justify-start items-center box-border"
      }
    >
      <Button
        label={"Find"}
        variant={"primary"}
        size={"small"}
        onClick={() => setIsInputVisible(!isInputVisible)}
      />

      {isInputVisible && (
        <Modal onClose={() => setIsInputVisible(false)}>
          <div className={"flex flex-col gap-4 w-full h-full p-4"}>
            <FormInput
              type={"text"}
              name={"searchText"}
              placeholder={
                type === "community"
                  ? "Search Community"
                  : type === "post"
                    ? "Search Post"
                    : "Search Member"
              }
              value={searchText}
              onChange={(e) => setSearchText(e.target.value)}
            />
          </div>
        </Modal>
      )}
    </div>
  );
};

export { ListFilter };
