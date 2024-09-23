import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthWithAPIService {
  private apiUrl = 'https://localhost:7168/api/Auth/login';

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string): Observable<any> {
    const body = {
      username, // Assuming you have username input
      password, // Assuming you have password input
    };
    return this.http.post(this.apiUrl, body, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    });
  }
}
