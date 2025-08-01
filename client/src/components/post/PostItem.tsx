import { useSelector } from "react-redux";
import type { RootState } from "../../redux/store.ts";

const PostItem = () => {
  const { isLoading, post } = useSelector(
    (state: RootState) => state.currentPost,
  );

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    post && (
      <main className={"w-full flex-1 flex flex-col gap-4 "}>
        <h3>{post.title}</h3>
      </main>
    )
  );
};
export { PostItem };
