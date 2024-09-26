import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { ITypes } from '../Components/Pages/types/ITypes';

@Injectable({
  providedIn: 'root'
})
export class TypesWithAPIService {

  token: any = localStorage.getItem('token');

  GetAllURL = "https://localhost:7168/api/Type/GetAll";

  parentEndpoint = 'Type';

  AddURL = "https://localhost:7168/api/Type";
  constructor(private http: HttpClient, private requestHandler: RequestHandlerService) { }

  getAllTypes(): Observable<any> {

    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    return this.http.get<any[]>(this.AddURL, { headers }); // Make the API call  
  }

  addTypeWithNotes(typeName: string, notes: string, companyID: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    const body = { name: typeName, notes, companyID }; // Prepare the request body with company name and notes  

    return this.http.post(this.AddURL, body, { headers }); // Make the API call  
  }


  getTypes(): Observable<any> {
    let methodEndpoint = 'GetAll';
    return this.requestHandler.get<ITypes[]>(`${this.parentEndpoint}/${methodEndpoint}`);
  }

  getTypesByCompanyId(companyId: string): Observable<any> {
    return this.requestHandler.get<ITypes[]>(`${this.parentEndpoint}/GetByCompanyId?companyId=${companyId}`);
  }

  deleteType(typeId: string): Observable<any> {
    return this.requestHandler.delete<any>(`${this.parentEndpoint}/${typeId}`);
  }
}
