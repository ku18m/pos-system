import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITypes } from '../Models/ITypes';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TypesWithAPIService {

  baseURL="http://localhost:3006/types";
  constructor(private http:HttpClient) { }

  getAllTypes(): Observable<ITypes[]>{
    return this.http.get<ITypes[]>(this.baseURL);
  }

  addType(type: any)  {  
    return this.http.post(this.baseURL, type);  
  }  

 
}
