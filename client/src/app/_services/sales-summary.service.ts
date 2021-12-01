import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SalesSummary } from '../_models/salesSummary';

@Injectable({
  providedIn: 'root',
})
export class SalesSummaryService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) {}

  getReport(year: number, id: number, headers: HttpHeaders) {
    return this.http
      .get<SalesSummary>(this.baseUrl + 'salessummary/' + year + '/' + id, {
        headers,
      })
      .pipe(
        map((sched) => {
          return sched;
        })
      );
  }

  updateSalesSummary(id: number, salesum: SalesSummary, headers: HttpHeaders) {
    return this.http
      .put(this.baseUrl + 'salessummary/' + id, salesum, { headers })
      .pipe(
        map(() => {
          return null;
        })
      );
  }
}
