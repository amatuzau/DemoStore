import React from "react";
import styles from "./Preloader.module.css";

const Preloader = ({ color }) => {
  const colorClassName = color && styles[color] ? styles[color] : styles.white;
  return (
    <div className={`${styles.ldsRoller} ${colorClassName}`}>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
    </div>
  );
};

export default Preloader;
