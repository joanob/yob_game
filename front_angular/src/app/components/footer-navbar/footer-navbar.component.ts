import { Component } from '@angular/core';
import { UserService } from 'src/app/auth/user.service';

@Component({
  selector: 'app-footer-navbar',
  templateUrl: './footer-navbar.component.html',
  styleUrls: ['./footer-navbar.component.scss'],
})
export class FooterNavbarComponent {
  constructor(public authService: UserService) {}
}
