import { Component, OnInit } from "@angular/core";
import { User } from "src/app/model/user";
import { Router, ActivatedRoute } from "@angular/router";
import { UserService } from "src/app/services/user/user.service";


@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: [
    "./login.component.css"
  ]
})
export class LoginComponent implements OnInit {
  public _user;
  public _message;
  public _spinnerActive: boolean;
  private _returnUrl: string;


  constructor(
    private _router: Router,
    private _activatedRouter: ActivatedRoute,
    private _userService: UserService

  ) {
  }

  ngOnInit(): void {
    this._user = new User();
    this._returnUrl = this._activatedRouter.snapshot.queryParams['returnUrl'];
  }

  entrar() {
    this._spinnerActive = true;
    this._userService.authenticate(this._user)
      .subscribe(
        usuario_json => {
          this._userService.user = usuario_json;

          let returnURL = this._returnUrl;
          if (returnURL == null)
            returnURL = "/";
          this._router.navigate([returnURL]);
        },
        err => {
          this._message = err.error;
          this._spinnerActive = false;
        }
      );
  }
}

