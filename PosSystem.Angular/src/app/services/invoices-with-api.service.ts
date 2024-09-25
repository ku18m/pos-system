import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployee } from '../Components/Pages/users/IEmployee';
import { IInvoices } from '../Components/Pages/invoices/IInvoices';

@Injectable({
  providedIn: 'root'
})
export class InvoicesWithAPIService {

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

}
