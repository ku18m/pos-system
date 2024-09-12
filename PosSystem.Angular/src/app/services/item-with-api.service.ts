import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IItems } from '../Models/IItems';

@Injectable({
  providedIn: 'root'
})
export class ItemWithAPIService {

  baseURL="http://localhost:3008/item";
  constructor(private http:HttpClient) { }

  getAllItems():Observable<IItems[]>{
    return this.http.get<IItems[]>(this.baseURL);
  }

  addItem(company: any)  {  
    return this.http.post(this.baseURL, company);  
  }  

  updateItemQuantity(id: string, item: any) {  

    return this.http.put(`${this.baseURL}/${id}`, item);   
  }  
}
