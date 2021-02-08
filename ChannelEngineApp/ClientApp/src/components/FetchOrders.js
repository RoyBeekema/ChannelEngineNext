import React, { Component } from 'react';

export class FetchOrders extends Component {
  static displayName = FetchOrders.name;

  constructor(props) {
    super(props);
    this.state = { orders: [], loading: true };
  }

  componentDidMount() {
    this.populateOrdersData();
  }

  static renderOrdersTable(orders,that) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Beschrijving</th>
            <th>Product nummer</th>
            <th>Aantal</th>
          </tr>
        </thead>
        <tbody>
        {orders.map(order =>
            <tr key={order.id}>
                <td>{order.description}</td>
                <td>{order.gtin}</td>
                <td>{order.quantity}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

    addStock(id,stock) {
        alert(id)
    }

    render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchOrders.renderOrdersTable(this.state.orders,this);

    return (
      <div>
        <h1 id="tabelLabel" >Order data</h1>
        <p>Top 5, gesorteed op aantal aflopend</p>
        {contents}
      </div>
    );
  }

  async populateOrdersData() {
    const response = await fetch('orders');
    const data = await response.json();
    this.setState({ orders: data, loading: false });
  }
}
