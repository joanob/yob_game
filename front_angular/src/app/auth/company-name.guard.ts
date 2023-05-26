import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
// Company name must be set to play
export class CompanyNameGuard {
  constructor(private authService: UserService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (
      this.authService.isLoggedIn() &&
      this.authService.user.companyName === ''
    ) {
      this.router.navigate(['/account/settings']);
      return false;
    }
    return true;
  }
}
