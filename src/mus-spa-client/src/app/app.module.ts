import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Router, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { KeycloakService } from 'keycloak-angular';

import { AppComponent } from './app.component';
import { UserModule } from './modules/user/user.module';
import { LandingComponent } from './shared/components/landing/landing.component';
import { TokenInterceptor } from './shared/Interceptors/token.interceptor';
import { SharedModule } from './shared/shared.module';

const importModules: any[] = [
  UserModule,
  SharedModule,
]

function initializeKeycloak(keycloak: KeycloakService) {
  return () =>
    keycloak.init({
      config: {
        url: 'http://localhost:8080',
        realm: 'mus',
        clientId: 'mus-app'
      },
      initOptions: {
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri:
          window.location.origin + '/assets/silent-check-sso.html'
      }
    });
}


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    RouterModule.forRoot(
      [
        { path: '', component: LandingComponent },
        { path: 'mus', component: LandingComponent },
        { path: 'user', loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule) }
      ]
      ),
      ...importModules
    ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      multi: true,
      deps: [KeycloakService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
