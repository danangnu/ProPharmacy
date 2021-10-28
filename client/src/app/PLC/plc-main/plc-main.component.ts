import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { FileVersion } from 'src/app/_models/fileVersion';
import { User } from 'src/app/_models/user';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-plc-main',
  templateUrl: './plc-main.component.html',
  styleUrls: ['./plc-main.component.css']
})
export class PlcMainComponent implements OnInit {
  member: User;
  files: FileVersion[] = [];

  constructor(private memberService: MembersService,
              private auth: AuthService) {
   }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    let email = '';
    this.auth.getAccessTokenSilently().subscribe(token => {
      this.auth.idTokenClaims$.subscribe(response => {
        email = response.email?.toString();
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        this.memberService.getMember(email, headers).subscribe(response => {
          this.member = response;
          const filev = [];
          for (const version of this.member.versionCreated) {
            filev.push({
              id: version?.id,
              versionName: version?.versionName,
              created: version?.created
            });
          }
          this.files = filev.slice().reverse();
        });
      });
    });
  }

  removeItem() {

  }
}

interface Filedet {
  name: string;
  date: string;
}
