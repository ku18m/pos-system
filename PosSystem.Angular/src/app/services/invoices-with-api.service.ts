import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { IInvoicesBack } from '../Components/Pages/invoices/IInvoicesBack';
import { IInvoices } from '../Components/Pages/invoices/IInvoices';
@Injectable({
  providedIn: 'root'
})
export class InvoicesWithAPIService {

  parentEndpoint = 'Invoice';

  token: any = localStorage.getItem('token');

  numberURL = "https://localhost:7168/api/Invoice/GetNextInvoiceNumber";

  clientURL = "https://localhost:7168/api/Client";
  itemURL = "https://localhost:7168/api/Product"
  unitURL = "https://localhost:7168/api/Unit";
  employeeURL = "https://localhost:7168/api/Users";
  invoiceURL = "https://localhost:7168/api/Invoice";


  constructor(
    private http: HttpClient,
    private requestHandler: RequestHandlerService
  ) { }

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

  addInvoice(invoice: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`); // Set the authorization header

    console.log(invoice);

    return this.http.post(this.invoiceURL, invoice, { headers }); // Make the API call
  }

  getAll(): Observable<any> {
    return this.requestHandler.get<IInvoices[]>(`${this.parentEndpoint}/GetAll`);
  }

  getById(id: string): Observable<any> {
    return this.requestHandler.get<IInvoices>(`${this.parentEndpoint}/${id}`);
  }

  add(invoice: IInvoicesBack): Observable<any> {
    return this.requestHandler.post<any>(this.parentEndpoint, invoice);
  }

  update(invoice: IInvoices): Observable<any> {
    console.log(invoice);
    return this.requestHandler.put<any>(`${this.parentEndpoint}/${invoice.id}`, invoice);
  }

  delete(id: string): Observable<any> {
    return this.requestHandler.delete<any>(`${this.parentEndpoint}/${id}`);
  }
}