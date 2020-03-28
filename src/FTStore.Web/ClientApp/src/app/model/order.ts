import { User } from "./user";
import { OrderItem } from "./orderItem";

export class Order {
  user: User;
  totalOrder: number;
  items: OrderItem[];
}
