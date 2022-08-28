import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { AuthCodeFlowConfig } from './auth/authCodeFlowConfig';
import { AuthService } from './auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'mus-spa-client';

  constructor(private _authService: OAuthService) {
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
    this._authService.initImplicitFlow();
  }
  
}
