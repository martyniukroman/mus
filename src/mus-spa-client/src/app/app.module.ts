import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { LandingComponent } from './shared/components/landing/landing.component';
import { SharedModule } from './shared/shared.module';
import { TokenInterceptor } from './shared/Interceptors/token.interceptor';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CateringModule } from './modules/catering/catering.module';
import { LoginComponent } from './shared/components/login/login.component';

const importModules: any[] = [
  CateringModule,
  SharedModule,
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    CommonModule,
    RouterModule.forRoot(
      [
        { path: '', component: LandingComponent },
        { path: 'landing', component: LandingComponent },
        { path: 'login', component: LoginComponent },
        { path: 'catering', loadChildren: () => import('./modules/catering/catering.module').then(m => m.CateringModule) },
        { path: 'customer', loadChildren: () => import('./modules/customer/customer.module').then(m => m.CustomerModule) },
      ]
      ),
      ...importModules,
    ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
