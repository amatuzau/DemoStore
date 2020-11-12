import React from "react";
import styles from "./CatalogItems.module.css";

const CatalogItems = (props) => {
  const items = props.products.map((p) => {
    return (
      <div className={styles.card}>
        <div className={styles.image}>
          <img alt="" src={p.image} />
        </div>
        <div className="description">
          <span className={styles.name}>{p.name}</span>
          <span className={styles.price}>${p.price}</span>
        </div>
        <button className="add">Add to cart</button>
      </div>
    );
  });

  return <div className={styles.items}>{items}</div>;
};

export default CatalogItems;
