import {
  ChangeDetectorRef,
  Component,
  OnChanges,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';
import { from, Observable, Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  public userName: string = '';
  public isMenuVisible: boolean = false;
  public isLoggedIn: boolean = false;

  private _destroy$: Subject<void> = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    private _router: Router,
    private _authService: AuthService
  ) {}

  ngOnDestroy(): void {
    this._destroy$.next();
    this._destroy$.complete();
  }

  ngOnInit() {
    this._authService.isLoggesIn
      .pipe(takeUntil(this._destroy$))
      .subscribe((x) => {
        this.isLoggedIn = x;
        if (x) {
          this._authService.UserName
            .pipe(takeUntil(this._destroy$))
            .subscribe((y) => {
              this.userName = y;
            });
        }
      });
  }

  public login() {
    this.isMenuVisible = false;
  }

  public logout() {
    this.isMenuVisible = false;
  }

  public goHome(): void {
    this._router.navigate(['landing']);
    this.isMenuVisible = false;
  }

  public goProfile(): void {
    this._router.navigate(['user/profile']);
    this.isMenuVisible = false;
  }
}
