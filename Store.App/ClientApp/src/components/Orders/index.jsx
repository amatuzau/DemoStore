import React, { Component } from "react";
import { connect } from "react-redux";
import { ordersHub } from "../../API/ordershub";

class Orders extends Component {
  constructor(props) {
    super(props);
    this.connection = ordersHub;
    this.connection.on("GetNewOrder", (order) => {
      this.setState({ orders: [...this.state.orders, order] });
    });
    this.state = { orders: [] };
  }

  componentDidMount() {
    this.connection.onclose(this.start);
    this.start();
  }

  sendMessage(clientId) {
    this.connection.invoke("GetNewOrder", { clientId });
  }

  async start() {
    try {
      await this.connection.start();
      console.log("SignalR Connected.");
    } catch (err) {
      console.log(err);
      setTimeout(this.start, 5000);
    }
  }

  render() {
    return (
      <div>
        {this.state.orders.map((order, index) => {
          return (
            <div key={order.id}>
              <div>{order.userId}</div>
              <div>{order.address}</div>
              <div>{order.phone}</div>
              <div>{order.total}</div>
            </div>
          );
        })}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {};
}

export default connect(mapStateToProps)(Orders);
