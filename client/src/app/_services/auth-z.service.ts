import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthData, AUTH_CONFIG } from '../_models/authData';
import { Tokem } from '../_models/tokem';

@Injectable({
  providedIn: 'root'
})
export class AuthZService {
  
  constructor(private http: HttpClient) { 
  }

  login() {
    const headerDict = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
    }
    this.http.post('https://dev-idc2f2o8.au.auth0.com/oauth/token', AUTH_CONFIG, {headers: headerDict}).pipe(
      map((response: Tokem) => {
         const member = response;
         console.log('test');
         if (member) {
           localStorage.setItem('tokens', JSON.stringify(member));
         }
    }));
  }
}
