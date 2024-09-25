import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class CompanyWithAPIService {
  token:any=localStorage.getItem('token');
  baseURL="https://localhost:44376/api/Company";
  GetIDURL="https://localhost:44376/api/Company/GetCompanyByName"
  constructor(private http:HttpClient) { }

  
  getAllCompanies(): Observable<any> {  
     
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    return this.http.get<any[]>(this.baseURL, { headers }); // Make the API call  
  }  

  addCompanyWithNotes(companyName: string, notes: string): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    const body = { name: companyName, notes }; // Prepare the request body with company name and notes  

    return this.http.post(this.baseURL, body, { headers }); // Make the API call  
  }  

  GetCompany(companyName: string): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    const body = { name: companyName }; // Prepare the request body with company name and notes  

    return this.http.get(`${this.GetIDURL}?name=${companyName}`, { headers }); // Make the API call  
  }
 
 
  
}  

