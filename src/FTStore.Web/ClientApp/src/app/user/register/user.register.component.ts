import { Component, OnInit } from "@angular/core";
import { User } from "src/app/model/user";
import { UserService } from "src/app/services/user/user.service";

@Component({
  selector: "app-cadastro-usuario",
  templateUrl: "./user.register.component.html",
  styleUrls: ["user.register.component.css"]
})

export class UserRegisterComponent implements OnInit {
  public user: User;
  public registeredUser: boolean;
  public spinnerActive: boolean;
  public message: string;

  constructor(
    private _userService: UserService
  ) { }

  ngOnInit(): void {
    this.user = new User();
  }

  public register() {
    this.spinnerActive = true;
    this.registeredUser = false;
    this._userService.register(this.user)
      .subscribe(
        user => {
          this.spinnerActive = false;
          this.message = "";
          this.registeredUser = true;
        },
        err => {
          this.spinnerActive = false;
          this.message = "Error: " + err.error;
          console.log(err);
        }
      );
  }
}
