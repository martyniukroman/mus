import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

const modules = [
  HeaderComponent
]

@NgModule({
  declarations: [
    ...modules
  ],
  imports: [
    CommonModule,
    NgbModule,
  ],
  exports: [
    ...modules
  ]
})
export class SharedModule { }
