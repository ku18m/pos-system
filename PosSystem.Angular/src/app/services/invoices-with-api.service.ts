import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployee } from '../Components/Pages/users/IEmployee';
import { IInvoices } from '../Components/Pages/invoices/IInvoices';

@Injectable({
  providedIn: 'root'
})
export class InvoicesWithAPIService {

  token: any = localStorage.getItem('token');

  numberURL = "https://localhost:7168/api/Invoice/GetNextInvoiceNumber";

  clientURL = "https://localhost:7168/api/Client";
  itemURL = "https://localhost:7168/api/Product"
  unitURL = "https://localhost:7168/api/Unit";
  employeeURL = "https://localhost:7168/api/Users";
  invoiceURL = "https://localhost:7168/api/Invoice";
  constructor(private http: HttpClient) { }

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

  fetchAllUsers(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`, // Set the Authorization header with the token  
      'Content-Type': 'application/json'
    });

    return this.http.get(this.employeeURL, { headers }); // Perform the GET request  
  }

  addInvoice(date: string, billDate: string, paidUp: number, totalDiscount: number, totalAmount: number, invoiceItems: any[], clientId: string, employeeId: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header  

    const body = { name: date, billDate, paidUp, totalDiscount, totalAmount, invoiceItems, clientId, employeeId }; // Prepare the request body with company name and notes  

    return this.http.post(this.invoiceURL, body, { headers }); // Make the API call  
  }
}
