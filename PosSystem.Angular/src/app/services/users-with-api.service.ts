import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { IUser } from '../Components/Pages/users/IUser';
import { IUserOperations } from '../Components/Pages/users/IUserOperations';

@Injectable({
  providedIn: 'root'
})
export class UsersWithAPIService {

  private apiUrl = 'https://localhost:7168/api/Users'; // Update with your API URL

  parentEndpoint = 'Users';

  constructor(
    private http: HttpClient,
    private requestHandler: RequestHandlerService
  ) { }

  // Method to fetch all users
  fetchAllUsers(token: string): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`, // Set the Authorization header with the token
      'Content-Type': 'application/json'
    });

    return this.http.get(this.apiUrl, { headers }); // Perform the GET request
  }

  getAllUsers(): Observable<any> {
    return this.requestHandler.get<IUser[]>(this.parentEndpoint);
  }

  getUserById(id: string): Observable<any> {
    return this.requestHandler.get<IUser>(`${this.parentEndpoint}/${id}`);
  }

  addNewUser(user: IUserOperations): Observable<any> {
    return this.requestHandler.post<IUser>(this.parentEndpoint, user);
  }

  updateUser(user: IUserOperations): Observable<any> {
    if (user.password === '') {
      delete user.password;
    }
    console.log(user);
    return this.requestHandler.put<IUser>(`${this.parentEndpoint}/${user.id}`, user);
  }

  deleteUser(id: string): Observable<any> {
    return this.requestHandler.delete(`${this.parentEndpoint}/${id}`);
  }

  getCurrentUser(): Observable<any> {
    return this.requestHandler.get<IUser>(`${this.parentEndpoint}/current`);
  }
}
