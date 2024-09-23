import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnitsWithAPIService {

  baseURL="http://localhost:7168/Unit"; // Adjust this based on your API configuration
  constructor(private http: HttpClient) { }

  // Get all units with token authorization
  getAllUnits(token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any[]>(this.baseURL, { headers }); // Make the API call
  }

  // Add a unit with token authorization
  addUnit(token:string ,unitName: string, notes: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const body = { name: unitName, notes };

    return this.http.post(this.baseURL, body, { headers });
  }
}
