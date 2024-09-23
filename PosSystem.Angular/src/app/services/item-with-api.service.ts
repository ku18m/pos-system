import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IItems } from '../Components/Pages/items/IItems';

@Injectable({
  providedIn: 'root'
})
export class ItemWithAPIService {

  baseURL="http://localhost:7168/Product";
  constructor(private http:HttpClient) { }

  getAllItems(token:string):Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any>(this.baseURL, { headers });
    }

    addItem(token: string, itemData: any): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

      return this.http.post(this.baseURL, itemData, { headers });
    }

  updateItemQuantity(id: string, item: any) {

    return this.http.put(`${this.baseURL}/${id}`, item);
  }
}
