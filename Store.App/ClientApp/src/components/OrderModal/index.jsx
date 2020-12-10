import React, { Component } from "react";
import Modal from "react-modal";
import { connect } from "react-redux";
import { closeModal, sendOrder } from "../../redux/reducers/order/actions";
import OrderForm from "../OrderForm";

Modal.setAppElement("#root");

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)",
  },
};

class OrderModal extends Component {
  render() {
    const { isModalOpened, closeModal, sendOrder } = this.props;

    return (
      <div>
        <Modal
          isOpen={isModalOpened}
          style={customStyles}
          shouldCloseOnOverlayClick
        >
          <h2>Provide order details</h2>
          <OrderForm onSubmit={(values) => sendOrder(values)} />
          <button onClick={closeModal}>Close</button>
        </Modal>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    isModalOpened: state.order.isModalOpened,
  };
}

export default connect(mapStateToProps, { closeModal, sendOrder })(OrderModal);
