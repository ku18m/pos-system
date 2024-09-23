import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IClient } from '../models/iclient'; 

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  baseUrl: string = "http://localhost:3004/clients"; 

  constructor(private http: HttpClient) {}

  getAllClients(): Observable<IClient[]> { 
    return this.http.get<IClient[]>(this.baseUrl);
  }

  getClientById(clientId: number): Observable<IClient> { 
    return this.http.get<IClient>(`${this.baseUrl}/${clientId}`);
  }

  addNewClient(client: IClient): Observable<IClient> { 
    return this.http.post<IClient>(this.baseUrl, client);
  }

  updateClient(client: IClient): Observable<IClient> { 
    return this.http.put<IClient>(`${this.baseUrl}/${client.id}`, client);
  }

  deleteClient(clientId: number): Observable<void> { 
    return this.http.delete<void>(`${this.baseUrl}/${clientId}`);
  }
}
