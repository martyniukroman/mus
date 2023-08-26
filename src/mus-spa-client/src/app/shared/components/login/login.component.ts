import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { featherAirplay } from '@ng-icons/feather-icons';
import { catchError, pipe } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {
  public isError: boolean = false;

  constructor(
    private readonly _authService: AuthService,
    private readonly _cdr: ChangeDetectorRef
  ) {}

  public loginForm = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(6)]),
  });

  public loginSubmit(): void {
    this.loginForm.markAllAsTouched();

    if (this.loginForm.touched && this.loginForm.valid) {
      this._authService
        .login(
          this.loginForm.controls['email'].value as string,
          this.loginForm.controls['password'].value as string
        )
        .pipe(
          catchError((x) => {

            if (x.error && x.error.caption) {
              this.isError = true;
              this._cdr.detectChanges();
            }

            throw x;
          })
        )
        .subscribe((x) => {
          console.log(x);
        });
    }
  }
}
