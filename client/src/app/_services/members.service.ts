import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FileVersion } from '../_models/fileVersion';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.backendUrl;
  members: User[] = [];
  memberCache = new Map();
  files: FileVersion;

  constructor(private http: HttpClient) { }

  getMember(email: string, headers: HttpHeaders) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: User) => member.email === email);

    if (member) {
      return of(member);
    }
    return this.http.get<User>(this.baseUrl + 'users/' + email, {headers});
  }

  addVersion(model: any, headers: HttpHeaders) {
    return this.http.post<FileVersion>(this.baseUrl + 'users/add-version', model, {headers}).pipe(
      map((files: FileVersion) => {
        this.files = files;
      })
    );
  }
}
