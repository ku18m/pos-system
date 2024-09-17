import { FooterComponent } from './Components/Pages/footer/footer.component';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './Components/Pages/navbar/navbar.component';
import { SidebarComponent } from './Components/Pages/sidebar/sidebar.component';
import { HomeComponent } from './Components/Pages/home/home.component';
import { CompanyComponent } from './Components/Pages/company/company.component';
import { TypesComponent } from './Components/Pages/types/types.component';
import { UnitComponent } from './Components/Pages/unit/unit.component';
import { ItemsComponent } from './Components/Pages/items/items.component';
import { ClientsComponent } from './Components/Pages/clients/clients.component';
import { InvoicesComponent } from './Components/Pages/invoices/invoices.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NavbarComponent,FooterComponent,SidebarComponent,HomeComponent,CompanyComponent,TypesComponent,UnitComponent,ItemsComponent,ClientsComponent,InvoicesComponent,ReportComponent,StockComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AngularGP';
}
