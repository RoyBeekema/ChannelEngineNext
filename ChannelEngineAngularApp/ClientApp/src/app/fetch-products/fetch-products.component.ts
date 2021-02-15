import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-products',
  templateUrl: './fetch-products.component.html'
})
export class FetchProductsComponent {
  public products: ProductModel[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ProductModel[]>(baseUrl + 'products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }
  
  public setStock(merchantProductNo, stock) {
    this.updateProductsData(merchantProductNo, 'Stock', stock);
  }

  public async updateProductsData(merchantProductNo, property, stock) {

    const response = await fetch('products/' + merchantProductNo + '/' + property + '/' + stock, { method: 'PATCH' });
    const data = await response.json();
    this.products = data;
  }
}

interface ProductModel {
  name: string;
  description: string;
  merchantProductNo: string;
  stock: number;
}
