import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ItemsRoutingModule } from './items-routing.module';
import { ItemsMainComponent } from './items-main.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ItemsRoutingModule,
    ItemsMainComponent
  ]
})
export class ItemsModule { }
