import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IClients } from '../Components/Pages/clients/IClients';

@Injectable({
  providedIn: 'root'
})
export class ClientsWithAPIService {
  baseURL = "http://localhost:7168/Client"; // Adjust this based on your API configuration

  constructor(private http: HttpClient) { }

  // Get all clients with token authorization
  getAllClients(token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any>(this.baseURL, { headers }); // Make the API call
  }

  // Add a client with token authorization
  addClient(token: string, clientNumber:number,clientName:string,phone:string,address:string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header
    const body = { name: clientName,clientNumber:clientNumber, Phone:phone,Address:address };


    return this.http.post(this.baseURL, body, { headers }); // Make the API call
  }
}
