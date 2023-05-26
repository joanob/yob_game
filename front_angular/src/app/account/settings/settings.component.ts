import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/auth/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent {
  companyName: string;

  constructor(private router: Router, public userService: UserService) {
    this.companyName = userService.user.companyName;
  }

  updateCompanyName() {
    if (this.userService.user.companyName === '') {
      this.userService.updateCompanyName(this.companyName).subscribe(() => {
        // Redirect to home after setting company name for the first time
        this.router.navigate(['/home']);
      });
    } else {
      this.userService.updateCompanyName(this.companyName);
    }
  }
}
