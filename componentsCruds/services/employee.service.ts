import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IEmployee } from '../models/iemployee';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

 baseUrl:string="http://localhost:3007/employees";
    constructor(private http: HttpClient) {}
    getAllEmployee(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(this.baseUrl);
  }
   getEmployeeById(employeeId: number): Observable<IEmployee> {
    return this.http.get<IEmployee>(`${this.baseUrl}/${employeeId}`);
  }
  addNewEmployee(employee: IEmployee): Observable<IEmployee> {
    return this.http.post<IEmployee>(this.baseUrl, employee);
  }


  updateEmployee(employee: IEmployee): Observable<IEmployee> {
    return this.http.put<IEmployee>(`${this.baseUrl}/${employee.id}`,employee);
  }

  deleteEmployee(employeeId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${employeeId}`);

}
}
