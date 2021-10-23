import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Auth } from '../_models/auth';
import { AuthZService } from '../_services/auth-z.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any
  auths: Auth;

  constructor(public auth: AuthService, private authlog: AuthZService) { }

  ngOnInit(): void {
    if (this.auth.isAuthenticated$) {
      this.auth.user$.subscribe(data => {
      this.model = data;
      this.authlog.login(this.auths).subscribe(response => {
        console.log(response);
      });
      });   

      
    }
  }

}
