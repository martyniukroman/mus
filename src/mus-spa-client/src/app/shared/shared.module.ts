import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LandingComponent } from './components/landing/landing.component';
import { FormsModule } from '@angular/forms';

const modules = [
  HeaderComponent,
  LandingComponent
]

@NgModule({
  declarations: [
    ...modules //!To be declared in the modules[]
  ],
  imports: [
    CommonModule,
    NgbModule,
    FormsModule,
  ],
  exports: [
    ...modules
  ]
})
export class SharedModule { }
