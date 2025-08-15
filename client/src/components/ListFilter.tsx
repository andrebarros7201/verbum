import { useEffect, useState } from "react";
import { Button } from "./ui/Button.tsx";
import { Modal } from "./ui/Modal.tsx";
import { FormInput } from "./ui/FormInput.tsx";
import { useDispatch, useSelector } from "react-redux";
import type { RootDispatch, RootState } from "../redux/store.ts";
import { filterCommunities } from "../redux/slices/communitySlice.ts";
import { ListFilterCommunityItem } from "./ListFilterCommunityItem.tsx";
import {
  filterMembers,
  filterPosts,
} from "../redux/slices/currentCommunitySlice.ts";
import { ListFilterPostItem } from "./ListFilterPostItem.tsx";
import { MemberListItem } from "./Community/MemberListItem.tsx";

type Props = {
  type: "community" | "post" | "member";
  label?: string;
};

const ListFilter = ({ label = "Find", type }: Props) => {
  const { filteredCommunities } = useSelector(
    (state: RootState) => state.community,
  );
  const { filteredMembers, filteredPosts } = useSelector(
    (state: RootState) => state.currentCommunity,
  );
  const [searchText, setSearchText] = useState("");
  const [isInputVisible, setIsInputVisible] = useState<boolean>(false);
  const dispatch = useDispatch<RootDispatch>();

  useEffect(() => {
    switch (type) {
      case "community":
        dispatch(filterCommunities({ searchText }));
        break;

      case "post":
        dispatch(filterPosts({ searchText }));
        break;

      case "member":
        dispatch(filterMembers({ searchText }));
        break;
    }
  }, [searchText, dispatch, type]);

  useEffect(() => {}, []);

  return (
    <>
      <Button
        label={label}
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
            <div className="w-full h-[250px] flex flex-col gap-4 justify-start items-center overflow-y-auto">
              {type === "community" && filteredCommunities
                ? filteredCommunities.map((x) => (
                    <ListFilterCommunityItem community={x} key={x.id} />
                  ))
                : type === "post" && filteredPosts
                  ? filteredPosts.map((x) => (
                      <ListFilterPostItem post={x} key={x.id} />
                    ))
                  : filteredMembers &&
                    filteredMembers.map((x) => (
                      <MemberListItem
                        id={x.id}
                        username={x.username}
                        isAdmin={x.isAdmin}
                        key={x.id}
                      />
                    ))}
            </div>
          </div>
        </Modal>
      )}
    </>
  );
};

export { ListFilter };
