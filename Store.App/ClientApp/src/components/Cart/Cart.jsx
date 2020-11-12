import React from "react";
import style from "./Cart.module.css";

const Cart = () => {
  return (
    <>
      <div className={`${style.item} ${style.odd}`}>
        <h3>Item1</h3>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad
          dignissimos ex illo minus natus quasi. Accusantium asperiores cumque
          enim fugiat, inventore iste libero nobis reiciendis tempora totam unde
          vero, voluptatibus?
        </p>
      </div>
      <div className={`${style.item} ${style.even}`}>
        <h3>Item2</h3>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto
          at autem deleniti dignissimos dolor dolores ducimus enim eum fugit
          iusto necessitatibus nihil nostrum odit pariatur repellendus sapiente
          suscipit, temporibus voluptatem!
        </p>
      </div>
      <div className={`${style.item} ${style.odd}`}>
        <h3>Item3</h3>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto
          at autem deleniti dignissimos dolor dolores ducimus enim eum fugit
          iusto necessitatibus nihil nostrum odit pariatur repellendus sapiente
          suscipit, temporibus voluptatem!
        </p>
      </div>
    </>
  );
};

export default Cart;
