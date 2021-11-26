import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ExpenseSummary } from '../_models/expenseSummary';
import { Mur } from '../_models/mur';
import { PrescriptionSummary } from '../_models/prescriptionSummary';
import { SalesSummary } from '../_models/salesSummary';
import { VersionSetting } from '../_models/versionSetting';

@Injectable({
  providedIn: 'root',
})
export class VersionsService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) {}

  addPrescSummary(id: number, model: any, headers: HttpHeaders) {
    return this.http
      .post<PrescriptionSummary>(
        this.baseUrl + 'versions/add-prescsum/' + id,
        model,
        { headers }
      )
      .pipe(
        map((presc: PrescriptionSummary) => {
          return presc;
        })
      );
  }

  addMur(id: number, model: any, headers: HttpHeaders) {
    return this.http
      .post<Mur>(this.baseUrl + 'versions/add-mur/' + id, model, { headers })
      .pipe(
        map((mur: Mur) => {
          return mur;
        })
      );
  }

  addSaleSummary(id: number, model: any, headers: HttpHeaders) {
    return this.http
      .post<SalesSummary>(this.baseUrl + 'versions/add-salessum/' + id, model, { headers })
      .pipe(
        map((sale: SalesSummary) => {
          return sale;
        })
      );
  }

  addExpSummary(id: number, model: any, headers: HttpHeaders) {
    return this.http
      .post<ExpenseSummary>(this.baseUrl + 'versions/add-expensesum/' + id, model, { headers })
      .pipe(
        map((exp: ExpenseSummary) => {
          return exp;
        })
      );
  }

  addVersionSetting(id: number, model: any, headers: HttpHeaders) {
    return this.http
      .post<VersionSetting>(this.baseUrl + 'versions/add-versionsetting/' + id, model, { headers })
      .pipe(
        map((verset: VersionSetting) => {
          return verset;
        })
      );
  }
}
