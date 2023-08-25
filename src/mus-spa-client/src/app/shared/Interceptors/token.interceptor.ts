import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { BehaviorSubject, catchError, map, Observable, tap } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private isTokenRefreshing: boolean = false;
  private errorString: string = '';

  constructor(private _authService: AuthService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('jwt');

    if (token) {
      req = req.clone({
        setHeaders: {
          'Authorization': 'Bearer ' + token
        }
      });
    }

    if (!req.headers.has('Content-Type')) {
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json'
        }
      });
    }

    req = req.clone({
      headers: req.headers.set('Accept', 'application/json')
    });

    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError(x => {
        if(x && x.ok == false && x.status == 401) {
          this.setTokenResponse();
        }
        throw x;
      }));
  }

  private RaiseErrorMessage(text: string) {
    console.log(text);
  }

  private setTokenResponse(): void {
    if (!this.isTokenRefreshing) {
      this.isTokenRefreshing = true;
      this.tokenSubject.next('');
      this._authService.getNewRefreshToken().subscribe(result => {
        console.log(result);
        if (result && result.authToken.token) {
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('jwt', result.authToken.token);
          localStorage.setItem('username', result.authToken.username);
          localStorage.setItem('expiration', result.authToken.expiration);
          localStorage.setItem('userRole', result.authToken.roles);
          localStorage.setItem('refreshToken', result.authToken.refresh_token);
          localStorage.setItem('displayName', result.authToken.displayName);
          localStorage.setItem('userId', result.authToken.userId);
        }
        location.reload();
      });
    } else {
      this.isTokenRefreshing = false;
    }
  }

}
