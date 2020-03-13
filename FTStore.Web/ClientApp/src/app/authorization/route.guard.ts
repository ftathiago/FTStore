import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { UserService } from "../services/user/user.service";

@Injectable({
  providedIn: 'root'
})
export class RouteGuard implements CanActivate {
  constructor(
    private _router: Router,
    private _userService: UserService
  ) {
    this._router = _router;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    var authenticated = this._userService.isAuthenticated();
    if (authenticated)
      return true;
    this._router.navigate(['/signin'], { queryParams: { returnUrl: state.url } });
    return false;
  }

}
