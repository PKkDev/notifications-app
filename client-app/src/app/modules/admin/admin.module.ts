import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// routing
import { AdminRoutingModule } from './admin-routing.module';
// components
import { AdminComponent } from './admin.component';


@NgModule({
  declarations: [
    AdminComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
