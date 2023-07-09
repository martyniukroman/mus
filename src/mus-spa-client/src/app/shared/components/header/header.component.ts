import { ChangeDetectorRef, Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakProfile } from 'keycloak-js';
import { from, Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  public userInfo: any = undefined;
  public isMenuVisible: boolean = false;
  public isLoggedIn: boolean = false;
  public userProfile: KeycloakProfile | undefined = undefined;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _router: Router,
    private _keycloakService: KeycloakService
  ) { }

  async ngOnInit() {
    this.isLoggedIn = await this._keycloakService.isLoggedIn();
    type userRoles = Array<{id: number, text: string}>;
    if(this.isLoggedIn) {
      this.userProfile = await this._keycloakService.loadUserProfile();
      console.log(this.userProfile);
    }
  }

  public login() {
    this._keycloakService.login();
    this.isMenuVisible = false;
  }

  public logout() {
    this._keycloakService.logout();
    this.isMenuVisible = false;
  }
  
  public goHome() : void {
    this._router.navigate(['mus']);
    this.isMenuVisible = false;
  }

  public goProfile(): void {
    this._router.navigate(['user/profile']);
    this.isMenuVisible = false;
  }         

}
