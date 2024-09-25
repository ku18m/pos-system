import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientsRoutingModule } from './clients-routing.module';
import { ClientsmainComponent } from './clientsmain.component';


@NgModule({
  imports: [
    CommonModule,
    ClientsRoutingModule,
    ClientsmainComponent
  ]
})
export class ClientsModule { }
