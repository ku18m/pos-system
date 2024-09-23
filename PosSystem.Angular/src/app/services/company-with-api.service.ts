import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class CompanyWithAPIService {
  baseURL="https://localhost:44376/api/Company";

  constructor(private http:HttpClient) { }


  getAllCompanies(token:string): Observable<any> {

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any[]>(this.baseURL, { headers }); // Make the API call
  }

  addCompanyWithNotes(token:string ,companyName: string, notes: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    const body = { name: companyName, notes }; // Prepare the request body with company name and notes

    return this.http.post(this.baseURL, body, { headers }); // Make the API call
  }



}

