import { Injectable, Inject, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Observable } from "rxjs";
import { Product } from "src/app/model/product";

@Injectable({
  providedIn: "root"
})
export class ProdutoService implements OnInit {
  public products: Product[];
  private _baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.products = [];
  }

  get headers(): HttpHeaders {
    return new HttpHeaders()
      .set('content-type', 'application/json');
  }

  public delete(product: Product): Observable<Product[]> {
    return this.http.delete<Product[]>(this._baseUrl + "api/produto/" + product.id, { headers: this.headers });
  }

  public listAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this._baseUrl + "api/produto");
  }

  public getById(productId: number): Observable<Product> {
    return this.http.get<Product>(this._baseUrl + "api/produto/" + productId);
  }

  public save(product: Product): Observable<Product> {
    return this.registerProduct(product);
  }

  private registerProduct(product: Product): Observable<Product> {
    let productAlreadyRegistered = product.id > 0;
    if (productAlreadyRegistered)
      return this.sendProductToEdit(product);
    return this.sendProductToRegister(product);
  }

  public addPicture(product: Product, arquivo: File): Observable<boolean> {
    let formData = new FormData();
    formData.append("files", arquivo, arquivo.name);
    return this.http.put<boolean>(this._baseUrl + "api/produto/" + product.id, formData);
  }

  private sendProductToEdit(product: Product): Observable<Product> {
    let body = JSON.stringify(product)
    return this.http.put<Product>(this._baseUrl + "api/produto", body, { headers: this.headers });
  }

  private sendProductToRegister(product: Product): Observable<Product> {
    let body = JSON.stringify(product);
    return this.http.post<Product>(this._baseUrl + "api/produto/", body, { headers: this.headers });
  }
}
