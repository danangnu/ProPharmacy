import { HttpClient } from '@angular/common/http';
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

  constructor(private http: HttpClient, private auth: AuthService) { }

  getUser(id: number) {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {

      })
    );
  }

  getToken() {
    return this.auth.getAccessTokenSilently().pipe(
      map((response: string) => {
        const user = response;
        return user;
      })
    );
  }
}
