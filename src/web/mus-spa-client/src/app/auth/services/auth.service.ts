import { Inject, Injectable, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthCodeFlowConfig } from '../authCodeFlowConfig';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { DOCUMENT } from '@angular/common';
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  constructor(private _authService: OAuthService) {
  }

  ngOnInit(): void {
    this._authService.configure(AuthCodeFlowConfig);
    this._authService.tokenValidationHandler = new JwksValidationHandler();
    this._authService.loadDiscoveryDocumentAndTryLogin();
  }

  public login() {
    this._authService.initImplicitFlow();
  }

  public implicit() {
    this._authService.initLoginFlow();
  }

  public currentUserInfo() {
    return this._authService.getIdentityClaims();
  }

  public logout() {
    this._authService.loadUserProfile()
    return this._authService.logOut();
  }

}
