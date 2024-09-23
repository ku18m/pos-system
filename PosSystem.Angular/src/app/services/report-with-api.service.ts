import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReportWithApiService {
  baseURL = 'https://localhost:7168/api/Sales/GetPeriodReport';
  constructor(private http: HttpClient) {}

  private formatDate(date: Date): string {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const year = date.getFullYear();

    return `${month}/${day}/${year}`;
  }

  getPeriodReport(
    startDate: Date,
    endDate: Date,
    token: string
  ): Observable<any> {
    // Format dates as MM/DD/YYYY
    const formattedStartDate = this.formatDate(startDate);
    const formattedEndDate = this.formatDate(endDate);

    const params = new HttpParams()
      .set('startDate', formattedStartDate)
      .set('endDate', formattedEndDate);

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseURL, { params, headers });
  }

  getAllReports(token: string): Observable<any> {
    const headers = new HttpHeaders().set(`Authorization`, `Bearer ${token}`);

    return this.http.get<any[]>(this.baseURL + `/GetPeriodReport`, { headers });
  }
}
