import { Injectable, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthCodeFlowConfig } from '../authCodeFlowConfig';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  constructor(private _authService: OAuthService) {
      this._authService.configure(AuthCodeFlowConfig);
      this._authService.tokenValidationHandler = 
        new JwksValidationHandler();
      this._authService.loadDiscoveryDocumentAndTryLogin();
  }

  ngOnInit(): void {
    this._authService.configure(AuthCodeFlowConfig);
    this._authService.tokenValidationHandler = new JwksValidationHandler();
    this._authService.loadDiscoveryDocumentAndTryLogin();
  }

  public login() {
    this._authService.initLoginFlow();
  }

  public implicit() {
    this._authService.initLoginFlow();
  }

}
