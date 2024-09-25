import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TypesWithAPIService {
  token:any=localStorage.getItem('token');
  GetAllURL="https://localhost:44376/api/Type/GetAll";
  
  AddURL="https://localhost:44376/api/Type";
  

  constructor(private http:HttpClient) { }

  getAllTypes(): Observable<any> {  
     
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    return this.http.get<any[]>(this.AddURL, { headers }); // Make the API call  
  }  

  addTypeWithNotes(typeName: string, notes: string,companyID:string): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    const body = { name: typeName, notes, companyID }; // Prepare the request body with company name and notes  

    return this.http.post(this.AddURL, body, { headers }); // Make the API call  
  }  

  
 
}
