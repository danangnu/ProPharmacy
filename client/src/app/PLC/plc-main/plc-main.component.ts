import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { take } from 'rxjs/operators';
import { FileVersion } from 'src/app/_models/fileVersion';
import { UserReport } from 'src/app/_models/userReport';
import { MembersService } from 'src/app/_services/members.service';
import { UserReportService } from 'src/app/_services/user-report.service';

@Component({
  selector: 'app-plc-main',
  templateUrl: './plc-main.component.html',
  styleUrls: ['./plc-main.component.css'],
})
export class PlcMainComponent implements OnInit {
  member: UserReport;
  files: FileVersion[] = [];
  bsModalRef: BsModalRef;
  message: string;

  constructor(
    private userreportService: UserReportService,
    private memberService: MembersService,
    private auth: AuthService,
    private modalService: BsModalService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    let email = '';
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        this.auth.idTokenClaims$.subscribe((response) => {
          email = response.email?.toString();
          const headers = new HttpHeaders().set(
            'Authorization',
            `Bearer ${token}`
          );
          this.userreportService
            .getMember(Number(this.route.snapshot.paramMap.get('id')), headers)
            .subscribe((response) => {
              this.member = response;
              const filev = [];
              for (const version of this.member.versionCreated) {
                filev.push({
                  id: version?.id,
                  versionName: version?.versionName,
                  created: version?.created,
                });
              }
              this.files = filev.slice().reverse();
            });
        });
      });
  }

  removeItem(files: FileVersion) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.memberService.deleteMessage(files.id, headers).subscribe(() => {
          this.files.splice(
            this.files.findIndex((m) => m.id === files.id),
            1
          );
        });
      });
  }

  receiveMessage($event) {
    this.message = $event;
    this.modalService.hide(1);
  }

  closeModal(modalId?: number) {
    this.modalService.hide(modalId);
  }

  postToggle(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(template, {
      id: 1,
      class: 'modal-lg',
    });
  }
}
