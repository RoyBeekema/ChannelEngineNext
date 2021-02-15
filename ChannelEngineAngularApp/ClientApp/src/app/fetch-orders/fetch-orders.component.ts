import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-orders',
  templateUrl: './fetch-orders.component.html'
})
export class FetchOrdersComponent {
  public orders: OrderModel[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<OrderModel[]>(baseUrl + 'orders').subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }
}

interface OrderModel {
  id: number;
  description: string;
  gtin: string;
  quantity: number;
}
