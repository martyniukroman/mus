import { Component } from '@angular/core';
import { AuthCodeFlowConfig } from './auth/authCodeFlowConfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'mus-spa-client';

  constructor() {
  }

  ngOnInit(): void {
  }

  public login() {
  }

  public implicit() {
  }
  
}
