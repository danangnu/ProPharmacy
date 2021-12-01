import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PrescriptionReport } from '../_models/prescriptionReport';
import { PrescriptionSummary } from '../_models/prescriptionSummary';

@Injectable({
  providedIn: 'root',
})
export class PrescriptionService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) {}

  getReport(headers: HttpHeaders) {
    return this.http
      .get<PrescriptionReport[]>(this.baseUrl + 'prescriptions/report', {
        headers,
      })
      .pipe(
        map((presc) => {
          return presc;
        })
      );
  }

  getSummary(year: number, id: number, headers: HttpHeaders) {
    return this.http
      .get<PrescriptionSummary>(
        this.baseUrl + 'prescriptionsummary/' + year + '/' + id,
        { headers }
      )
      .pipe(
        map((presc) => {
          return presc;
        })
      );
  }

  updateSummary(
    id: number,
    prescsum: PrescriptionSummary,
    headers: HttpHeaders
  ) {
    return this.http
      .put(this.baseUrl + 'prescriptionsummary/' + id, prescsum, { headers })
      .pipe(
        map(() => {
          return null;
        })
      );
  }
}
