import React, { Component } from 'react';

export class FetchProducts extends Component {
    static displayName = FetchProducts.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    componentDidMount() {
        this.populateProductsData();
    }

    static renderProductsTable(products, that) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Beschrijving</th>
                        <th>Product nummer</th>
                        <th>Voorraad</th>
                        <th>Voorraad aanpassen</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.merchantProductNo}>
                            <td>{product.name}</td>
                            <td>{product.merchantProductNo}</td>
                            <td>{product.stock}</td>
                            <td>
                                <button onClick={(e) => that.setStock(product.merchantProductNo, 0)}>0</button>
                                <button onClick={(e) => that.setStock(product.merchantProductNo, product.stock-1)}>-</button>
                                <button onClick={(e) => that.setStock(product.merchantProductNo, product.stock+1)}>+</button>
                                <button onClick={(e) => that.setStock(product.merchantProductNo, 25)}>25</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    setStock(merchantProductNo, stock) {
        this.updateProductsData(merchantProductNo,stock)
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchProducts.renderProductsTable(this.state.products, this);

        return (
            <div>
                <h1 id="tabelLabel" >Product data</h1>
                <p>Vooraad aanpasbaar via de lelijke knoppen</p>
                {contents}
            </div>
        );
    }

    async populateProductsData() {
        const response = await fetch('products');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }

    async updateProductsData(merchantProductNo, stock) {
        const response = await fetch('products/' + merchantProductNo + '/Stock/' + stock, { method: 'PATCH' });
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }
}
