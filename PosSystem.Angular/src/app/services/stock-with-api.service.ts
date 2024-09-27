import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockWithAPIService {
  token: any = localStorage.getItem('token');
  constructor(private http: HttpClient) { }
  itemURL = "https://localhost:7168/api/Product"

  GetAllItems(): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    return this.http.get(`${this.itemURL}`, { headers }); // Make the API call  
  }
}
