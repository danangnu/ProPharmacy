import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.css']
})
export class LoginButtonComponent implements OnInit {
  
  constructor(public auth: AuthService, private accountService: AccountService) { }

  ngOnInit(): void {
  }

  async loginWithRedirect() {
    await this.auth.loginWithRedirect();
  }
}
