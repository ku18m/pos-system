import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthWithAPIService } from './auth-with-api.service';

@Injectable({
  providedIn: 'root'
})
export class RequestHandlerService {
  private apiUrl = 'https://localhost:7168/api';

  constructor(private http: HttpClient) { }

  private getToken(): string | null {
    return localStorage.getItem('token');
  }

  private generateHeaders(): HttpHeaders {
    const token = this.getToken();

    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return headers;
  }

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`, {
      headers: this.generateHeaders()
    });
  }

  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, body, {
      headers: this.generateHeaders()
    });
  }

  put<T>(endpoint: string, body: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${endpoint}`, body, {
      headers: this.generateHeaders()
    });
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${endpoint}`, {
      headers: this.generateHeaders()
    });
  }
}
