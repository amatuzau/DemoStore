import React from "react";
import { connect } from "react-redux";
import { getCart } from "../../redux/reducers/cart/actions";
import { openModal } from "../../redux/reducers/order/actions";

import CartItems from "../CartItems/CartItems";
import Preloader from "../Preloader/Preloader";

class Cart extends React.Component {
  componentDidMount() {
    this.props.getCart();
  }

  render() {
    return this.props.isLoading ? (
      <Preloader color={"black"} />
    ) : (
      <>
        <CartItems items={this.props.cart.items} />
        <button onClick={this.props.openModal}>Place order</button>
      </>
    );
  }
}

const mapStateToProps = (state) => ({
  isLoading: state.cart.isLoading,
  cart: state.cart.cartContent,
});

export default connect(mapStateToProps, { getCart, openModal })(Cart);
