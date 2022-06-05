import { DOCUMENT } from '@angular/common';
import { ChangeDetectionStrategy } from '@angular/compiler';
import { ChangeDetectorRef, Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  public userInfo: any = undefined;
  public isMenuVisible: boolean = false;

  constructor(private _authService: AuthService,
    private _cdr: ChangeDetectorRef,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._getUserInfo();
  }

  ngOnDestroy(): void {
      
  }

  public login(): void {
    console.log('login');
    this._authService.implicit();
    this._getUserInfo();
  }

  public logout(): void {
    this._authService.logout();
  }

  private _getUserInfo(): void {
    this.userInfo = this._authService.currentUserInfo();
    console.log(this.userInfo);
    this._reloadCurrentPage();
  }
  
  private _reloadCurrentPage(): void{
    this._router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
  }

}
