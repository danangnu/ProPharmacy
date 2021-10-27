import { DOCUMENT } from '@angular/common';
import { HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { delay } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  profileJson: string = null;
  user: any;
  token: string;

  constructor(public auth: AuthService,
              @Inject(DOCUMENT) private doc: Document,
              private accountService: AccountService) { }

  ngOnInit(): void {
    let i =0;
    this.auth.getAccessTokenSilently().subscribe(token => {
      this.auth.idTokenClaims$.subscribe(response => {
        if (i===1) {
          const bar: any = {email: response.email?.toString(),
            lastName: response.family_name?.toString(),
            firstName: response.given_name?.toString()};
            const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
            if (bar !== 'undefined') {
              this.accountService.register(bar, headers).subscribe((response) => {
                console.log(response);
              });
            }
        }
        i++;
      });
    });
  }

  logout() {
    this.auth.logout({returnTo: this.doc.location.origin });
  }

  async getToken() {
    await this.auth.getAccessTokenSilently().subscribe(token => {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.accountService.getUser('nattesc82@gmail.com', headers).subscribe((response) => {
        console.log(response);
      });
    });
    
  }

  async saveUsers() {
    if (await this.auth.isAuthenticated$) {
      this.auth.idTokenClaims$.subscribe(response => {
        const bar: any = {email: response.email?.toString(),
          lastName: response.family_name?.toString(),
          firstName: response.given_name?.toString()};
        this.user = bar;
      });
  
      await this.auth.getAccessTokenSilently().subscribe(token => {
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        if (this.user !== 'undefined') {
          this.accountService.register(this.user, headers).subscribe((response) => {
            console.log(response);
          });
        }
      });
    }
  }
}

