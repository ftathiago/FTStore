import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Observable } from "rxjs";
import { User } from "src/app/model/user";

@Injectable({
  providedIn: "root"
})
export class UserService {
  private _baseURL: string;
  private _user: User;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this._baseURL = baseUrl;
  }

  set user(user: User) {
    this._user = user;
    sessionStorage.setItem("authenticated-user", JSON.stringify(user));
  }

  get user(): User {
    let authenticatedUser = sessionStorage.getItem("authenticated-user");
    if (!authenticatedUser)
      return;
    this._user = JSON.parse(authenticatedUser);
    return this._user;
  }

  get headers(): HttpHeaders {
    return new HttpHeaders()
      .set('content-type', 'application/json');
  }

  public isAuthenticated(): boolean {
    return this.user != null && this._user.email != "" && this._user.password != "";
  }

  public isAdmin(): boolean {
    if (!this.user)
      return;
    return this.isAuthenticated() && this.user.isAdmin;
  }

  public clearSession() {
    sessionStorage.setItem("authenticated-user", "");
    this._user = null;
  }

  public authenticate(user: User): Observable<User> {
    var body = {
      email: user.email,
      password: user.password
    };
    return this.http.post<User>(this._baseURL + "api/usuario/verificarusuario", body, { headers: this.headers });
  }

  public register(user: User): Observable<User> {
    var body = JSON.stringify(user);
    return this.http.post<User>(this._baseURL + "api/usuario", body, { headers: this.headers })
  }
}
