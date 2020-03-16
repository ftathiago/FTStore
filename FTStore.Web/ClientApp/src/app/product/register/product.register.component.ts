import { Component, OnInit } from "@angular/core"
import { Product } from "../../model/product"
import { ProdutoService } from "../../services/product/product.service";
import { Router } from "@angular/router";

@Component({
  selector: "product-register",
  templateUrl: "./product.register.component.html",
  styleUrls: ["./product.register.component.css"]
})

export class ProductComponent implements OnInit {
  public product: Product;
  public spinnerActive: boolean;
  public message: string;
  public registeredProduct: boolean;
  private selectedFile: File;

  constructor(
    private _productService: ProdutoService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    let sessionProduct = sessionStorage.getItem("productSession");

    if (sessionProduct) {
      this.product = JSON.parse(sessionProduct);
      return;
    }
    this.product = new Product();
  }

  public inputChange(files: FileList) {
    this.selectedFile = files.item(0);
  }

  public save() {
    this.waitingActivate();
    this.registeredProduct = false;
    this._productService.save(this.product)
      .subscribe(
        produtoJson => {
          if (!this.selectedFile) {
            this.successProductHandle();
            return;
          }
          this._productService.addPicture(produtoJson, this.selectedFile)
            .subscribe(
              fileAdded => {
                this.successProductHandle();
              },
              error => {
                this.errorProductHandle(error);
              }
            );
        },
        err => {
          this.errorProductHandle(err);
        }
      );
  }

  private successProductHandle() {
    this.registeredProduct = true;
    this.waitingDeactivate();
    this.message = "";
    this._router.navigate(['product-search']);
  }

  private errorProductHandle(err: any) {
    this.message = err.error;
    this.registeredProduct = false;
    this.waitingDeactivate();
  }

  public waitingActivate() {
    this.spinnerActive = true;
  }

  public waitingDeactivate() {
    this.spinnerActive = false;
  }
}
