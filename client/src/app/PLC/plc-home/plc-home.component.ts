import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { take } from 'rxjs/operators';
import { UserReport } from 'src/app/_models/userReport';
import { MembersService } from 'src/app/_services/members.service';
import { UserReportService } from 'src/app/_services/user-report.service';

@Component({
  selector: 'app-plc-home',
  templateUrl: './plc-home.component.html',
  styleUrls: ['./plc-home.component.css'],
})
export class PlcHomeComponent implements OnInit {
  member: User;
  bsModalRef: BsModalRef;
  userReport: UserReport[] = [];
  message: string;

  constructor(
    private modalService: BsModalService,
    private auth: AuthService,
    private memberService: MembersService,
    private userreportService: UserReportService
  ) {}

  ngOnInit() {
    this.loadReport();
  }

  loadReport() {
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
          this.memberService.getMember(email, headers).subscribe((response) => {
            this.member = response;
            const filev = [];
            for (const report of this.member.reportCreated) {
              filev.push({
                id: report?.id,
                reportName: report?.reportName,
                created: report?.created,
              });
            }
            this.userReport = filev.slice().reverse();
          });
        });
      });
  }

  removeItem(files: UserReport) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.userreportService
          .deleteMessage(files.id, headers)
          .subscribe(() => {
            this.userReport.splice(
              this.userReport.findIndex((m) => m.id === files.id),
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
