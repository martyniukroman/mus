import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _oauth: OAuthService) { }

  public login() {
    this._oauth.initImplicitFlow();
  }

}
