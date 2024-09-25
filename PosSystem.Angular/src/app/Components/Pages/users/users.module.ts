import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersMainComponent } from './users-main.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    UsersRoutingModule,
    UsersMainComponent
  ]
})
export class UsersModule { }
