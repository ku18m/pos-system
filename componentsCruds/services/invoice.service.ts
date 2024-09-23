import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Iinvoice } from '../models/Iinvoices';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = 'http://localhost:3005/invoices'; 

  constructor(private http: HttpClient) {}


  getAllInvoices(): Observable<Iinvoice[]> {
    return this.http.get<Iinvoice[]>(this.apiUrl);
  }

 
  getInvoiceById(id: string): Observable<Iinvoice> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Iinvoice>(url);
  }

  
  addInvoice(invoice: Iinvoice): Observable<Iinvoice> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Iinvoice>(this.apiUrl, invoice, { headers });
  }


  updateInvoice(id: string, invoice: Iinvoice): Observable<Iinvoice> {
    return this.http.put<Iinvoice>(`${this.apiUrl}/${id}`, invoice);
  }

  deleteInvoice(id: string): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
