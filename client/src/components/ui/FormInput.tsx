import { forwardRef } from "react";

interface Props {
  placeholder?: string;
  type: string;
  name: string;
  min?: number;
  max?: number;
  label?: string;
  value?: string | number;
}

const FormInput = forwardRef<HTMLInputElement, Props>(
  ({ placeholder, type, name, min, max, value, label }, ref) => {
    return (
      <div className={"flex flex-col items-start gap-2 text-nowrap w-full"}>
        {label ? <label htmlFor={name}>{label}</label> : null}
        <input
          type={type}
          required
          id={name}
          name={name}
          min={min}
          max={max}
          ref={ref}
          value={value}
          placeholder={placeholder}
          className="p-2 bg-gray-300 rounded w-full text-black border-b-2 box-border outline-none border-b-amber-600 placeholder:italic appearance-none [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none [&::-moz-appearance:textfield]"
        />
      </div>
    );
  },
);

FormInput.displayName = "Input";

export { FormInput };
