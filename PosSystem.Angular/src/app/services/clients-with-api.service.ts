import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IClients } from '../Components/Pages/clients/IClients';
import { RequestHandlerService } from './request-handler.service';
import { IClientsBack } from '../Components/Pages/clients/IClientBack';

@Injectable({
  providedIn: 'root',
})
export class ClientsWithAPIService {
  baseURL = 'https://localhost:7168/api/Client'; // Adjust this based on your API configuration
  parentEndpoint = 'Client';

  constructor(
    private http: HttpClient,
    private requestHandler: RequestHandlerService
  ) {}

  // Get all clients with token authorization
  getAllClients(): Observable<any> {
    // const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header
    let methodEndpoint = 'GetAll';

    return this.requestHandler.get<IClients[]>(
      `${this.parentEndpoint}/${methodEndpoint}`
    );
  }

  // Add a client with token authorization
  addClient(
    token: string,
    clientName: number,
    clientNumber: string,
    phone: string,
    address: string
  ): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header
    const body = {
      name: clientName,
      phoneNumber: phone,
      address: address,
    };

    return this.http.post(this.baseURL, body, { headers }); // Make the API call
  }

  getClientById(id: string): Observable<any> {
    return this.requestHandler.get<IClients>(`${this.parentEndpoint}/${id}`);
  }

  addNewClient(client: IClientsBack): Observable<any> {
    return this.requestHandler.post<IClients>(this.parentEndpoint, client);
  }

  updateClient(client: IClients): Observable<any> {
    return this.requestHandler.put<IClients>(`${this.parentEndpoint}/${client.id}`, client);
  }

  deleteClient(id: string): Observable<any> {
    return this.requestHandler.delete<IClients>(`${this.parentEndpoint}/${id}`);
  }

  getNextClientNumber(): Observable<any> {
    return this.requestHandler.get<any>(`${this.parentEndpoint}/GetNextClientNumber`);
  }
}
