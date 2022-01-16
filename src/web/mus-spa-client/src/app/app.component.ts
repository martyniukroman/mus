import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { MusAuthConfig } from './shared/auth/auth.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'mus-spa-client';

  constructor(private readonly _oAuthService: OAuthService,
    private readonly _httpClient: HttpClient) {
    
  }

  ngOnInit(): void {
    this._oAuthService.configure(MusAuthConfig);
    this._oAuthService.tokenValidationHandler = 
      new JwksValidationHandler();
    this._oAuthService.loadDiscoveryDocumentAndTryLogin();
  }

  
  login(){ this._oAuthService.initImplicitFlow(''); }
  register(){ this._oAuthService.initImplicitFlow(); }
  logout(){ this._oAuthService.logOut(); }

  get givenName() {
    let claims = this._oAuthService.getIdentityClaims();
    if(!claims) return null;
    return (claims as any).preferred_username;
  }

  public forecasts: any;

  getWeather(): void {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
          Authorization: "Bearer " + this._oAuthService.getAccessToken()
        })
      };
      //this._httpClient.get<any>('https://localhost:44304/weatherforecast/auth', httpOptions)
      this._httpClient.get<any>('http://localhost:5000/weatherforecast/auth', httpOptions)
          .subscribe(result => {
              this.forecasts = result;
          }, error => console.error(error));
  }

}
