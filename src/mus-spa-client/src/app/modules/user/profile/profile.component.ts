import { Component, OnInit } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakProfile } from 'keycloak-js';

@Component({
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public isLoggedIn: boolean = false;
  public userProfile: KeycloakProfile | undefined = undefined;

  constructor(private _keycloakService: KeycloakService) { }

  async ngOnInit() {
    this.isLoggedIn = await this._keycloakService.isLoggedIn();
    type userRoles = Array<{id: number, text: string}>;
    if(this.isLoggedIn) {
      this.userProfile = await this._keycloakService.loadUserProfile();
    }
  }

  public edit() : void {
    
  }

}
