import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TypesWithAPIService {

  GetAllURL="https://localhost:44376/api/Type/GetAll";
  
  AddURL="https://localhost:44376/api/Type";
  constructor(private http:HttpClient) { }

  getAllTypes(token:string): Observable<any> {  
     
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header  

    return this.http.get<any[]>(this.GetAllURL, { headers }); // Make the API call  
  }  

  addTypeWithNotes(token:string ,typeName: string, notes: string,companyID:string): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header  

    const body = { name: typeName, notes, companyID }; // Prepare the request body with company name and notes  

    return this.http.post(this.AddURL, body, { headers }); // Make the API call  
  }  

 
}
