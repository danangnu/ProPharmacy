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
  Entries2Array: FormArray;
  LabelsArray: FormArray;
  registerForm: FormGroup;
  Expense: number[] = [];
  gross = 0;
  noYear = 1;
  decrease = 0.0;
  startYear: number;
  YearA: number;
  Years: string[] = [];
  AvgYears: string[] = [];
  YearVal: any[][] = [[], [], [], [], [], [], []];
  MonthPresc: any[][] = [[], [], [], [], [], [], [], [], [], [], [], []];
  zeroOTCSale: any[] = [];
  vatOTCSale: any[] = [];
  mur: any[] = [];
  nhsother: any[] = [];
  nms: any[] = [];
  advother: any[] = [];
  nhsenhancedserv: any[] = [];
  nhsundries: any[] = [];
  qualitypay: any[] = [];
  pharmacyaccscheme: any[] = [];
  buyingprofit: any[] = [];
  transitionpay: any[] = [];

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
    this.mur.push('Total MURs (Max 400)');
    this.mur.push(400);
    this.mur.push(400);
    this.MonthPresc[0][0] = 'April';
    this.MonthPresc[1][0] = 'May';
    this.MonthPresc[2][0] = 'June';
    this.MonthPresc[3][0] = 'July';
    this.MonthPresc[4][0] = 'August';
    this.MonthPresc[5][0] = 'September';
    this.MonthPresc[6][0] = 'October';
    this.MonthPresc[7][0] = 'November';
    this.MonthPresc[8][0] = 'December';
    this.MonthPresc[9][0] = 'January';
    this.MonthPresc[10][0] = 'February';
    this.MonthPresc[11][0] = 'March';
    this.initialValue();
    this.YearVal[0][0] = 'OTC Sales';
    this.YearVal[1][0] = 'NHS Sales';
    this.zeroOTCSale.push('Zero Rated OTC Sales');
    this.vatOTCSale.push('VAT Exclusive OTC Sales');
    this.nhsother.push('Other');
    this.nms.push('NMS');
    this.transitionpay.push('Transition Payment');
    this.advother.push('Other');
    this.nhsenhancedserv.push('NHS Enhanced services');
    this.nhsundries.push('NHS Sundries');
    this.qualitypay.push('Quality Payments');
    this.pharmacyaccscheme.push('Pharmacy Access Scheme');
    this.buyingprofit.push('Buying Profit');
    this.AvgYears.push('Month');
    this.AvgYears.push(
      'Annual Prescription Items ' +
        this.startYear.toString() +
        '/' +
        (this.startYear + 1).toString().substring(2, 4)
    );
    this.AvgYears.push('average item value');
    this.AvgYears.push(
      'Annual Prescription Items ' +
        (this.startYear + 1).toString() +
        '/' +
        (this.startYear + 2).toString().substring(2, 4)
    );
    this.AvgYears.push('average item value');
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.AvgYears.push(
        'Projected Prescription volume for ' +
          (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString().substring(2, 4)
      );
    }
    this.Years.push('');
    this.Years.push(
      this.startYear.toString() +
        '/' +
        (this.startYear + 1).toString().substring(2, 4)
    );
    this.Years.push(
      'Year 1 ' +
        ' (' +
        (this.startYear + 1).toString() +
        '/' +
        (this.startYear + 2).toString().substring(2, 4) +
        ')'
    );
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.Years.push(
        'Year ' +
          (i + 2) +
          ' (' +
          (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString().substring(2, 4) +
          ')'
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
    this.Entries2Array = new FormArray([
      new FormControl('', [
        Validators.required,
        Validators.pattern('^[0-9]*$'),
      ]),
    ]);
  }

  initialValue() {
    for (let i = 1; i < 5; i++) {
      for (let j = 0; j < 12; j++) this.MonthPresc[j][i] = 0;
    }
  }

  addInputControl() {
    this.LabelsArray.push(new FormControl('', Validators.required));
    this.EntriesArray.push(
      new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$')])
    );
    this.Entries2Array.push(
      new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$')])
    );
  }

  removeInputControl(idx: number) {
    this.LabelsArray.removeAt(idx);
    this.EntriesArray.removeAt(idx);
    this.Entries2Array.removeAt(idx);
  }

  changeColumns() {
    this.AvgYears = [];
    this.Years = [];
    this.AvgYears.push('Month');
    this.AvgYears.push(
      'Annual Prescription Items ' +
        this.startYear.toString() +
        '/' +
        (Number(this.startYear) + 1).toString().substring(2, 4)
    );
    this.AvgYears.push('average item value');
    this.AvgYears.push(
      'Annual Prescription Items ' +
        (Number(this.startYear) + 1).toString() +
        '/' +
        (Number(this.startYear) + 2).toString().substring(2, 4)
    );
    this.AvgYears.push('average item value');
    this.Years.push('');
    this.Years.push(
      this.startYear.toString() +
        '/' +
        (Number(this.startYear) + 1).toString().substring(2, 4)
    );
    this.Years.push(
      'Year 1' +
        ' (' +
        (Number(this.startYear) + 1).toString() +
        '/' +
        (Number(this.startYear) + 2).toString().substring(2, 4) +
        ')'
    );
    const yr = Number(this.startYear) + 2;
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = Number(this.startYear);
      else this.YearA += 1;
      this.AvgYears.push(
        'Projected Prescription volume for ' +
          yr.toString() +
          '/' +
          (Number(this.YearA) + 3).toString().substring(2, 4)
      );
      this.Years.push(
        'Year ' +
          (i + 2) +
          ' (' +
          (Number(this.YearA) + 2).toString() +
          '/' +
          (Number(this.YearA) + 3).toString().substring(2, 4) +
          ')'
      );
    }
  }

  getTotal(idx: number): number {
    let total = 0;
    for (var i = 0; i < 2; i++) {
      if (this.YearVal[i][idx] != null) total += Number(this.YearVal[i][idx]);
    }
    return total;
  }

  cpZeroOTCSale(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.zeroOTCSale[idx + i] = this.zeroOTCSale[idx];
      }
    }
  }

  cpVatOTCSale(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.vatOTCSale[idx + i] = this.vatOTCSale[idx];
      }
    }
  }

  cpNhsOther(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsother[idx + i] = this.nhsother[idx];
      }
    }
  }

  cpNms(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nms[idx + i] = this.nms[idx];
      }
    }
  }

  cpAdvOther(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.advother[idx + i] = this.advother[idx];
      }
    }
  }

  cpNHSenhancedServ(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsenhancedserv[idx + i] = this.nhsenhancedserv[idx];
      }
    }
  }

  cpNHSundries(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsundries[idx + i] = this.nhsundries[idx];
      }
    }
  }

  cpQualityPay(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.qualitypay[idx + i] = this.qualitypay[idx];
      }
    }
  }

  cpPharmacy(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.pharmacyaccscheme[idx + i] = this.pharmacyaccscheme[idx];
      }
    }
  }

  cpBuyingProfit(idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.buyingprofit[idx + i] = this.buyingprofit[idx];
      }
    }
  }

  copyData(j: number, idx: number) {
    if (idx == 2) {
      for (var i = 1; i <= this.noYear; i++) {
        this.YearVal[j][idx + i] = this.YearVal[j][idx];
      }
    }
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
    if (idx == 2) {
      let first = 0.0;
      for (var i = 0; i < 4; i++) {
        if (this.MonthPresc[i][idx + 1] != null)
          first += Number(this.MonthPresc[i][idx + 1]);
        else first += 0;
      }
      first = Number(first * 1.26);
      let second = 0.0;
      for (var i = 4; i < this.MonthPresc.length; i++) {
        if (this.MonthPresc[i][idx + 1] != null)
          second += Number(this.MonthPresc[i][idx + 1]);
        else second += 0;
      }
      second = Number(second * 1.27);
      saf = Math.round(first + second);
    }
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
    if (this.getSAF(idx) != null) total += this.getSAF(idx);
    if (this.getEstablishedPay(idx) != null)
      total += this.getEstablishedPay(idx);
    if (this.nhsother[idx] != null) total += Number(this.nhsother[idx]);
    return total;
  }

  getSubtotalAdv(idx: number): number {
    let total = 0;
    if (this.nms[idx] != null) total += Number(this.nms[idx]);
    if (idx < 3) total += this.getMUR(idx);
    else total += 0;
    if (this.transitionpay[idx] != null)
      total += Number(this.transitionpay[idx]);
    if (this.advother[idx] != null) total += Number(this.advother[idx]);
    return total;
  }

  getMUR(idx: number): number {
    let mur = 0;
    mur = Number(this.mur[idx]) * 28;
    return mur;
  }

  getTotalOTC(idx: number): number {
    let total = 0;
    if (this.zeroOTCSale[idx] != null) total += Number(this.zeroOTCSale[idx]);
    if (this.vatOTCSale[idx] != null) total += Number(this.vatOTCSale[idx]);
    return total;
  }

  getNHSSales(idx: number): number {
    let total = 0;
    if (this.getNHSSalesReimburse(idx) != null)
      total += this.getNHSSalesReimburse(idx);
    if (this.getSubtotalNHS(idx) != null) total += this.getSubtotalNHS(idx);
    if (this.getSubtotalAdv(idx) != null) total += this.getSubtotalAdv(idx);
    if (this.nhsenhancedserv[idx] != null)
      total += Number(this.nhsenhancedserv[idx]);
    if (this.nhsundries[idx] != null) total += Number(this.nhsundries[idx]);
    if (this.qualitypay[idx] != null) total += Number(this.qualitypay[idx]);
    if (this.pharmacyaccscheme[idx] != null)
      total += Number(this.pharmacyaccscheme[idx]);
    if (this.buyingprofit[idx] != null) total += Number(this.buyingprofit[idx]);
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
    if (this.getGrandTotalNHS(idx) != null && this.getTotal(idx) != null)
      gross = this.getGrandTotalNHS(idx) - this.getTotal(idx);
    return gross;
  }

  getProfitLoss(idx: number): number {
    let total = 0;
    if (this.getGross(idx) != null) total += this.getGross(idx);
    if (this.Expense[idx] != null) total -= Number(this.Expense[idx]);
    return total;
  }

  saveExpense(idx) {
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
    if (idx == 1) {
      this.Expense[idx] = this.EntriesArray.value.reduce(
        (prev, next) => Number(prev) + Number(next),
        0
      );
    }
    if (idx == 2) {
      this.Expense[idx] = this.Entries2Array.value.reduce(
        (prev, next) => Number(prev) + Number(next),
        0
      );
      for (let j = 1; j <= this.noYear; j++) {
        this.Expense[idx + j] = this.Expense[2];
      }
    }
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
          /*this.gross = sched
            .map((a) => a.nhS_SalesSum)
            .reduce(function (a, b) {
              return a + b;
            });*/
          //this.Expense = +this.gross;
        });
      });
  }
}
