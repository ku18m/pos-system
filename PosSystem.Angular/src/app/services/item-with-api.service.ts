import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IItems } from '../Components/Pages/items/IItems';
import { RequestHandlerService } from './request-handler.service';
import { IItemsBack } from '../Components/Pages/items/IItemsBack';

@Injectable({
  providedIn: 'root'
})
export class ItemWithAPIService {

  baseURL="http://localhost:7168/Product";

  parentEndpoint = 'Product';

  constructor(private http:HttpClient, private requestHandler: RequestHandlerService) { }

  getAllItems(token:string):Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.get<any>(this.baseURL, { headers });
  }

  addItem(token: string, itemData: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Set the authorization header

    return this.http.post(this.baseURL, itemData, { headers });
  }

  getItemsPage(page: number, pageSize: number): Observable<any> {
    return this.requestHandler.get<any>(`${this.parentEndpoint}?pageNumber=${page}&pageSize=${pageSize}`);
  }

  getAll(): Observable<any> {
    return this.requestHandler.get<IItems[]>(`${this.parentEndpoint}/GetAll`);
  }

  updateItemQuantity(id: string, item: any) {
    return this.http.put(`${this.baseURL}/${id}`, item);
  }

  getItemById(id: string): Observable<any> {
    return this.requestHandler.get<IItems>(`${this.parentEndpoint}/${id}`);
  }

  addNewItem(product: IItemsBack): Observable<any> {
    return this.requestHandler.post<IItems>(this.parentEndpoint, product);
  }

  updateItem(product: IItemsBack): Observable<any> {
    return this.requestHandler.put<IItems>(`${this.parentEndpoint}/${product.id}`, product);
  }

  deleteItem(id: string): Observable<any> {
    return this.requestHandler.delete<any>(`${this.parentEndpoint}/${id}`);
  }
}
