import { Component } from '@angular/core';
import { AuthWithAPIService } from '../../../services/auth-with-api.service';
import { Router } from '@angular/router';
import { UsersWithAPIService } from '../../../services/users-with-api.service';
import { IUser } from '../users/IUser';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  loggedIn: boolean = false;
  currentUser: IUser | null = null;

  constructor(
    private authService: AuthWithAPIService,
    private router: Router,
    private userService: UsersWithAPIService
  ) {}

  ngOnInit() {
    this.loggedIn = this.authService.isLoggedIn();

    this.userService.getCurrentUser().subscribe({
      next: (user: IUser) => {
        this.currentUser = user;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }


  logoutUser() {
    this.authService.logout();
    this.loggedIn = false;
  }

  switchLogin() {
    this.router.navigate(['/login']);
  }
}
