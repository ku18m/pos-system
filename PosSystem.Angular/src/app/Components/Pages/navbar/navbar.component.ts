import { Component } from '@angular/core';
import { AuthWithAPIService } from '../../../services/auth-with-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  loggedIn: boolean = false;

  constructor(private authService: AuthWithAPIService, private router: Router) {}

  ngOnInit() {
    this.loggedIn = this.authService.isLoggedIn();
  }


  logoutUser() {
    this.authService.logout();
    this.loggedIn = false;
  }

  switchLogin() {
    this.router.navigate(['/login']);
  }
}
