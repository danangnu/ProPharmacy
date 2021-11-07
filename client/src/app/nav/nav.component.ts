import { DOCUMENT } from '@angular/common';
import { HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
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
    this.saveUsers();
  }

  logout() {
    this.auth.logout({returnTo: this.doc.location.origin });
  }

  async saveUsers() {
    let i = 0;
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(token => {
      this.auth.idTokenClaims$.subscribe(response => {
        if (i === 0) {
          const bar: any = {email: response.email?.toString(),
            name: response.name?.toString()};
          const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
          if (bar !== 'undefined') {
              this.accountService.register(bar, headers).subscribe(() => {
              });
            }
        }
        i++;
      });
    });
  }
}

