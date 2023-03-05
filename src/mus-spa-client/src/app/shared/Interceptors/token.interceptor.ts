import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { BehaviorSubject, finalize, map, Observable, tap } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { responceModel } from '../components/landing/landing.component';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private isTokenRefreshing: boolean = false;
  private errorString: string = '';

  constructor(private _authService: OAuthService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const header = this._authService.authorizationHeader();

    console.log(header);

    if (header) {
      req = req.clone({
        setHeaders: {
          'Authorization': header,
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
      tap((error: any) => {

          console.log('response');
          console.log(error);

          this.errorString = '';

          if (!error.ok) {

            console.log('error->');
            console.log(error);
            this._authService.refreshToken();

            if (error.status) {
              if (error.error) {
                if (error.error.caption) {
                  this.errorString += ' ' + error.error.caption;
                }
                else {
                  this.errorString += ' ' + error.error;
                }
                if (error.error.afterAction == 'relogin') {
                  this._authService.logOut();
                }
              }
              if (error.status == 401 ) {
                this.setTokenResponse();
              }
            }

            if (this.errorString)
              this.RaiseErrorMessage(this.errorString);

          }
        }
      ));
  }

  private RaiseErrorMessage(text: string) {
    alert(text);
  }

  private setTokenResponse(): void {
    if (!this.isTokenRefreshing) {
      this.isTokenRefreshing = true;
      this._authService.refreshToken();
    } 
  }

}
