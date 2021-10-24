import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';

@Injectable()
export class AccesstokenInterceptor implements HttpInterceptor {
  private currentToken = new ReplaySubject<string>(1);
  currentToken$ = this.currentToken.asObservable();
  token: string;

  constructor(private auth: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.getToken();
    if (this.token != null) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`
        }
      });
    }  
    return next.handle(request);
  }

  async getToken() {
    await this.auth.getAccessTokenSilently().subscribe(response => {
      if (response) {
        this.token = response;
      }
    });
  }
}
