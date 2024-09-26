import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestHandlerService } from './request-handler.service';
import { IUnits } from '../Components/Pages/unit/IUnits';

@Injectable({
  providedIn: 'root'
})
export class UnitsWithAPIService {

  parentEndpoint = 'Unit';

  baseURL="http://localhost:7168/Unit"; // Adjust this based on your API configuration
  constructor(private http: HttpClient, private requestHandler: RequestHandlerService) { }

  // Get all units with token authorization
  getAllUnits(token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any[]>(this.baseURL, { headers }); // Make the API call
  }

  // Add a unit with token authorization
  addUnit(token:string ,unitName: string, notes: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const body = { name: unitName, notes };

    return this.http.post(this.baseURL, body, { headers });
  }

  getAll(): Observable<IUnits[]> {
    return this.requestHandler.get<IUnits[]>(`${this.parentEndpoint}/GetAll`);
  }

  getById(id: string): Observable<IUnits> {
    return this.requestHandler.get<IUnits>(`${this.parentEndpoint}/${id}`);
  }

  add(unit: IUnits): Observable<any> {
    return this.requestHandler.post(`${this.parentEndpoint}`, unit);
  }

  update(unit: IUnits): Observable<any> {
    return this.requestHandler.put(`${this.parentEndpoint}/${unit.id}`, unit);
  }

  delete(id: string): Observable<any> {
    return this.requestHandler.delete(`${this.parentEndpoint}/${id}`);
  }

}
