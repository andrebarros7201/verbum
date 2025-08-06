import { useEffect, useState } from "react";
import type { RootDispatch, RootState } from "../redux/store.ts";
import { useDispatch, useSelector } from "react-redux";
import { setNotification } from "../redux/slices/notificationSlice.ts";
import type { IReturnNotification } from "../interfaces/IReturnNotification.ts";
import { userMe } from "../redux/slices/userSlice.ts";
import { List } from "../components/List.tsx";
import { Button } from "../components/ui/Button.tsx";
import { CommentsList } from "../components/post/CommentsList.tsx";

const Profile = () => {
  const { communities, posts, comments } = useSelector(
    (state: RootState) => state.user,
  );
  const [index, setIndex] = useState<number>(0);

  const handleClick = (index: number) => {
    setIndex(index);
  };

  const items = [
    {
      type: "Communities",
      component: <List list={communities} type={"community"} />,
    },
    {
      type: "Posts",
      component: <List list={posts} type={"post"} />,
    },
    {
      type: "Comment",
      component: <CommentsList list={comments} />,
    },
  ];

  const dispatch = useDispatch<RootDispatch>();
  useEffect(() => {
    const fetchMe = async () => {
      try {
        await dispatch(userMe());
      } catch (e) {
        const err = e as { notification: IReturnNotification };
        dispatch(setNotification(err.notification));
      }
    };
    fetchMe();
  }, [communities, posts, comments, dispatch, setIndex, index, items]);
  return (
    <div className={"w-full flex flex-col gap-4"}>
      <div className={"w-full flex flex-start items-center gap-4"}>
        {items.map((item, index) => (
          <Button
            label={item.type}
            variant={"primary"}
            size={"small"}
            onClick={() => handleClick(index)}
          />
        ))}
      </div>
      <div className={"w-full"}>{items[index].component}</div>
    </div>
  );
};
export { Profile };
