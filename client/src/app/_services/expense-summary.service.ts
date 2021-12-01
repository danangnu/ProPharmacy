import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ExpenseSummary } from '../_models/expenseSummary';

@Injectable({
  providedIn: 'root',
})
export class ExpenseSummaryService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) {}

  getReport(year: number, id: number, headers: HttpHeaders) {
    return this.http
      .get<ExpenseSummary>(this.baseUrl + 'expensesummary/' + year + '/' + id, {
        headers,
      })
      .pipe(
        map((sched) => {
          return sched;
        })
      );
  }

  updateExpenseSummary(
    id: number,
    expsum: ExpenseSummary,
    headers: HttpHeaders
  ) {
    return this.http
      .put(this.baseUrl + 'expensesummary/' + id, expsum, { headers })
      .pipe(
        map(() => {
          return null;
        })
      );
  }
}
