import { Routes } from '@angular/router';
import { HomeComponent } from './Components/Pages/home/home.component';
import { UnitComponent } from './Components/Pages/unit/unit.component';
import { InvoicesComponent } from './Components/Pages/invoices/invoices.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';
import { LoginComponent } from './Components/Pages/login/login.component';
import { ClientsmainComponent } from './Components/Pages/clients/clientsmain.component';
import { CompanyMainComponent } from './Components/Pages/company/company-main.component';
import { UsersMainComponent } from './Components/Pages/users/users-main.component';
import { ItemsMainComponent } from './Components/Pages/items/items-main.component';
import { TypesMainComponent } from './Components/Pages/types/types-main.component';

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
  {
    path: 'types',
    component: TypesMainComponent,
    loadChildren: () =>
      import('./Components/Pages/types/types.module').then(
        (m) => m.TypesModule
      ),
  },
  { path: 'units', component: UnitComponent },
  {
    path: 'items',
    component: ItemsMainComponent,
    loadChildren: () =>
      import('./Components/Pages/items/items.module').then(
        (m) => m.ItemsModule
      ),
  },
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
  {
    path: 'users',
    component: UsersMainComponent,
    loadChildren: () =>
      import('./Components/Pages/users/users.module').then(
        (m) => m.UsersModule
      ),
  }
];
