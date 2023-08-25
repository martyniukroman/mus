import { HttpClient, HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { musConfig } from '../../configs/mus-config';
import { AuthService } from 'src/app/auth/services/auth.service';

export class responceModel {
  public weatherData: string = '';
}

@Component({
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LandingComponent {

  public wAuth: any;
  public wNoAuth: any;

  public isRegister: boolean = false;
  public isLogin: boolean = false;

  public rEmail: any = '';
  public rDisplayName: any = '';
  public rPassword: any = '';
  
  public lEmail: string = '';
  public lPassword: string = '';

  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _cdr: ChangeDetectorRef,
    private readonly _authService: AuthService) {

  }

  public getData(): void {

      this._httpClient.get(musConfig.hostUrl + 'api/WeatherForecast/GetAuth').subscribe(x => {
        this.wAuth = x;
        this._cdr.detectChanges();
      });
      this._httpClient.get(musConfig.hostUrl + 'api/WeatherForecast/GetNoAuth').subscribe(x => {
        this.wNoAuth = x;
        this._cdr.detectChanges();
      });

  }

  public register(): void {

    if(this.isRegister == false) {
      this.isRegister = true;
      return;
    }

    this._authService.register({Email: this.rEmail, DisplayName: this.rDisplayName, Password: this.rPassword}).subscribe(x => {
      console.log(x);
      if(x) {
        this._authService.login(this.rEmail, this.rPassword).subscribe(x => {
          console.log(x);
        });
      }
    });
  }

  public login(): void {
    if(this.isLogin == false) {
      this.isLogin = true;
      return;
    }

    this._authService.login(this.lEmail, this.lPassword).subscribe(x => {
      console.log(x);
    });
  }

  public logout() : void {
    this._authService.logout();
  }


}
