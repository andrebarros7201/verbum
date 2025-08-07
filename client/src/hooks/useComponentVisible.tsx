import { useEffect, useRef, useState } from "react";

const useComponentVisible = <T extends HTMLElement>() => {
  const [isVisible, setIsVisible] = useState<boolean>(false);
  const ref = useRef<T>(null);

  const handleClickOutside = (event: MouseEvent) => {
    if (
      ref.current &&
      event.target instanceof Node &&
      !ref.current.contains(event.target)
    ) {
      setIsVisible(false);
    }
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return { ref, isVisible, setIsVisible };
};

export { useComponentVisible };
