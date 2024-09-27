import { Routes } from '@angular/router';
import { HomeComponent } from './Components/Pages/home/home.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';
import { LoginComponent } from './Components/Pages/login/login.component';
import { ClientsmainComponent } from './Components/Pages/clients/clientsmain.component';
import { CompanyMainComponent } from './Components/Pages/company/company-main.component';
import { UsersMainComponent } from './Components/Pages/users/users-main.component';
import { ItemsMainComponent } from './Components/Pages/items/items-main.component';
import { TypesMainComponent } from './Components/Pages/types/types-main.component';
import { UnitsMainComponent } from './Components/Pages/unit/units-main.component';
import { InvoicesMainComponent } from './Components/Pages/invoices/invoices-main.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AdminGuardService } from './services/admin-guard.service';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'company',
    component: CompanyMainComponent,
    canActivate: [AuthGuardService],
    loadChildren: () =>
      import('./Components/Pages/company/company.module').then(
        (m) => m.CompanyModule
      ),
  },
  {
    path: 'types',
    canActivate: [AuthGuardService],
    component: TypesMainComponent,
    loadChildren: () =>
      import('./Components/Pages/types/types.module').then(
        (m) => m.TypesModule
      ),
  },
  {
    path: 'units',
    canActivate: [AuthGuardService],
    component: UnitsMainComponent,
    loadChildren: () =>
      import('./Components/Pages/unit/units.module').then(
        (m) => m.UnitsModule
      ),
  },
  {
    path: 'items',
    canActivate: [AuthGuardService],
    component: ItemsMainComponent,
    loadChildren: () =>
      import('./Components/Pages/items/items.module').then(
        (m) => m.ItemsModule
      ),
  },
  {
    path: 'clients',
    canActivate: [AuthGuardService],
    component: ClientsmainComponent,
    loadChildren: () =>
      import('./Components/Pages/clients/clients.module').then(
        (m) => m.ClientsModule
      ),
  },
  {
    path: 'invoices',
    canActivate: [AuthGuardService],
    component: InvoicesMainComponent,
    loadChildren: () =>
      import('./Components/Pages/invoices/invoices.module').then(
        (m) => m.InvoicesModule
      ),
  },
  { path: 'report', canActivate: [AuthGuardService], component: ReportComponent },
  { path: 'stock', canActivate: [AuthGuardService], component: StockComponent },
  {
    path: 'users',
    canActivate: [AdminGuardService],
    component: UsersMainComponent,
    loadChildren: () =>
      import('./Components/Pages/users/users.module').then(
        (m) => m.UsersModule
      ),
  }
];
