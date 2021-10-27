import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
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
    this.auth.user$.subscribe(
      (profile) => (this.profileJson = JSON.stringify(profile, null, 2))
    );
  }

  logout() {
    this.auth.logout({returnTo: this.doc.location.origin });
  }

  async getToken() {
    this.auth.idTokenClaims$.subscribe(response => {
      const bar: any = {email: response.email?.toString(),
        lastName: response.family_name?.toString(),
        firstName: response.given_name?.toString()};
      this.user = bar;
    });

    await this.accountService.getUser(1).subscribe((response) => {
      console.log(response);
    });

    if (await this.user !== 'undefined') {
      this.accountService.register(this.user).subscribe((response) => {
        console.log(response);
      });
    }
  }
}

