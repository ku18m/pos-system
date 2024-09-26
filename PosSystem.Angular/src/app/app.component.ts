import { FooterComponent } from './Components/Pages/footer/footer.component';
import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './Components/Pages/navbar/navbar.component';
import { SidebarComponent } from './Components/Pages/sidebar/sidebar.component';
import { HomeComponent } from './Components/Pages/home/home.component';
import { CompanyComponent } from './Components/Pages/company/add-company/company.component';
import { TypesComponent } from './Components/Pages/types/add-type/types.component';
import { UnitComponent } from './Components/Pages/unit/unit.component';
import { ItemsComponent } from './Components/Pages/items/add-product/items.component';
import { ClientsComponent } from './Components/Pages/clients/add-client/clients.component';
import { InvoicesComponent } from './Components/Pages/invoices/invoices.component';
import { ReportComponent } from './Components/Pages/report/report.component';
import { StockComponent } from './Components/Pages/stock/stock.component';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NgbModule, CommonModule, RouterOutlet,NavbarComponent,FooterComponent,SidebarComponent,HomeComponent,CompanyComponent,TypesComponent,UnitComponent,ItemsComponent,ClientsComponent,InvoicesComponent,ReportComponent,StockComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AngularGP';
  showNavbarAndFooter: boolean = true;

  constructor(private router: Router) {}

  ngOnInit() {
    // Subscribe to router events to track navigation
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.showNavbarAndFooter = !this.isLoginRoute();
      }
    });
  }

  isLoginRoute(): boolean {
    return this.router.url === '/';
  }
}
