import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { ICompany } from '../Components/Pages/company/ICompany';



@Injectable({
  providedIn: 'root'
})
export class CompanyWithAPIService {
  token: any = localStorage.getItem('token');
  baseURL = "https://localhost:7168/api/Company";
  GetIDURL = "https://localhost:7168/api/Company/GetCompanyByName"

  parentEndpoint = 'Company';

  constructor(private http: HttpClient, private requestHandler: RequestHandlerService) { }


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

  getCompanyById(companyId: string): Observable<any> {
    return this.requestHandler.get<ICompany>(`${this.parentEndpoint}/${companyId}`);
  }

  updateCompany(company: ICompany): Observable<any> {
    return this.requestHandler.put<ICompany>(`${this.parentEndpoint}/${company.id}`, company);
  }

  deleteCompany(companyId: string): Observable<any> {
    return this.requestHandler.delete<any>(`${this.parentEndpoint}/${companyId}`);
  }

  getCompaniesPage(page: number, pageSize: number): Observable<any> {
    return this.requestHandler.get<any>(`${this.parentEndpoint}?pageNumber=${page}&pageSize=${pageSize}`);
  }
}
