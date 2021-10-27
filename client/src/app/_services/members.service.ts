import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.backendUrl;
  members: User[] = [];
  memberCache = new Map();

  constructor(private http: HttpClient) { }

  getMember(email: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: User) => member.email === email);

    if (member) {
      return of(member);
    }
    return this.http.get<User>(this.baseUrl + 'users/' + email);
  }
}
