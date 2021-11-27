import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SchedulePaymentReport } from '../_models/schedulePaymentReport';

@Injectable({
  providedIn: 'root'
})
export class SchedulePaymentService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) { }

  getReport(id: number, year: number, headers: HttpHeaders) {
    return this.http.get<SchedulePaymentReport>(this.baseUrl + 'schedulepayment/report/' + year + '/' + id, {headers}).pipe(
      map((sched) => {
        return sched;
      })
    );
  }
}
