import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
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
  styleUrls: ['./plc-report.component.css'],
})
export class PlcReportComponent implements OnInit {
  fileVersion: FileVersion;
  prescriptionReport: PrescriptionReport[] = [];
  schedulePaymentReports: SchedulePaymentReport[] = [];
  EntriesArray: FormArray;
  LabelsArray: FormArray;
  registerForm: FormGroup;
  Expense = 0;
  gross = 0;
  noYear = 1;
  decrease = 0.0;
  startYear: number;
  YearA: number;
  Years: string[] = [];
  AvgYears: string[] = [];
  YearVal: any[][] = [[], []];
  MonthPresc: number[][] = [[], [], [], [], [], [], [], [], [], [], [], []];
  zeroOTCSale: number[] = [];
  vatOTCSale: number[] = [];
  mur: number[] = [];
  nhsother: number[] = [];
  nms: number[] = [];
  advother: number[] = [];
  nhsenhancedserv: number[] = [];
  nhsundries: number[] = [];
  qualitypay: number[] = [];
  pharmacyaccscheme: number[] = [];
  buyingprofit: number[] = [];

  constructor(
    private auth: AuthService,
    private prescriptionService: PrescriptionService,
    private schedulePayService: SchedulePaymentService,
    private fb: FormBuilder
  ) {
    this.startYear = new Date().getFullYear();
  }
  ngOnInit(): void {
    this.loadPrescrptionReports();
    this.loadScheduleReports();
    this.initializeForm();
    this.mur.push(400);
    this.mur.push(400);
    this.AvgYears.push('Month');
    this.AvgYears.push(
      this.startYear.toString() + '/' + (this.startYear + 1).toString()
    );
    this.AvgYears.push('Avg Item');
    this.AvgYears.push(
      (this.startYear + 1).toString() + '/' + (this.startYear + 2).toString()
    );
    this.AvgYears.push('Avg Item');
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.AvgYears.push(
        'Projected Volume ' +
          (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString()
      );
    }
    this.Years.push('');
    this.Years.push(
      this.startYear.toString() + '/' + (this.startYear + 1).toString()
    );
    this.Years.push(
      (this.startYear + 1).toString() + '/' + (this.startYear + 2).toString()
    );
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.Years.push(
        (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString()
      );
    }
  }

  initializeForm() {
    this.LabelsArray = new FormArray([
      new FormControl('', Validators.required),
    ]);
    this.EntriesArray = new FormArray([
      new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
    ]);
  }

  addInputControl() {
    this.LabelsArray.push(new FormControl('', Validators.required));
    this.EntriesArray.push(
      new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$')])
    );
  }

  removeInputControl(idx: number) {
    this.LabelsArray.removeAt(idx);
    this.EntriesArray.removeAt(idx);
  }

  changeColumns() {
    this.AvgYears = [];
    this.Years = [];
    this.AvgYears.push('Month');
    this.AvgYears.push(
      this.startYear.toString() + '/' + (Number(this.startYear) + 1).toString()
    );
    this.AvgYears.push('Avg Item');
    this.AvgYears.push(
      (Number(this.startYear) + 1).toString() +
        '/' +
        (Number(this.startYear) + 2).toString()
    );
    this.AvgYears.push('Avg Item');
    this.Years.push('');
    this.Years.push(
      this.startYear.toString() + '/' + (Number(this.startYear) + 1).toString()
    );
    this.Years.push(
      (Number(this.startYear) + 1).toString() +
        '/' +
        (Number(this.startYear) + 2).toString()
    );
    const yr = Number(this.startYear) + 2;
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = Number(this.startYear);
      else this.YearA += 1;
      this.AvgYears.push(
        'Projected Volume ' +
          yr.toString() +
          '/' +
          (Number(this.YearA) + 3).toString()
      );
      this.Years.push(
        (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString()
      );
    }
  }

  getTotal(idx: number): number {
    let total = 0;
    for (var i = 0; i < 2; i++) {
      total += Number(this.YearVal[i][idx]);
    }
    return total;
  }

  getTotalItems(idx: number): number {
    let total = 0;
    for (var i = 0; i < this.MonthPresc.length; i++) {
      if (this.MonthPresc[i][idx] !== undefined)
        total += Number(this.MonthPresc[i][idx]);
      else total += 0;
    }
    if (idx == 2 || idx == 4) total = total / this.MonthPresc.length;
    if (idx > 4)
      total = Math.round(
        this.getTotalItems(3) * Math.pow(1 - this.decrease / 100, idx - 4)
      );
    return total;
  }

  getNHSSalesReimburse(idx: number): number {
    let nsr = 0;
    nsr = this.getTotalItems(idx) * this.getTotalItems(idx + 1);
    if (idx > 1)
      nsr = this.getTotalItems(idx + 1) * this.getTotalItems(idx + 2);
    if (idx > 2) nsr = this.getNHSSalesReimburse(2);
    return nsr;
  }

  getSAF(idx: number): number {
    let saf = 0;
    saf = Math.round(this.getTotalItems(idx) * 1.26);
    if (idx == 2) saf = Math.round(this.getTotalItems(idx + 1) * 1.27);
    if (idx > 2) saf = Math.round(this.getTotalItems(idx + 2) * 1.27);
    return saf;
  }

  getEstablishedPay(idx: number): number {
    let est = 0;
    if (idx > 1) idx += 1;
    for (var i = 0; i < this.MonthPresc.length; i++) {
      if (this.MonthPresc[i][idx] !== undefined) {
        if (
          this.MonthPresc[i][idx] >= 2500 &&
          this.MonthPresc[i][idx] <= 2829
        ) {
          est += 1164;
        } else if (
          this.MonthPresc[i][idx] >= 2830 &&
          this.MonthPresc[i][idx] <= 3149
        ) {
          est += 1210;
        } else if (this.MonthPresc[i][idx] >= 3150) {
          est += 1255;
        } else if (this.MonthPresc[i][idx] < 2500) {
          est += 0;
        } else est += 0;
      }
    }
    return est;
  }

  getSubtotalNHS(idx: number): number {
    let total = 0;
    total += this.getSAF(idx);
    total += this.getEstablishedPay(idx);
    total += Number(this.nhsother[idx]);
    return total;
  }

  getSubtotalAdv(idx: number): number {
    let total = 0;
    total += Number(this.nms[idx]);
    total += this.getMUR(idx);
    total += Number(this.advother[idx]);
    return total;
  }

  getMUR(idx: number): number {
    let mur = 0;
    mur = Number(this.mur[idx - 1]) * 28;
    return mur;
  }

  getTotalOTC(idx: number): number {
    let total = 0;
    total += Number(this.zeroOTCSale[idx]);
    total += Number(this.vatOTCSale[idx]);
    return total;
  }

  getNHSSales(idx: number): number {
    let total = 0;
    total += this.getNHSSalesReimburse(idx);
    total += this.getSubtotalNHS(idx);
    total += this.getSubtotalAdv(idx);
    total += Number(this.nhsenhancedserv[idx]);
    total += Number(this.nhsundries[idx]);
    total += Number(this.qualitypay[idx]);
    total += Number(this.pharmacyaccscheme[idx]);
    total += Number(this.buyingprofit[idx]);
    return total;
  }

  getGrandTotalNHS(idx: number): number {
    let total = 0;
    total += this.getTotalOTC(idx);
    total += this.getNHSSales(idx);
    return total;
  }

  getGross(idx: number): number {
    let gross = 0;
    gross = this.Expense;
    if (this.getTotal(idx) != null) gross += Number(this.getTotal(idx));
    return gross;
  }

  saveExpense() {
    let form: FormGroup = new FormGroup({});
    for (let i = 0; i < this.EntriesArray.length; i++) {
      form.addControl(
        this.LabelsArray.value[i],
        new FormControl(this.EntriesArray.value[i], [
          Validators.required,
          Validators.pattern('^[0-9]*$'),
        ])
      );
    }
    this.Expense =
      this.gross +
      this.EntriesArray.value.reduce(
        (prev, next) => Number(prev) + Number(next),
        0
      );
  }

  getPRHeaders() {
    let headers: string[] = [];
    if (this.prescriptionReport) {
      this.prescriptionReport.forEach((value) => {
        Object.keys(value).forEach((key) => {
          if (!headers.find((header) => header == key)) {
            headers.push(key);
          }
        });
      });
    }
    return headers;
  }

  getSPHeaders() {
    let headers: string[] = [];
    if (this.schedulePaymentReports) {
      this.schedulePaymentReports.forEach((value) => {
        Object.keys(value).forEach((key) => {
          if (!headers.find((header) => header == key)) {
            headers.push(key);
          }
        });
      });
    }
    return headers;
  }

  loadPrescrptionReports() {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.prescriptionService.getReport(headers).subscribe((presc) => {
          this.prescriptionReport = presc;
        });
      });
  }

  loadScheduleReports() {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.schedulePayService.getReport(headers).subscribe((sched) => {
          this.schedulePaymentReports = sched;
          this.gross = sched
            .map((a) => a.nhS_SalesSum)
            .reduce(function (a, b) {
              return a + b;
            });
          this.Expense = +this.gross;
        });
      });
  }
}
