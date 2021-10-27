import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.backendUrl;

  constructor(private http: HttpClient,
              private auth: AuthService) { }

  getUser(email: string, headers: HttpHeaders) {
    return this.http.get<User>(this.baseUrl + 'users/' + email, {headers: headers});
  }

  register(model: any, headers: HttpHeaders) {
    return this.http.post(this.baseUrl + 'account/register', model, {headers: headers}).pipe(
      map((user: User) => {

      })
    );
  }
}
