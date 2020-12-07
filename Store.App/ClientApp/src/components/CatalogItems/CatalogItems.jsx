import React from "react";
import styles from "./CatalogItems.module.css";

const CatalogItems = ({ products, onAddToCart }) => {
  const items = products.map((p) => {
    return (
      <div className={styles.card} key={p.id}>
        <div className={styles.image}>
          <img alt="" src={`/img/${p.image}`} />
        </div>
        <div className="description">
          <span className={styles.name}>{p.name}</span>
          <span className={styles.price}>${p.price}</span>
        </div>
        <button className="add" onClick={() => onAddToCart(p.id)}>
          Add to cart
        </button>
      </div>
    );
  });

  return <div className={styles.items}>{items}</div>;
};

export default CatalogItems;
