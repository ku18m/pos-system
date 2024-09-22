import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersWithAPIService {

  private apiUrl = 'https://localhost:7168/api/Users'; // Update with your API URL

  constructor(private http: HttpClient) { }

  // Method to fetch all users
  fetchAllUsers(token: string): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`, // Set the Authorization header with the token
      'Content-Type': 'application/json'
    });

    return this.http.get(this.apiUrl, { headers }); // Perform the GET request
  }
}
