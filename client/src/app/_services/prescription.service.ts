import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PrescriptionReport } from '../_models/prescriptionReport';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) { }

  getReport(headers: HttpHeaders) {
    return this.http.get<PrescriptionReport[]>(this.baseUrl + 'prescriptions/report', {headers}).pipe(
      map((presc) => {
        return presc;
      })
    );
  }
}
