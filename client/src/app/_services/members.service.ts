import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FileVersion } from '../_models/fileVersion';
import { User } from '../_models/user';
import { UserReport } from '../_models/userReport';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.backendUrl;
  members: User[] = [];
  memberCache = new Map();
  files: FileVersion;

  constructor(private http: HttpClient) {}

  getMember(email: string, headers: HttpHeaders) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: User) => member.email === email);

    if (member) {
      return of(member);
    }
    return this.http.get<User>(this.baseUrl + 'users/' + email, { headers });
  }

  addReport(model: any, headers: HttpHeaders) {
    return this.http
      .post<UserReport>(this.baseUrl + 'users/add-report', model, { headers })
      .pipe(
        map((files: UserReport) => {
          return files;
        })
      );
  }

  deleteMessage(id: number, headers: HttpHeaders) {
    return this.http.delete(this.baseUrl + 'versions/' + id, { headers });
  }
}
