import { FooterComponent } from '../footer/footer.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { Phone2Component } from '../phone2/phone2.component';
import { Phone3Component } from '../phone3/phone3.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { Phone1Component } from './../phone1/phone1.component';
import { Component } from '@angular/core';

;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [Phone1Component,Phone2Component,Phone3Component,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
