import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  _isExpanded = false;

  constructor(
    private _router: Router,
    private _userService: UserService
  ) { }

  collapse() {
    this._isExpanded = false;
  }

  toggle() {
    this._isExpanded = !this._isExpanded;
  }

  public isUserAuthenticated(): boolean {
    return this._userService.isAuthenticated();
  }

  public isUserAdmin(): boolean {
    return this._userService.isAdmin();
  }

  public logout() {
    this._userService.clearSession();
    this._router.navigate(['/']);
  }

  get user() {
    return this._userService.user;
  }
}
