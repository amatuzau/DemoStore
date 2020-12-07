import React from "react";
import { Field, reduxForm } from "redux-form";

const OrderForm = (props) => {
  const { handleSubmit } = props;
  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor={"name"}>Name</label>
        <Field
          name={"name"}
          component={Input}
          type={"text"}
          validate={[required, minLength(3)]}
        />
      </div>
      <div>
        <label htmlFor={"address"}>Address</label>
        <Field
          name={"address"}
          component={Input}
          type={"text"}
          validate={[required]}
        />
      </div>
      <div>
        <label htmlFor={"phone"}>Phone</label>
        <Field
          name={"phone"}
          component={Input}
          type={"text"}
          validate={[required]}
        />
      </div>
      <button type={"submit"}>Submit</button>
    </form>
  );
};

const FormControl = ({ input, meta, ...rest }) => {
  const hasError = meta.touched && meta.error;
  return (
    <div>
      <div>{rest.children}</div>
      {hasError && <span>{meta.error}</span>}
    </div>
  );
};

export const Input = (props) => {
  const { input, meta, child, ...rest } = props;
  return (
    <FormControl {...props}>
      <input {...input} {...rest} />
    </FormControl>
  );
};

const required = (value) => {
  if (value) return undefined;

  return "Field is required";
};

const minLength = (min) => {
  return (value) => {
    if (value && value.length >= min) return undefined;

    return `Minimum length is ${min}`;
  };
};

export default reduxForm({ form: "contact" })(OrderForm);
