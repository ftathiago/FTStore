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

  public save(product: Product, picture: File): Observable<Product> {
    const formData: FormData = new FormData();

    this.AddPicture(formData, picture);

    formData.append("produto", JSON.stringify(product));

    var productAlreadyRegistered = product.id > 0;

    if (productAlreadyRegistered)
      return this.sendProductToEdit(formData);
    return this.sendProductToRegister(formData);
  }

  private AddPicture(formData: FormData, arquivo: File): FormData {
    if (!arquivo)
      return formData;
    formData.append("imagem-produto", arquivo, arquivo.name);
    return formData;
  }

  private sendProductToEdit(formData: FormData): Observable<Product> {
    return this.http.put<Product>(this._baseUrl + "api/produto", formData);
  }

  private sendProductToRegister(formData: FormData): Observable<Product> {
    return this.http.post<Product>(this._baseUrl + "api/produto/", formData);
  }
}
