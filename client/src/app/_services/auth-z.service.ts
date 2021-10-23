import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Auth } from '../_models/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthZService {
  
  constructor(private http: HttpClient) { }

  login(auth: Auth) {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type'
    });
    return this.http.post('https://dev-idc2f2o8.au.auth0.com/oauth/token', auth, {headers: headers});
  }
}
