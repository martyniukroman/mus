import { ChangeDetectionStrategy } from '@angular/compiler';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
      
  }

  login() {
    this._authService.login();
  }

}
