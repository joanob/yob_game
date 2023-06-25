import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../types/User';
import { environment } from 'src/environment/environment';
import { CookieService } from 'ngx-cookie-service';
import { map } from 'rxjs';

export interface AuthAction {
  username: string;
  password: string;
}

const nullUser: User = {
  id: 0,
  username: '',
  companyName: '',
  companyMoney: 0,
};

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private isCheckingCookies: boolean;
  public user: User;

  constructor(private http: HttpClient, private cookieService: CookieService) {
    const sessionStartedCookie = this.cookieService.get('X-Session-Started');

    if (sessionStartedCookie === 'true') {
      this.isCheckingCookies = true;
      this.http
        .get<User>(`${environment.apiBase}/user/session`, {
          withCredentials: true,
        })
        .subscribe({
          next: (res) => {
            this.user = res;
            this.isCheckingCookies = false;
          },
          error: () => {
            this.isCheckingCookies = false;
          },
        });
    } else {
      this.isCheckingCookies = false;
    }
    this.user = nullUser;
  }

  isLoggedIn() {
    if (this.isCheckingCookies) {
      return true;
    }
    return this.user.id !== 0;
  }

  signup(authAction: AuthAction) {
    return this.http.post(`${environment.apiBase}/user/signup`, authAction);
  }

  login(authAction: AuthAction) {
    return this.http
      .post<User>(`${environment.apiBase}/user/login`, authAction, {
        withCredentials: true,
      })
      .pipe(
        map((user) => {
          this.user = user;
        })
      );
  }

  logout() {
    this.cookieService.delete('X-Session-Started');
    this.user = nullUser;
  }

  updateCompanyName(companyName: string) {
    return this.http
      .put(
        `${environment.apiBase}/user/company/name`,
        { companyName },
        { withCredentials: true }
      )
      .pipe(
        map(() => {
          this.user.companyName = companyName;
        })
      );
  }
}
