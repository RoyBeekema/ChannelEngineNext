import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hallo, Channel Engine</h1>
        <p>In het kort even een uitleg</p>
        <p>Deze applicatie kent 3 pagina's</p>
        <ul>
          <li><strong>Home</strong>. Deze pagina.</li>
          <li><strong>Orders</strong>. Om bestellingen te bekijken.</li>
          <li><strong>Products</strong>. Voor het aanpassen van de vooraad.</li>
        </ul>
       </div>
    );
  }
}
