import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUnit } from '../models/iunit';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'  
})
export class UnitServices {
baseUrl:string="http://localhost:3008/units";
  constructor(private http: HttpClient) { }
  getAllUnits(): Observable<IUnit[]> {
    return this.http.get<IUnit[]>(this.baseUrl);
  }

  getUnitById(id: string): Observable<IUnit> {
    return this.http.get<IUnit>(`${this.baseUrl}/${id}`);
  }

  addNewUnit(unit: IUnit): Observable<IUnit> {
    return this.http.post<IUnit>(this.baseUrl, unit);
  }

  updateUnit(unit: IUnit): Observable<IUnit> {
    return this.http.put<IUnit>(`${this.baseUrl}/${unit.id}`, unit);
  }

  deleteUnit(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
