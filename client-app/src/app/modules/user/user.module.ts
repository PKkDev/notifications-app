import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// routing
import { UserRoutingModule } from './user-routing.module';
// components
import { UserComponent } from './user.component';


@NgModule({
  declarations: [
    UserComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
