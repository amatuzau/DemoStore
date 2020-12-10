import classNames from "classnames";
import React, { Component } from "react";
import { connect } from "react-redux";
import { lockOrder } from "../../redux/reducers/order/actions";
import styles from "./style.module.css";

class Orders extends Component {
  render() {
    const { orders, lockedOrders, lockOrder, user } = this.props;
    return (
      <div>
        {orders.map((order) => {
          const isLocked = lockedOrders[order.id] !== undefined;
          const classes = classNames({
            [styles.order]: true,
            [styles.locked]: isLocked,
          });

          return (
            <div key={order.id} className={classes}>
              <div>{order.userId}</div>
              <div>{order.address}</div>
              <div>{order.phone}</div>
              <div>{order.total}</div>
              {!isLocked && (
                <button
                  onClick={() =>
                    lockOrder({ userId: user.sub, orderId: order.id })
                  }
                >
                  Lock Order
                </button>
              )}
            </div>
          );
        })}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    orders: state.order.orders,
    lockedOrders: state.order.lockedOrders,
    user: state.oidc.user.profile,
  };
}

export default connect(mapStateToProps, { lockOrder })(Orders);
