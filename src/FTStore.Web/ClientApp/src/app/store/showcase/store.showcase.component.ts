import { Component, OnInit } from "@angular/core";
import { ProdutoService } from "src/app/services/product/product.service";
import { Product } from "src/app/model/product";
import { Router } from "@angular/router";

@Component({
  selector: "app-store",
  templateUrl: "./store.showcase.component.html",
  styleUrls: ["./store.showcase.component.css"]
})
export class LojaVitrineComponent implements OnInit {
  public _products: Product[];

  constructor(
    private _produtoService: ProdutoService,
    private _router: Router
  ) {
    this._produtoService.listAll().subscribe(
      products => {
        this._products = products;
      },
      err => {
        console.log(err.error());
      }
    );
  }

  ngOnInit(): void { }

  public openProduct(product: Product) {
    sessionStorage.setItem("productDetail", JSON.stringify(product));
    this._router.navigate(["/store-product-details"]);
  }
}
