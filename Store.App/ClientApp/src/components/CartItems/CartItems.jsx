import React from "react";
import classNames from "classnames";
import styles from "./CartItems.module.css";

const CartItems = (props) => {
  return props.items.map((item, index) => {
    const isEven = index % 2 === 1;
    return (
      <div
        className={classNames({
          [styles.item]: true,
          [styles.even]: isEven,
          [styles.odd]: !isEven,
        })}
        key={item.product.id}
      >
        <h3>{item.product.name}</h3>
        <p>Amount: {item.amount}</p>
        <p>Price: {item.product.price}</p>
      </div>
    );
  });
};

export default CartItems;
