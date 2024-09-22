import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployee } from '../Components/Pages/Employee/IEmployee';
import { IInvoices } from '../Components/Pages/invoices/IInvoices';

@Injectable({
  providedIn: 'root'
})
export class InvoicesWithAPIService {
  token:any=localStorage.getItem('token');

  numberURL="https://localhost:44376/api/Invoice/GetNextInvoiceNumber";

  clientURL="https://localhost:44376/api/Client";
  itemURL="https://localhost:44376/api/Product"
  unitURL="https://localhost:44376/api/Unit";
  invoiceURL="https://localhost:44376/api/Invoice";


  billURL="http://localhost:3010/bill";
  discountURL="http://localhost:3011/discount";
  employeeNameURL="http://localhost:3012/employee";
  billEmployeeURL="http://localhost:3013/billEmployee";
  constructor(private http:HttpClient) { }

  getAllBills():Observable<IInvoices[]>{
    return this.http.get<IInvoices[]>(this.billURL);
  }

  addBill(bill: any)  {  
    return this.http.post(this.billURL, bill);  
  }  


  getAllDiscount():Observable<IInvoices[]>{
    return this.http.get<IInvoices[]>(this.discountURL);
  }

  getAllEmployees():Observable<IEmployee[]>{
    return this.http.get<IEmployee[]>(this.employeeNameURL);
  }

  getAllBillEmployees():Observable<IInvoices[]>{
    return this.http.get<IInvoices[]>(this.billEmployeeURL);
  }

  addBillEmployee(employee: any)  {  
    return this.http.post(this.billEmployeeURL, employee);  
  }  

  GetBillNumber(): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    return this.http.get(`${this.numberURL}`, { headers }); // Make the API call  
  }


  GetAllClients(): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  
    
    return this.http.get(`${this.clientURL}`, { headers }); // Make the API call  
  }

  GetAllItems(): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  
    
    return this.http.get(`${this.itemURL}`, { headers }); // Make the API call  
  }

  GetAllUnits(): Observable<any> {   
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  
    
    return this.http.get(`${this.unitURL}`, { headers }); // Make the API call  
  }
}
