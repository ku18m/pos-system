import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { UsersWithAPIService } from './users-with-api.service';
import { map, catchError, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthWithAPIService {

  private apiUrl = 'https://localhost:7168/api/Auth/login';

  parentEndpoint = 'Auth';

  constructor(
    private http: HttpClient,
    private router: Router,
    private requestHandler: RequestHandlerService,
    private userService: UsersWithAPIService
  ) {}

  login(username: string, password: string): Observable<any> {
    const body = {
      username,
      password
    };
    return this.requestHandler.post<any>(`${this.parentEndpoint}/login`, body);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  isAuthinticated(): Observable<boolean> {
    return this.userService.getCurrentUser().pipe(
      map((user) => !!user),
      catchError((error) => {
        return of(false);
      })
    );
  }

  isAdmin(): Observable<boolean> {
    return this.userService.getCurrentUser().pipe(
      map((user) => user.role === 'Admin'),
      catchError((error) => {
        return of(false);
      })
    );
  }
}
