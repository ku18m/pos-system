import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvoicesMainComponent } from './invoices-main.component';
import { InvoicesComponent } from './add-invoice/invoices.component';
import { ShowinvoicesComponent } from './showinvoices/showinvoices.component';
import { InvoiceCrudsComponent } from './invoice-cruds/invoice-cruds.component';

const routes: Routes = [{
  path: '',
  component: InvoicesMainComponent,
  children: [
    {path: '', component: InvoicesComponent},
    {path: 'operations', component: ShowinvoicesComponent},
    {path: 'operations/:id', component: InvoiceCrudsComponent}
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InvoicesRoutingModule { }
