import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent {
  username: string;
  password: string;

  constructor(private router: Router, private authService: AuthService) {
    this.username = '';
    this.password = '';
  }

  signup() {
    this.authService
      .signup({
        username: this.username,
        password: this.password,
      })
      .subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: () => {
          alert('Error');
        },
      });
  }
}
