import { Component, OnInit } from "@angular/core";
import { Cart } from "src/app/store/cart/store.cart";
import { Product } from "src/app/model/product";
import { OrderItem } from "src/app/model/orderItem";

@Component({
  selector: "app-show-cart",
  templateUrl: "./store.show-cart.component.html",
  styleUrls: ["./store.show-cart.component.css"]
})
export class LojaEfetivarComponent implements OnInit {
  public _cart: Cart;
  public _orderItems: OrderItem[];
  public _total: number;
  constructor() { }

  ngOnInit() {
    this._cart = new Cart();
    this._orderItems = this._cart.getOrderItems();
    this.updateTotal();
  }

  public updateOrderItemPrice(orderItem: OrderItem) {
    if (!orderItem.total) orderItem.total = orderItem.product.price;
    if (orderItem.quantity <= 0) orderItem.quantity = 1;
    orderItem.total = orderItem.product.price * orderItem.quantity;
    this._cart.update(this._orderItems);
    this.updateTotal();
  }

  public remove(vendaItem: OrderItem) {
    this._cart.removeOrderItem(vendaItem);
    this._orderItems = this._cart.getOrderItems();
    this.updateTotal();
  }

  public updateTotal() {
    this._total = this._cart.totalOrder();
  }
}
