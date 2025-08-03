import { forwardRef } from "react";

interface Props {
  placeholder?: string;
  name: string;
  min?: number;
  max?: number;
  label?: string;
  value?: string | number;
}

const FormTextArea = forwardRef<HTMLTextAreaElement, Props>(
  ({ placeholder, name, min, max, value, label }, ref) => {
    return (
      <div className={"flex flex-col items-start gap-2 text-nowrap w-full"}>
        {label ? <label htmlFor={name}>{label}</label> : null}
        <textarea
          required
          id={name}
          name={name}
          minLength={min}
          maxLength={max}
          ref={ref}
          value={value}
          placeholder={placeholder}
          className="p-2 bg-gray-300 rounded w-full resize-y min-h-12 text-black border-b-2 box-border outline-none border-b-amber-600 placeholder:italic appearance-none [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none [&::-moz-appearance:textfield]"
        />
      </div>
    );
  },
);

FormTextArea.displayName = "TextArea";

export { FormTextArea };
