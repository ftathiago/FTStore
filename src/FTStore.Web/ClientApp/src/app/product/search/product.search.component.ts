import { Component, OnInit } from "@angular/core";
import { Product } from "src/app/model/product";
import { ProdutoService } from "src/app/services/product/product.service";
import { Router } from "@angular/router";

@Component({
  selector: "product-search",
  templateUrl: "./product.search.component.html",
  styleUrls: ["./product.search.component.css"]
})

export class ProductSearchComponent implements OnInit {
  public _products: Product[];
  public _searchText: string;

  constructor(
    private _productService: ProdutoService,
    private _router: Router
  ) {
    this._productService.listAll()
      .subscribe(
        products => {
          this._products = products;
        },
        errors => {
          console.log(errors.error);
        });
  }

  ngOnInit(): void {

  }

  public addProduct() {
    sessionStorage.setItem("productSession", "");
    this._router.navigate(['product']);
  }

  public delete(product: Product) {
    var confirmed = confirm("Do you want to delete this product?");
    if (!confirmed)
      return;
    this._productService.delete(product).subscribe(
      products => {
        let productIndex = this._products.indexOf(product);
        this._products.splice(productIndex, 1);
      },
      err => {
        console.log(err.error);
      }
    )
  }

  public edit(product: Product) {
    sessionStorage.setItem('productSession', JSON.stringify(product));
    this._router.navigate(['/product']);
  }
}
