import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TypesRoutingModule } from './types-routing.module';
import { TypesMainComponent } from './types-main.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TypesRoutingModule,
    TypesMainComponent
  ]
})
export class TypesModule { }
