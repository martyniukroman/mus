import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Router, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppComponent } from './app.component';
import { UserModule } from './modules/user/user.module';
import { LandingComponent } from './shared/components/landing/landing.component';
import { SharedModule } from './shared/shared.module';

const importModules: any[] = [
  UserModule,
  SharedModule,
]

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    OAuthModule.forRoot(),
    RouterModule.forRoot(
      [
        { path: '', component: LandingComponent },
        { path: 'mus', component: LandingComponent },
        { path: 'user', loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule) }
      ]
      ),
      ...importModules
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
