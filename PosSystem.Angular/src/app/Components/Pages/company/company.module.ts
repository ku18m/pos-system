import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing.module';
import { CompanyMainComponent } from './company-main.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CompanyRoutingModule,
    CompanyMainComponent
  ]
})
export class CompanyModule { }
