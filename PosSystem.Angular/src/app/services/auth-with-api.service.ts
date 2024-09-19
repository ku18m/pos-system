import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { IUser } from '../Models/IUser';

@Injectable({
  providedIn: 'root',
})
export class AuthWithApiService {
  baseUrl: string = 'http://localhost:3007/usersList'; //replace this link with api
  constructor(private http: HttpClient) {}

  // Login method with token handling
  login(credentials: {
    username: string;
    password: string;
    rememberMe: boolean;
  }): Observable<any> {
    const { username, password, rememberMe } = credentials;

    return this.http.post<any>(this.baseUrl, { username, password }).pipe(
      tap((response) => {
        if (response.success) {
          this.storeToken(response.token, username, rememberMe);
        }
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Login failed:', error.message);
        return throwError(() => new Error('Login failed. Please try again.'));
      })
    );
  }

  // Store token in localStorage/sessionStorage based on rememberMe option
  private storeToken(
    token: string,
    username: string,
    rememberMe: boolean
  ): void {
    const expirationTime = new Date().getTime() + 8 * 60 * 60 * 1000; // 8 hours

    const authData = {
      token,
      username,
      expirationTime,
    };

    if (rememberMe) {
      localStorage.setItem('authData', JSON.stringify(authData));
    } else {
      sessionStorage.setItem('authToken', token);
    }
  }

  // Get token for future authenticated requests
  getToken(): string | null {
    const authData =
      localStorage.getItem('authData') || sessionStorage.getItem('authToken');
    if (!authData) return null;

    try {
      const parsedAuthData = JSON.parse(authData);
      const currentTime = new Date().getTime();
      if (currentTime > parsedAuthData.expirationTime) {
        this.clearStorage();
        return null;
      }
      return parsedAuthData.token;
    } catch {
      return null;
    }
  }

  // Clear storage on logout or token expiration
  clearStorage(): void {
    localStorage.removeItem('authData');
    sessionStorage.removeItem('authToken');
  }

  getAllUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.baseUrl);
  }
}
