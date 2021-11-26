import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserReport } from '../_models/userReport';

@Injectable({
  providedIn: 'root',
})
export class UserReportService {
  baseUrl = environment.backendUrl;
  memberCache = new Map();

  constructor(private http: HttpClient) {}

  getMember(id: number, headers: HttpHeaders) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: UserReport) => member.id === id);

    if (member) {
      return of(member);
    }
    return this.http.get<UserReport>(this.baseUrl + 'userreport/' + id, {
      headers,
    });
  }

  deleteMessage(id: number, headers: HttpHeaders) {
    return this.http.delete(this.baseUrl + 'userreport/' + id, { headers });
  }
}
