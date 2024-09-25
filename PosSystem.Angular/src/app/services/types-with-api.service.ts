import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { ITypes } from '../Components/Pages/types/ITypes';

@Injectable({
  providedIn: 'root'
})
export class TypesWithAPIService {

  GetAllURL="https://localhost:44376/api/Type/GetAll";

  parentEndpoint = 'Type';

  AddURL="https://localhost:44376/api/Type";
  constructor(private http:HttpClient, private requestHandler: RequestHandlerService) { }

  getAllTypes(token:string): Observable<any> {

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any[]>(this.GetAllURL, { headers }); // Make the API call
  }

  addTypeWithNotes(token:string ,typeName: string, notes: string,companyID:string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

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
