import { HttpHeaders } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
import { FileVersion } from 'src/app/_models/fileVersion';
import { PrescriptionReport } from 'src/app/_models/prescriptionReport';
import { SchedulePaymentReport } from 'src/app/_models/schedulePaymentReport';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import { SchedulePaymentService } from 'src/app/_services/schedule-payment.service';

@Component({
  selector: 'app-plc-report',
  templateUrl: './plc-report.component.html',
  styleUrls: ['./plc-report.component.css']
})
export class PlcReportComponent implements OnInit {
  fileVersion: FileVersion;
  prescriptionReport: PrescriptionReport[] = [];
  schedulePaymentReports: SchedulePaymentReport[] = [];

  constructor(private auth: AuthService,
              private prescriptionService: PrescriptionService,
              private schedulePayService: SchedulePaymentService) { }
  ngOnInit(): void {
    this.loadPrescrptionReports();
    this.loadScheduleReports();
    //this.transpose();
  }

  transpose() {
    var data = {d : 
      [
        {
           Mon01: "03/2015",
           Mon02: "04/2015",
           Mon03: "05/2015",
        },
        {
           Mon01: "1,0",
           Mon02: "3,0",
           Mon03: "5,0",
        },
        {
           Mon01: "2,0",
           Mon02: "4,0",
           Mon03: "6,0",
           Mon04: "",
           Mon05: "",
           Mon06: "",
        },
        {
           Mon01: "10,0",
           Mon02: "11,0",
           Mon03: "12,0",
           Mon04: "",
           Mon05: "",
           Mon06: "",
      }
      ],
              length: 3};
              //console.log(data.d[0]);
    var keys = [];
for(var key in data.d[data.length]){
  // console.log(key);
  keys.push(key);
}

var newObj = [];
//newObj['length'] = data.length;
for(var k =0;k<data.length;k++){
  var obj = {};
  for(var cnt in keys){
    obj[keys[cnt]] = "";
  }
  newObj.push(obj);
 }
for(var k =0;k<data.length;k++){
   //var obj = {};
  //console.log(k);
  for(var j=0;j<data.length;j++){
    newObj[k][keys[j]] = data.d[j][keys[k]];
  }
}
console.log(newObj);
  }

  getPRHeaders() {
    let headers: string[] = [];
    if(this.prescriptionReport) {
      this.prescriptionReport.forEach((value) => {
        Object.keys(value).forEach((key) => {
          if(!headers.find((header) => header == key)){
            headers.push(key)
          }
        })
      })
    }
    return headers;
  }

  getSPHeaders() {
    let headers: string[] = [];
    if(this.schedulePaymentReports) {
      this.schedulePaymentReports.forEach((value) => {
        Object.keys(value).forEach((key) => {
          if(!headers.find((header) => header == key)){
            headers.push(key)
          }
        })
      })
    }
    return headers;
  }

  loadPrescrptionReports() {
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(token => {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.prescriptionService.getReport(headers).subscribe(presc => {
        this.prescriptionReport = presc;
      });
    });
  }

  loadScheduleReports() {
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(token => {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.schedulePayService.getReport(headers).subscribe(sched => {
        this.schedulePaymentReports = sched;
      });
    });
  }
}
