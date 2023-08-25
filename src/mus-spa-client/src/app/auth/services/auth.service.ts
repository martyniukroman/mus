import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { musConfig } from 'src/app/shared/configs/mus-config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  public loginStatus = new BehaviorSubject<any>(this.checkLoginStatus());
  public UserName = new BehaviorSubject<any>(localStorage.getItem('username'));
  public UserDisplayName = new BehaviorSubject<any>(
    localStorage.getItem('displayName')
  );
  public UserRole = new BehaviorSubject<any>(localStorage.getItem('userRole'));

  // Register Method
  register(data: any): Observable<any> {
    return this.http.post<any>(
      musConfig.hostUrl + 'api/Registration/Register',
      data
    );
  }

  // Method to get new refresh token
  getNewRefreshToken(): Observable<any> {
    let username = localStorage.getItem('username');
    let refreshToken = localStorage.getItem('refreshToken');
    const grantType = 'refresh_token';

    if (!username || !refreshToken) throw 'no creds to refresh token';

    return this.http.post<any>(musConfig.hostUrl + 'api/Auth/login', {
      UserName: username,
      RefreshToken: refreshToken,
      GrantType: grantType,
    });
  }

  //Login Method
  login(username: string, password: string): Observable<any> {
    const grantType = 'password';
    return this.http
      .post<any>(musConfig.hostUrl + 'api/Auth/login', {
        UserName: username,
        Password: password,
        GrantType: grantType,
      })
      .pipe(
        tap((x) => {
          console.log('auth');
          console.log(x);
          if (x && x.authToken.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('loginStatus', '1');
            localStorage.setItem('displayName', x.authToken.displayName);
            localStorage.setItem('userId', x.authToken.userId);
            localStorage.setItem('jwt', x.authToken.token);
            localStorage.setItem('username', x.authToken.username);
            localStorage.setItem('expiration', x.authToken.expiration);
            localStorage.setItem('userRole', x.authToken.roles);
            localStorage.setItem('refreshToken', x.authToken.refresh_token);
            this.UserName.next(localStorage.getItem('username'));
            this.UserDisplayName.next(localStorage.getItem('displayName'));
            this.UserRole.next(localStorage.getItem('userRole'));
            this.loginStatus.next(true);
            this.router.navigateByUrl('/landing');
            console.log('Welcome back ' + localStorage.getItem('displayName'));
          }
        })
      );
  }

  logout() {
    // Set Loginstatus to false and delete saved jwt cookie
    localStorage.removeItem('jwt');
    localStorage.removeItem('userRole');
    localStorage.removeItem('displayName');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('expiration');
    localStorage.removeItem('refreshToken');
    localStorage.setItem('loginStatus', '0');
    console.log('Logged Out Successfully');
    this.router.navigate(['/landing']);
    this.loginStatus.next(false);
  }

  checkLoginStatus(): boolean {
    var loginCookie = localStorage.getItem('loginStatus');

    if (loginCookie == '1') {
      if (
        localStorage.getItem('jwt') != null ||
        localStorage.getItem('jwt') != undefined
      ) {
        return true;
      }
    }
    return false;
  }

  get isLoggesIn() {
    return this.loginStatus.asObservable();
  }

  get currentUserName() {
    return this.UserName.asObservable();
  }

  get currentUserDisplayName() {
    return this.UserDisplayName.asObservable();
  }

  get currentUserRole() {
    return this.UserRole.asObservable();
  }
}
