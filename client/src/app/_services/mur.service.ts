import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Mur } from '../_models/mur';

@Injectable({
  providedIn: 'root'
})
export class MurService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient) { }

  getSummary(year: number, id: number, headers: HttpHeaders) {
    return this.http
      .get<Mur>(
        this.baseUrl + 'mur/' + year + '/' + id,
        { headers }
      )
      .pipe(
        map((mur) => {
          return mur;
        })
      );
  }

  updateSummary(
    id: number,
    mur: Mur,
    headers: HttpHeaders
  ) {
    return this.http
      .put(this.baseUrl + 'mur/' + id, mur, { headers })
      .pipe(
        map(() => {
          return null;
        })
      );
  }
}
