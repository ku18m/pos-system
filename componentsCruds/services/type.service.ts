import { Injectable } from '@angular/core';
import { itype } from '../models/itype';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
 apiUrl = 'http://localhost:3005/types'; 
 constructor(private http: HttpClient) {}

  getAllTypes(): Observable<itype[]> {
    return this.http.get<itype[]>(this.apiUrl);
  }

  getTypeById(id: string): Observable<itype> {
     return this.http.get<any>(`${this.apiUrl}/${id}`);
  }


  addType(type: itype): Observable<itype> {
 return this.http.post<any>(`${this.apiUrl}`, type);
  }


  updateType(id: string, type: itype): Observable<itype> {
      return this.http.put<any>(`${this.apiUrl}/${id}`, type);
  }

 
  deleteType(id: string): Observable<any> {
     return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
