import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { companies } from '../models/icompany';
@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

 apiUrl = 'http://localhost:3007/companies'; 
 typeUrl='http://localhost:3007/companies'

  constructor(private http: HttpClient) {}

  getCompanies(page: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?page=${page}`);
  }

  saveCompany(company: companies): Observable<any> {
    if (company.id) {
      return this.http.put(`${this.apiUrl}/${company.id}`, company);
    } else {
      return this.http.post(this.apiUrl, company);
    }
  }
   getCompanyById(companyId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/companies/${companyId}`);
  }
  addCompany(companyData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/companies`, companyData);}
   getTypes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.typeUrl}/types`); 
  }
   updateCompany(companyId: string, companyData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/companies/${companyId}`, companyData);
  }

  deleteCompany(companyId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${companyId}`);
  }
  
}

