import { Component } from '@angular/core';
import { UserService } from '../../auth/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  username: string;
  password: string;

  constructor(private router: Router, public authService: UserService) {
    this.username = '';
    this.password = '';
  }

  login() {
    this.authService
      .login({
        username: this.username,
        password: this.password,
      })
      .subscribe({
        next: () => {
          this.router.navigate(['/home']);
        },
        error: () => {
          alert('Error');
        },
      });
  }
}
