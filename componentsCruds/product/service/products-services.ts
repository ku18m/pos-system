import { types } from './../models/types';
import { Injectable } from "@angular/core";
import { Iproducts } from "../models/iproducts";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from 'rxjs';
import { companies } from "../models/companies";


@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  baseUrl: string = "http://localhost:3004/products";
  companyUrl:String="http://localhost:3007/companies";
  typeUrl:String="http://localhost:3006";
  

  constructor(private http: HttpClient) {}

 
 getProducts(page: number, size: number): Observable<any> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('size', size.toString());

    return this.http.get<any>(this.baseUrl, { params });
  }

  getProductById(productId: number): Observable<Iproducts> {
    return this.http.get<Iproducts>(`${this.baseUrl}/${productId}`);
  }

  addNewProduct(product: Iproducts): Observable<Iproducts> {
    return this.http.post<Iproducts>(this.baseUrl, product);
  }


  updateProduct(product: Iproducts): Observable<Iproducts> {
    return this.http.put<Iproducts>(`${this.baseUrl}/${product.id}`, product);
  }

  deleteProduct(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${productId}`);
  }


  editProduct(product: Iproducts): Observable<Iproducts> {
    return this.http.put<Iproducts>(`${this.baseUrl}/${product.id}`, product);
  }
    getCompanies(): Observable<any[]> {
    return this.http.get<companies[]>(`${this.companyUrl}`);
  }
     getCompanyById(companyId:string): Observable<companies> {
    return this.http.get<companies>(`${this.companyUrl}/${companyId}`);
  }
 

}
