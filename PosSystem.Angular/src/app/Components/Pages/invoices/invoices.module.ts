import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InvoicesRoutingModule } from './invoices-routing.module';
import { InvoicesMainComponent } from './invoices-main.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    InvoicesRoutingModule,
    InvoicesMainComponent
  ]
})
export class InvoicesModule { }
