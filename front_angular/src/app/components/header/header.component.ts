import { Component } from '@angular/core';
import { UserService } from '../../auth/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(private router: Router, public authService: UserService) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
