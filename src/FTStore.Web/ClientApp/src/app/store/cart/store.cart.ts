import { Product } from "src/app/model/product";
import { OrderItem } from "src/app/model/orderItem";
import { Order } from "src/app/model/order";

export class Cart {
  public _order: Order;

  constructor() {
    this._order = new Order();
    this._order.items = [];
  }
  public totalOrder(): number {
    return this._order.totalOrder;
  }

  public add(product: Product) {
    this.loadOrderFromLocalStorage();
    let orderItems = this._order.items;
    let productAlreadyAdded = orderItems.find(v => v.product.id == product.id)
    if (productAlreadyAdded) {
      this.addOneMore(product, orderItems);
    } else {
      this.addNewProduct(product, orderItems);
    }
    this.update(orderItems);
  }

  private addOneMore(product: Product, orderItems: OrderItem[]) {
    orderItems = orderItems.map(
      (value) => {
        if (value.product.id == product.id) {
          value.quantity++;
          value.total *= value.quantity;
        }
        return value;
      });
  }

  private addNewProduct(product: Product, orderItems: OrderItem[]) {
    let ordemItem = {
      product,
      quantity: 1,
      total: product.price
    };
    orderItems.push(ordemItem);
  }

  public getOrderItems(): OrderItem[] {
    this.loadOrderFromLocalStorage();
    return this._order.items;
  }

  public removeOrderItem(orderItem: OrderItem) {
    this.loadOrderFromLocalStorage();
    let vendaItens = this._order.items;
    vendaItens = vendaItens.filter(p => p.product.id != orderItem.product.id);
    this.update(vendaItens);
  }

  public update(orderItems: OrderItem[]) {
    this._order.items = orderItems;
    let totalOrder = orderItems.reduce((sum, v) => sum += v.total, 0);
    this._order.totalOrder = totalOrder;
    localStorage.setItem("orderLocalStorage", JSON.stringify(this._order));
  }

  private loadOrderFromLocalStorage() {
    let orderLocalStorage = localStorage.getItem("orderLocalStorage");
    if (orderLocalStorage) {
      this._order = JSON.parse(orderLocalStorage);
    }
  }
}
