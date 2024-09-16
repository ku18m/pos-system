import { Routes } from '@angular/router';
import { HomeComponent } from './Components/Pages/home/home.component';
import { CompanyComponent } from './Components/Pages/company/company.component';
import { TypesComponent } from './Components/Pages/types/types.component';
import { UnitComponent } from './Components/Pages/unit/unit.component';
import { ItemsComponent } from './Components/Pages/items/items.component';
import { ClientsComponent } from './Components/Pages/clients/clients.component';
import { InvoicesComponent } from './Components/Pages/invoices/invoices.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';
import { LoginComponent } from './Components/Pages/login/login.component';

export const routes: Routes = [
    {path:'',component:LoginComponent},
    {path:'login',component:LoginComponent},
    {path:'home',component:HomeComponent},
    {path:'company',component:CompanyComponent},
    {path:'types',component:TypesComponent},
    {path:'units',component:UnitComponent},
    {path:'items',component:ItemsComponent},
    {path:'clients',component:ClientsComponent},
    {path:'invoices',component:InvoicesComponent},
    {path:'report',component:ReportComponent},
    {path:'stock',component:StockComponent}
];
