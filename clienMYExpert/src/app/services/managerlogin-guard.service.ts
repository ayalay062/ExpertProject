import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { Expert } from '../classes/expert';

@Injectable({
  providedIn: 'root'
})
export class ManagerloginGuardService {
  public constructor(private router: Router) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      let user = localStorage.getItem("user");
      if (user === null) {
          this.router.navigate(["/about"]);
          return false;
      }
      if (JSON.parse(user).userType!=3)
          {
            this.router.navigate(["/about"]);
            return false;
          }
      return true;

}
}
