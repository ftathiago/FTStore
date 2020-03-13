import { Component, OnInit } from "@angular/core";
import { ProdutoService } from "src/app/services/product/product.service";
import { Product } from "src/app/model/product";
import { Router } from "@angular/router";
import { Cart } from "../cart/store.cart";

@Component({
  selector: "app-store-produto",
  templateUrl: "./store.product-details.component.html",
  styleUrls: ["./store.product-details.component.css"]
})
export class LojaProdutoComponent implements OnInit {
  public _product: Product;
  public _cart: Cart;

  constructor(
    private _productService: ProdutoService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._cart = new Cart();
    let produtoJSON = sessionStorage.getItem("productDetail");
    if (produtoJSON) this._product = JSON.parse(produtoJSON);
  }

  public buy() {
    this._cart.add(this._product);
    this._router.navigate(["./store-cart"]);
  }
}
