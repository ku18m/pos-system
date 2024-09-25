import { Routes } from '@angular/router';
import { HomeComponent } from './Components/Pages/home/home.component';
import { CompanyComponent } from './Components/Pages/company/add-company/company.component';
import { TypesComponent } from './Components/Pages/types/types.component';
import { UnitComponent } from './Components/Pages/unit/unit.component';
import { ItemsComponent } from './Components/Pages/items/items.component';
import { ClientsComponent } from './Components/Pages/clients/add-client/clients.component';
import { InvoicesComponent } from './Components/Pages/invoices/invoices.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';
import { LoginComponent } from './Components/Pages/login/login.component';
import { ClientsmainComponent } from './Components/Pages/clients/clientsmain.component';
import { CompanyMainComponent } from './Components/Pages/company/company-main.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  {
    path: 'company',
    component: CompanyMainComponent,
    loadChildren: () =>
      import('./Components/Pages/company/company.module').then(
        (m) => m.CompanyModule
      ),
  },
  { path: 'types', component: TypesComponent },
  { path: 'units', component: UnitComponent },
  { path: 'items', component: ItemsComponent },
  {
    path: 'clients',
    component: ClientsmainComponent,
    loadChildren: () =>
      import('./Components/Pages/clients/clients.module').then(
        (m) => m.ClientsModule
      ),
  },
  { path: 'invoices', component: InvoicesComponent },
  { path: 'report', component: ReportComponent },
  { path: 'stock', component: StockComponent },
];
