import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
import { FileVersion } from 'src/app/_models/fileVersion';
import { PrescriptionReport } from 'src/app/_models/prescriptionReport';
import { PrescriptionService } from 'src/app/_services/prescription.service';

@Component({
  selector: 'app-plc-report',
  templateUrl: './plc-report.component.html',
  styleUrls: ['./plc-report.component.css']
})
export class PlcReportComponent implements OnInit {
  fileVersion: FileVersion;
  prescriptionReport: PrescriptionReport[];
  
  constructor(private auth: AuthService,
              private prescriptionService: PrescriptionService) { }

  ngOnInit() {
    this.loadPrescrptionReports();
  }

  loadPrescrptionReports() {
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(token => {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.prescriptionService.getReport(headers).subscribe(presc => {
        this.prescriptionReport = presc;
      });
    });
  }
}
