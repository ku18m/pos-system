import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRegister } from '../Models/IRegister';

@Injectable({
  providedIn: 'root',
})
export class AuthWithApiService {
  baseUrl: string = 'http://localhost:3007/usersList'; //replace this link with api
  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login`, credentials);
  }

  GetUserbyCode(id: any) {
    return this.http.get(this.baseUrl + '/' + id);
  }

  getAllUsers(): Observable<IRegister[]> {
    return this.http.get<IRegister[]>(this.baseUrl);
  }
  getByEmail(email: string): Observable<IRegister[]> {
    return this.http.get<IRegister[]>(`${this.baseUrl}/users?email=${email}`);
  }
  addUser(data: any): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }
  updateUser(user: any, userId: any) {
    return this.http.put(this.baseUrl + '/' + userId, user);
  }
}
