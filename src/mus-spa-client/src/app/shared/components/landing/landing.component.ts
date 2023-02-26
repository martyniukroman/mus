import { HttpClient, HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { finalize, Observable } from 'rxjs';

export class responceModel {
  public weatherData: string = '';
}

@Component({
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LandingComponent {

  public wAuth$: Observable<responceModel> = new Observable<responceModel>();
  public wNoAuth$: Observable<responceModel> = new Observable<responceModel>();

  constructor(private readonly _httpClient: HttpClient, private readonly _cdr: ChangeDetectorRef) { }

  getData(): void {
    this.wNoAuth$ = this._httpClient.get<responceModel>('https://localhost:44304/api/WeatherForecast/GetNoAuth');
    this.wAuth$ =  this._httpClient.get<responceModel>('https://localhost:44304/api/WeatherForecast/GetAuth');
  }

}
