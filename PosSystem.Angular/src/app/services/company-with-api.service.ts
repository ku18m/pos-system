import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICompany } from '../Components/Pages/company/ICompany';


@Injectable({
  providedIn: 'root'
})
export class CompanyWithAPIService {
  baseURL="http://localhost:3005/company";
  constructor(private http:HttpClient) { }

  getAllCompanies():Observable<ICompany[]>{
    return this.http.get<ICompany[]>(this.baseURL);
  }

  addCompany(company: any)  {  
    return this.http.post(this.baseURL, company);  
  }  
 
}
