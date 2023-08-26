import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LandingComponent } from './components/landing/landing.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { Ban, ChevronRightCircle, LucideAngularModule,} from 'lucide-angular';

const components = [
  HeaderComponent,
  LandingComponent,
  LoginComponent,
]

const icons = [ LucideAngularModule.pick({Ban, ChevronRightCircle}) ];

@NgModule({
  declarations: [
    ...components,
  ],
  imports: [
    CommonModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    ...icons
  ],
  exports: [
    ...components
  ]
})
export class SharedModule { }
