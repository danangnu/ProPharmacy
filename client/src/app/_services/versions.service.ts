import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PrescriptionSummary } from '../_models/prescriptionSummary';

@Injectable({
  providedIn: 'root'
})
export class VersionsService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) { }

  addPrescSummary(id: number, model: any, headers: HttpHeaders) {
    return this.http.post<PrescriptionSummary>(this.baseUrl + 'versions/add-prescsum/' + id, model, {headers}).pipe(
      map((presc: PrescriptionSummary) => {
        return presc;
      })
    );
  }
}
