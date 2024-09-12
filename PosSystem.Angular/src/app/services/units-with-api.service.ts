import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUnits } from '../Models/IUnits';

@Injectable({
  providedIn: 'root'
})
export class UnitsWithAPIService {

  baseURL="http://localhost:3007/unit";
  constructor(private http:HttpClient) { }

  getAllUnits(): Observable<IUnits[]>{
    return this.http.get<IUnits[]>(this.baseURL);
  }

  addUnit(type: any)  {  
    return this.http.post(this.baseURL, type);  
  }  
}
