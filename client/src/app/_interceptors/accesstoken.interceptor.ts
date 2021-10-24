import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';
import { Token } from '@angular/compiler/src/ml_parser/lexer';
import { Tokem } from '../_models/tokem';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable()
export class AccesstokenInterceptor implements HttpInterceptor {
  token: string;

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   this.accountService.getToken().subscribe((response) =>{
      this.token = response;
   });
    
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.token}`
      }
    }); 
    return next.handle(request);
  }  
}

