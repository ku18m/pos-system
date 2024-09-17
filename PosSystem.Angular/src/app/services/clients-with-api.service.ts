import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IClients } from '../Components/Pages/clients/IClients';

@Injectable({
  providedIn: 'root'
})
export class ClientsWithAPIService {
  baseURL="http://localhost:3009/client";
  constructor(private http:HttpClient) { }

  getAllClients():Observable<IClients[]>{
    return this.http.get<IClients[]>(this.baseURL);
  }

  addClient(company: any)  {  
    return this.http.post(this.baseURL, company);  
  }  
 
}
