import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  NumberValueAccessor,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
import { FileVersion } from 'src/app/_models/fileVersion';
import { PrescriptionReport } from 'src/app/_models/prescriptionReport';
import { SchedulePaymentReport } from 'src/app/_models/schedulePaymentReport';
import { AccountService } from 'src/app/_services/account.service';
import { ExpenseSummaryService } from 'src/app/_services/expense-summary.service';
import { MurService } from 'src/app/_services/mur.service';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import { SalesSummaryService } from 'src/app/_services/sales-summary.service';
import { SchedulePaymentService } from 'src/app/_services/schedule-payment.service';
import { VersionsService } from 'src/app/_services/versions.service';

@Component({
  selector: 'app-plc-report',
  templateUrl: './plc-report.component.html',
  styleUrls: ['./plc-report.component.css'],
})
export class PlcReportComponent implements OnInit {
  fileVersion: FileVersion;
  prescriptionReport: PrescriptionReport[] = [];
  schedulePaymentReports: SchedulePaymentReport;
  EntriesArray: FormArray;
  Entries2Array: FormArray;
  LabelsArray: FormArray;
  registerForm: FormGroup;
  Expense: number[] = [];
  gross = 0;
  noYear = 5;
  decrease = 0;
  rateinflation = 2.48;
  startYear: number;
  YearA: number;
  Years: string[] = [];
  TOYears: string[] = [];
  AvgYears: string[] = [];
  InfYears: string[] = [];
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
  directorsalary: any[] = [];
  employeesalary: any[] = [];
  locumcost: any[] = [];
  othercost: any[] = [];
  rent: any[] = [];
  rates: any[] = [];
  utilities: any[] = [];
  telephone: any[] = [];
  repair: any[] = [];
  communication: any[] = [];
  leasing: any[] = [];
  insurance: any[] = [];
  proindemnity: any[] = [];
  computerit: any[] = [];
  recruitment: any[] = [];
  registrationfee: any[] = [];
  marketing: any[] = [];
  travel: any[] = [];
  entertainment: any[] = [];
  transport: any[] = [];
  accountancy: any[] = [];
  banking: any[] = [];
  interest: any[] = [];
  otherexpense: any[] = [];
  amortalisation: any[] = [];
  depreciation: any[] = [];
  inflation: number[] = [];
  presctab: boolean;
  versetab: boolean;
  salestab: boolean;
  expensetab: boolean;
  murtab: boolean;

  constructor(
    private auth: AuthService,
    private prescriptionService: PrescriptionService,
    private versionService: VersionsService,
    private salessumService: SalesSummaryService,
    private schedulePayService: SchedulePaymentService,
    private expensesumService: ExpenseSummaryService,
    private accountService: AccountService,
    private murService: MurService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.startYear = new Date().getFullYear();
    this.presctab = false;
    this.versetab = false;
    this.salestab = false;
    this.expensetab = false;
    this.murtab = false;
  }
  ngOnInit(): void {
    this.loadVersionSetting();
    this.loadPrescrptionReports();
    this.loadScheduleReports(this.startYear, 0);
    this.loadScheduleReports(Number(Number(this.startYear) + 1), 1);
    this.loadSalesSummary(this.startYear, 0);
    this.loadSalesSummary(Number(Number(this.startYear) + 1), 1);
    this.loadExpenseSummary(this.startYear, 0);
    this.loadExpenseSummary(Number(Number(this.startYear) + 1), 1);
    this.loadMur(this.startYear, 0);
    this.loadMur(Number(Number(this.startYear) + 1), 1);
    this.initializeForm();
    this.mur.push(400);
    this.mur.push(400);
    this.YearVal[0][0] = 'OTC Sales';
    this.YearVal[1][0] = 'NHS Sales';
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
    const yr = Number(this.startYear) + 2;
    for (let i = 0; i < this.noYear - 1; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.AvgYears.push(
        'Projected Prescription volume for ' +
          yr.toString() +
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

    this.InfYears.push('');
    this.InfYears.push(
      this.startYear.toString() +
        '/' +
        (this.startYear + 1).toString().substring(2, 4)
    );

    this.TOYears.push(
      this.startYear.toString() +
        '/' +
        (this.startYear + 1).toString().substring(2, 4)
    );
    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = this.startYear;
      else this.YearA = this.YearA + 1;

      this.InfYears.push(
        (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4)
      );

      this.Years.push(
        'Year ' +
          (i + 1) +
          ' (' +
          (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4) +
          ')'
      );

      this.TOYears.push(
        'Year ' +
          (i + 1) +
          ' (' +
          (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4) +
          ')'
      );
    }
    this.initialValue();
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
    for (let i = 1; i < this.InfYears.length - (this.noYear - 1); i++) {
      for (let j = 0; j < 12; j++) {
        if (j > 2 && j < 12) {
          let m = '';
          //console.log(Number((Number(this.startYear) + i - 1).toString() + m));
          if (j < 9) m = '0' + Number(j + 1).toString();
          else m = Number(j + 1).toString();
          if (i == 1) {
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                this.prescriptionService
                  .getSummary(
                    Number((Number(this.startYear) + i - 1).toString() + m),
                    Number(this.route.snapshot.paramMap.get('id')),
                    headers
                  )
                  .subscribe((presc) => {
                    if (j == 3) {
                      if (presc != undefined) {
                        this.presctab = true;
                        this.MonthPresc[0][0] = presc.prescItems;
                        this.MonthPresc[0][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[0][0] = 0;
                        this.MonthPresc[0][1] = 0;
                      }
                    }
                    if (j == 4) {
                      if (presc != undefined) {
                        this.MonthPresc[1][0] = presc.prescItems;
                        this.MonthPresc[1][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[1][0] = 0;
                        this.MonthPresc[1][1] = 0;
                      }
                    }
                    if (j == 5) {
                      if (presc != undefined) {
                        this.MonthPresc[2][0] = presc.prescItems;
                        this.MonthPresc[2][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[2][0] = 0;
                        this.MonthPresc[2][1] = 0;
                      }
                    }
                    if (j == 6) {
                      if (presc != undefined) {
                        this.MonthPresc[3][0] = presc.prescItems;
                        this.MonthPresc[3][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[3][0] = 0;
                        this.MonthPresc[3][1] = 0;
                      }
                    }
                    if (j == 7) {
                      if (presc != undefined) {
                        this.MonthPresc[4][0] = presc.prescItems;
                        this.MonthPresc[4][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[4][0] = 0;
                        this.MonthPresc[4][1] = 0;
                      }
                    }
                    if (j == 8) {
                      if (presc != undefined) {
                        this.MonthPresc[5][0] = presc.prescItems;
                        this.MonthPresc[5][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[5][0] = 0;
                        this.MonthPresc[5][1] = 0;
                      }
                    }
                    if (j == 9) {
                      if (presc != undefined) {
                        this.MonthPresc[6][0] = presc.prescItems;
                        this.MonthPresc[6][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[6][0] = 0;
                        this.MonthPresc[6][1] = 0;
                      }
                    }
                    if (j == 10) {
                      if (presc != undefined) {
                        this.MonthPresc[7][0] = presc.prescItems;
                        this.MonthPresc[7][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[7][0] = 0;
                        this.MonthPresc[7][1] = 0;
                      }
                    }
                    if (j == 11) {
                      if (presc != undefined) {
                        this.MonthPresc[8][0] = presc.prescItems;
                        this.MonthPresc[8][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[8][0] = 0;
                        this.MonthPresc[8][1] = 0;
                      }
                    }
                  });
              });
          } else {
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                this.prescriptionService
                  .getSummary(
                    Number((Number(this.startYear) + i - 1).toString() + m),
                    Number(this.route.snapshot.paramMap.get('id')),
                    headers
                  )
                  .subscribe((presc) => {
                    if (j == 3) {
                      if (presc != undefined) {
                        this.presctab = true;
                        this.MonthPresc[0][2] = presc.prescItems;
                        this.MonthPresc[0][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[0][2] = 0;
                        this.MonthPresc[0][3] = 0;
                      }
                    }
                    if (j == 4) {
                      if (presc != undefined) {
                        this.MonthPresc[1][2] = presc.prescItems;
                        this.MonthPresc[1][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[1][2] = 0;
                        this.MonthPresc[1][3] = 0;
                      }
                    }
                    if (j == 5) {
                      if (presc != undefined) {
                        this.MonthPresc[2][2] = presc.prescItems;
                        this.MonthPresc[2][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[2][2] = 0;
                        this.MonthPresc[2][3] = 0;
                      }
                    }
                    if (j == 6) {
                      if (presc != undefined) {
                        this.MonthPresc[3][2] = presc.prescItems;
                        this.MonthPresc[3][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[3][2] = 0;
                        this.MonthPresc[3][3] = 0;
                      }
                    }
                    if (j == 7) {
                      if (presc != undefined) {
                        this.MonthPresc[4][2] = presc.prescItems;
                        this.MonthPresc[4][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[4][2] = 0;
                        this.MonthPresc[4][3] = 0;
                      }
                    }
                    if (j == 8) {
                      if (presc != undefined) {
                        this.MonthPresc[5][2] = presc.prescItems;
                        this.MonthPresc[5][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[5][2] = 0;
                        this.MonthPresc[5][3] = 0;
                      }
                    }
                    if (j == 9) {
                      if (presc != undefined) {
                        this.MonthPresc[6][2] = presc.prescItems;
                        this.MonthPresc[6][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[6][2] = 0;
                        this.MonthPresc[6][3] = 0;
                      }
                    }
                    if (j == 10) {
                      if (presc != undefined) {
                        this.MonthPresc[7][2] = presc.prescItems;
                        this.MonthPresc[7][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[7][2] = 0;
                        this.MonthPresc[7][3] = 0;
                      }
                    }
                    if (j == 11) {
                      if (presc != undefined) {
                        this.MonthPresc[8][2] = presc.prescItems;
                        this.MonthPresc[8][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[8][2] = 0;
                        this.MonthPresc[8][3] = 0;
                      }
                    }
                  });
              });
          }
        } else {
          let m = '0' + Number(j + 1).toString();
          if (i == 1) {
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                this.prescriptionService
                  .getSummary(
                    Number((Number(this.startYear) + i).toString() + m),
                    Number(this.route.snapshot.paramMap.get('id')),
                    headers
                  )
                  .subscribe((presc) => {
                    if (j == 0) {
                      if (presc != undefined) {
                        this.presctab = true;
                        this.MonthPresc[9][0] = presc.prescItems;
                        this.MonthPresc[9][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[9][0] = 0;
                        this.MonthPresc[9][1] = 0;
                      }
                    }
                    if (j == 1) {
                      if (presc != undefined) {
                        this.MonthPresc[10][0] = presc.prescItems;
                        this.MonthPresc[10][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[10][0] = 0;
                        this.MonthPresc[10][1] = 0;
                      }
                    }
                    if (j == 2) {
                      if (presc != undefined) {
                        this.MonthPresc[11][0] = presc.prescItems;
                        this.MonthPresc[11][1] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[11][0] = 0;
                        this.MonthPresc[11][1] = 0;
                      }
                    }
                  });
              });
          } else {
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                this.prescriptionService
                  .getSummary(
                    Number((Number(this.startYear) + i).toString() + m),
                    Number(this.route.snapshot.paramMap.get('id')),
                    headers
                  )
                  .subscribe((presc) => {
                    if (j == 0) {
                      if (presc != undefined) {
                        this.presctab = true;
                        this.MonthPresc[9][2] = presc.prescItems;
                        this.MonthPresc[9][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[9][2] = 0;
                        this.MonthPresc[9][3] = 0;
                      }
                    }
                    if (j == 1) {
                      if (presc != undefined) {
                        this.MonthPresc[10][2] = presc.prescItems;
                        this.MonthPresc[10][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[10][2] = 0;
                        this.MonthPresc[10][3] = 0;
                      }
                    }
                    if (j == 2) {
                      if (presc != undefined) {
                        this.MonthPresc[11][2] = presc.prescItems;
                        this.MonthPresc[11][3] = presc.prescAvgItem;
                      } else {
                        this.MonthPresc[11][2] = 0;
                        this.MonthPresc[11][3] = 0;
                      }
                    }
                  });
              });
          }
        }
        //if (i == 0 || i == 2) this.MonthPresc[j][i] = 9000;
        //if (i == 1 || i == 3) this.MonthPresc[j][i] = 8;
      }
    }
    this.inflation.push(0.02);
    this.inflation.push(0.03);
    this.inflation.push(0.04);
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
    this.TOYears = [];
    this.InfYears = [];
    this.InfYears.push('');
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
    this.InfYears.push(
      this.startYear.toString() +
        '/' +
        (Number(this.startYear) + 1).toString().substring(2, 4)
    );
    this.TOYears.push(
      this.startYear.toString() +
        '/' +
        (this.startYear + 1).toString().substring(2, 4)
    );
    const yr = Number(this.startYear) + 2;
    for (let i = 0; i < this.noYear - 1; i++) {
      if (i === 0) this.YearA = Number(this.startYear);
      else this.YearA += 1;
      this.AvgYears.push(
        'Projected Prescription volume for ' +
          yr.toString() +
          '/' +
          (Number(this.YearA) + 3).toString().substring(2, 4)
      );
    }

    for (let i = 0; i < this.noYear; i++) {
      if (i === 0) this.YearA = Number(this.startYear);
      else this.YearA += 1;
      this.Years.push(
        'Year ' +
          (i + 1) +
          ' (' +
          (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4) +
          ')'
      );
      this.InfYears.push(
        (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4)
      );
      this.TOYears.push(
        'Year ' +
          (i + 1) +
          ' (' +
          (Number(this.YearA) + 1).toString() +
          '/' +
          (Number(this.YearA) + 2).toString().substring(2, 4) +
          ')'
      );
    }
    // this.loadScheduleReports(this.startYear, 0);
    // this.loadScheduleReports(Number(Number(this.startYear) + 1), 1);
    // this.initialValue();
  }

  getTotal(idx: number): number {
    let total = 0;
    if (this.getTotalOTC(idx - 1) != null)
      total += Math.round(this.getTotalOTC(idx - 1) * 0.85);
    if (this.getNHSSalesReimburse(idx) != null)
      total += Math.round(this.getNHSSalesReimburse(idx) * 0.8);
    return total;
  }

  cpZeroOTCSale(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.zeroOTCSale[idx + i] = this.zeroOTCSale[idx];
      }
    }
  }

  percentZeroOTCSale(idx: number): number {
    let total = 0;
    if (this.zeroOTCSale[idx] != null)
      total = Math.round(
        (this.zeroOTCSale[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpVatOTCSale(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.vatOTCSale[idx + i] = this.vatOTCSale[idx];
      }
    }
  }

  percentVatOTCSale(idx: number): number {
    let total = 0;
    if (this.vatOTCSale[idx] != null)
      total = Math.round(
        (this.vatOTCSale[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  percentNhsEnhancedServ(idx: number): number {
    let total = 0;
    if (this.nhsenhancedserv[idx + 1] != null)
      total = Math.round(
        (this.nhsenhancedserv[idx + 1] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  percentNhSundries(idx: number): number {
    let total = 0;
    if (this.nhsundries[idx + 1] != null)
      total = Math.round(
        (this.nhsundries[idx + 1] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  percentBuyingProfit(idx: number): number {
    let total = 0;
    if (this.getBuyingProfit(idx + 1) != null)
      total = Math.round(
        (this.getBuyingProfit(idx + 1) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpNhsOther(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsother[idx + i] = this.nhsother[idx];
      }
    }
  }

  cpNms(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nms[idx + i] = this.nms[idx];
      }
    }
  }

  cpAdvOther(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.advother[idx + i] = this.advother[idx];
      }
    }
  }

  cpNHSenhancedServ(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsenhancedserv[idx + i] = this.nhsenhancedserv[idx];
      }
    }
  }

  cpNHSundries(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.nhsundries[idx + i] = this.nhsundries[idx];
      }
    }
  }

  cpTransitionalPay(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.transitionpay[idx + i] = this.transitionpay[idx];
      }
    }
  }

  cpQualityPay(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.qualitypay[idx + i] = this.qualitypay[idx];
      }
    }
  }

  cpPharmacy(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.pharmacyaccscheme[idx + i] = this.pharmacyaccscheme[idx];
      }
    }
  }

  cpBuyingProfit(idx: number) {
    if (idx == 1) {
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

  cpDirectorSalary(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.directorsalary[idx + i] = this.directorsalary[idx];
      }
    }
  }

  percentDirectorSalary(idx: number): number {
    let total = 0;
    if (this.directorsalary[idx] != null)
      total = Math.round(
        (this.directorsalary[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpEmployeeSalary(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.employeesalary[idx + i] = this.employeesalary[idx];
      }
    }
  }

  percentEmployeeSalary(idx: number): number {
    let total = 0;
    if (this.employeesalary[idx] != null)
      total = Math.round(
        (this.employeesalary[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpLocumCost(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.locumcost[idx + i] = this.locumcost[idx];
      }
    }
  }

  percentLocumCost(idx: number): number {
    let total = 0;
    if (this.locumcost[idx] != null)
      total = Math.round(
        (this.locumcost[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpOtherCost(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.othercost[idx + i] = this.othercost[idx];
      }
    }
  }

  percentOtherCost(idx: number): number {
    let total = 0;
    if (this.othercost[idx] != null)
      total = Math.round(
        (this.othercost[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpRent(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.rent[idx + i] = this.rent[idx];
      }
    }
  }

  percentRent(idx: number): number {
    let total = 0;
    if (this.rent[idx] != null)
      total = Math.round(
        (this.rent[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpRates(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.rates[idx + i] = this.rates[idx];
      }
    }
  }

  percentRates(idx: number): number {
    let total = 0;
    if (this.rates[idx] != null)
      total = Math.round(
        (this.rates[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpUtilities(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.utilities[idx + i] = this.utilities[idx];
      }
    }
  }

  percentUtilities(idx: number): number {
    let total = 0;
    if (this.utilities[idx] != null)
      total = Math.round(
        (this.utilities[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpTelephone(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.telephone[idx + i] = this.telephone[idx];
      }
    }
  }

  percentTelephone(idx: number): number {
    let total = 0;
    if (this.telephone[idx] != null)
      total = Math.round(
        (this.telephone[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpRepair(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.repair[idx + i] = this.repair[idx];
      }
    }
  }

  percentRepair(idx: number): number {
    let total = 0;
    if (this.repair[idx] != null)
      total = Math.round(
        (this.repair[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpCommunication(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.communication[idx + i] = this.communication[idx];
      }
    }
  }

  percentCommunication(idx: number): number {
    let total = 0;
    if (this.communication[idx] != null)
      total = Math.round(
        (this.communication[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpLeasing(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.leasing[idx + i] = this.leasing[idx];
      }
    }
  }

  percentLeasing(idx: number): number {
    let total = 0;
    if (this.leasing[idx] != null)
      total = Math.round(
        (this.leasing[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpInsurance(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.insurance[idx + i] = this.insurance[idx];
      }
    }
  }

  percentInsurance(idx: number): number {
    let total = 0;
    if (this.insurance[idx] != null)
      total = Math.round(
        (this.insurance[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpProindemnity(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.proindemnity[idx + i] = this.proindemnity[idx];
      }
    }
  }

  percentProindemnity(idx: number): number {
    let total = 0;
    if (this.proindemnity[idx] != null)
      total = Math.round(
        (this.proindemnity[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpComputerit(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.computerit[idx + i] = this.computerit[idx];
      }
    }
  }

  percentComputerit(idx: number): number {
    let total = 0;
    if (this.computerit[idx] != null)
      total = Math.round(
        (this.computerit[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpRecruitment(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.recruitment[idx + i] = this.recruitment[idx];
      }
    }
  }

  percentRecruitment(idx: number): number {
    let total = 0;
    if (this.recruitment[idx] != null)
      total = Math.round(
        (this.recruitment[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpRegistrationfee(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.registrationfee[idx + i] = this.registrationfee[idx];
      }
    }
  }

  percentRegistrationfee(idx: number): number {
    let total = 0;
    if (this.registrationfee[idx] != null)
      total = Math.round(
        (this.registrationfee[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpMarketing(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.marketing[idx + i] = this.marketing[idx];
      }
    }
  }

  percentMarketing(idx: number): number {
    let total = 0;
    if (this.marketing[idx] != null)
      total = Math.round(
        (this.marketing[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpTravel(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.travel[idx + i] = this.travel[idx];
      }
    }
  }

  percentTravel(idx: number): number {
    let total = 0;
    if (this.travel[idx] != null)
      total = Math.round(
        (this.travel[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpEntertainment(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.entertainment[idx + i] = this.entertainment[idx];
      }
    }
  }

  percentEntertainment(idx: number): number {
    let total = 0;
    if (this.entertainment[idx] != null)
      total = Math.round(
        (this.entertainment[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpTransport(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.transport[idx + i] = this.transport[idx];
      }
    }
  }

  percentTransport(idx: number): number {
    let total = 0;
    if (this.transport[idx] != null)
      total = Math.round(
        (this.transport[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpAccountancy(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.accountancy[idx + i] = this.accountancy[idx];
      }
    }
  }

  percentAccountancy(idx: number): number {
    let total = 0;
    if (this.accountancy[idx] != null)
      total = Math.round(
        (this.accountancy[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpBanking(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.banking[idx + i] = this.banking[idx];
      }
    }
  }

  percentBanking(idx: number): number {
    let total = 0;
    if (this.banking[idx] != null)
      total = Math.round(
        (this.banking[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpInterest(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.interest[idx + i] = this.interest[idx];
      }
    }
  }

  percentInterest(idx: number): number {
    let total = 0;
    if (this.interest[idx] != null)
      total = Math.round(
        (this.interest[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpOtherexpense(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.otherexpense[idx + i] = this.otherexpense[idx];
      }
    }
  }

  percentOtherexpense(idx: number): number {
    let total = 0;
    if (this.otherexpense[idx] != null)
      total = Math.round(
        (this.otherexpense[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpAmortalisation(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.amortalisation[idx + i] = this.amortalisation[idx];
      }
    }
  }

  percentAmortalisation(idx: number): number {
    let total = 0;
    if (this.amortalisation[idx] != null)
      total = Math.round(
        (this.amortalisation[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  cpDepreciation(idx: number) {
    if (idx == 1) {
      for (var i = 1; i <= this.noYear; i++) {
        this.depreciation[idx + i] = this.depreciation[idx];
      }
    }
  }

  percentDepreciation(idx: number): number {
    let total = 0;
    if (this.depreciation[idx] != null)
      total = Math.round(
        (this.depreciation[idx] / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getTotalOperatingCost(idx: number): number {
    let total = 0;
    if (this.rent[idx] != null) total += Number(this.rent[idx]);
    if (this.rates[idx] != null) total += Number(this.rates[idx]);
    if (this.utilities[idx] != null) total += Number(this.utilities[idx]);
    if (this.telephone[idx] != null) total += Number(this.telephone[idx]);
    if (this.repair[idx] != null) total += Number(this.repair[idx]);
    if (this.communication[idx] != null)
      total += Number(this.communication[idx]);
    if (this.leasing[idx] != null) total += Number(this.leasing[idx]);
    if (this.insurance[idx] != null) total += Number(this.insurance[idx]);
    if (this.proindemnity[idx] != null) total += Number(this.proindemnity[idx]);
    if (this.computerit[idx] != null) total += Number(this.computerit[idx]);
    if (this.recruitment[idx] != null) total += Number(this.recruitment[idx]);
    if (this.registrationfee[idx] != null)
      total += Number(this.registrationfee[idx]);
    if (this.marketing[idx] != null) total += Number(this.marketing[idx]);
    if (this.travel[idx] != null) total += Number(this.travel[idx]);
    if (this.entertainment[idx] != null)
      total += Number(this.entertainment[idx]);
    if (this.transport[idx] != null) total += Number(this.transport[idx]);
    if (this.accountancy[idx] != null) total += Number(this.accountancy[idx]);
    if (this.banking[idx] != null) total += Number(this.banking[idx]);
    if (this.interest[idx] != null) total += Number(this.interest[idx]);
    if (this.otherexpense[idx] != null) total += Number(this.otherexpense[idx]);
    if (this.amortalisation[idx] != null)
      total += Number(this.amortalisation[idx]);
    if (this.depreciation[idx] != null) total += Number(this.depreciation[idx]);
    return total;
  }

  percentTotalOperatingCost(idx: number): number {
    let total = 0;
    if (this.getTotalOperatingCost(idx) != null)
      total = Math.round(
        (this.getTotalOperatingCost(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getTotalExpense(idx: number): number {
    let total = 0;
    if (this.getTotalPersonelCost(idx) != null)
      total += this.getTotalPersonelCost(idx);
    if (this.getTotalOperatingCost(idx) != null)
      total += this.getTotalOperatingCost(idx);
    return total;
  }

  calcInflation(idx: number, idy: number): number {
    let result = 0;
    result = this.getTotalExpense(0) * Math.pow(1 + this.inflation[idx], idy);
    return result;
  }

  calcInflation2(idx: number, idy: number): number {
    let result = 0;
    if (idx == 0 && idy == 1)
      result = Math.round(this.getGross(1) - this.calcInflation(idx, idy));
    else result = Math.round(this.getGross(2) - this.calcInflation(idx, idy));
    return result;
  }

  increaseExpense(): number {
    let incr = 0;
    incr =
      this.getTotalExpense(0) *
        Math.pow(Number(1 + this.rateinflation / 100), this.noYear) -
      this.getTotalExpense(0);
    return incr;
  }

  getExpenseAfterInfl(): number {
    let total = 0;
    total += this.getTotalExpense(0) + this.increaseExpense();
    return total;
  }

  getPLAfterInfl(): number {
    let total = 0;
    total += this.getGross(1) - this.getExpenseAfterInfl();
    return total;
  }

  getPLAfterInflPercent(): number {
    let total = 0;
    total += this.getExpenseAfterInfl() / this.getGross(1);
    return total;
  }

  getExpenseAfterInflPercent(): number {
    let total = 0;
    total += this.getExpenseAfterInfl() / this.getGrandTotalNHS(1);
    return total;
  }

  percentTotalExpense(idx: number): number {
    let total = 0;
    if (this.getTotalExpense(idx) != null)
      total = Math.round(
        (this.getTotalExpense(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getTotalItems(idx: number): number {
    let total = 0;
    for (var i = 0; i < this.MonthPresc.length; i++) {
      if (this.MonthPresc[i][idx] !== undefined)
        total += Number(this.MonthPresc[i][idx]);
      else total += 0;
    }
    if (idx == 1 || idx == 3) total = total / this.MonthPresc.length;
    if (idx > 3)
      total = Math.round(
        this.getTotalItems(2) * Math.pow(1 - this.decrease / 100, idx - 3)
      );
    return total;
  }

  getNHSSalesReimburse(idx: number): number {
    let nsr = 0;
    nsr = this.getTotalItems(idx - 1) * this.getTotalItems(idx);
    if (idx > 0) nsr = this.getTotalItems(idx) * this.getTotalItems(idx + 1);
    if (idx > 1) nsr = this.getNHSSalesReimburse(1);
    return nsr;
  }

  getBuyingProfit(idx: number): number {
    let bp = 0;
    if (this.getNHSSalesReimburse(idx) != 0)
      bp = Math.round(this.getNHSSalesReimburse(idx) * 0.12);
    return bp;
  }

  percentNHSSalesReimburse(idx: number): number {
    let total = 0;
    if (this.getNHSSalesReimburse(idx + 1) != null)
      total = Math.round(
        (this.getNHSSalesReimburse(idx + 1) / this.getGrandTotalNHS(idx + 1)) *
          100
      );
    return total;
  }

  getSAF(idx: number): number {
    let saf = 0;
    saf = Math.round(this.getTotalItems(idx) * 1.26);
    if (idx == 1) {
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
    if (idx > 1) saf = Math.round(this.getTotalItems(idx + 2) * 1.27);
    return saf;
  }

  getEstablishedPay(idx: number): number {
    let est = 0;
    if (idx > 0) idx += 1;
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

  percentSubtotalNHS(idx: number): number {
    let total = 0;
    if (this.getSubtotalNHS(idx) != null)
      total = Math.round(
        (this.getSubtotalNHS(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getSubtotalAdv(idx: number): number {
    let total = 0;
    if (this.nms[idx - 1] != null) total += Number(this.nms[idx - 1]);
    if (idx < 3) total += this.getMUR(idx - 1);
    else total += 0;
    if (this.transitionpay[idx - 1] != null)
      total += Number(this.transitionpay[idx - 1]);
    if (this.advother[idx - 1] != null) total += Number(this.advother[idx - 1]);
    return total;
  }

  percentSubtotalAdv(idx: number): number {
    let total = 0;
    if (this.getSubtotalAdv(idx + 1) != null)
      total = Math.round(
        (this.getSubtotalAdv(idx + 1) / this.getGrandTotalNHS(idx + 1)) * 100
      );
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

  percentTotalOTC(idx: number): number {
    let total = 0;
    if (this.getTotalOTC(idx) != null)
      total = Math.round(
        (this.getTotalOTC(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getNHSSales(idx: number): number {
    let total = 0;
    if (this.getNHSSalesReimburse(idx) != null)
      total += this.getNHSSalesReimburse(idx);
    if (this.getSubtotalNHS(idx - 1) != null)
      total += this.getSubtotalNHS(idx - 1);
    if (this.getSubtotalAdv(idx) != null) total += this.getSubtotalAdv(idx);
    if (this.nhsenhancedserv[idx - 1] != null)
      total += Number(this.nhsenhancedserv[idx - 1]);
    if (this.nhsundries[idx - 1] != null)
      total += Number(this.nhsundries[idx - 1]);
    if (this.qualitypay[idx - 1] != null)
      total += Number(this.qualitypay[idx - 1]);
    if (this.pharmacyaccscheme[idx - 1] != null)
      total += Number(this.pharmacyaccscheme[idx - 1]);
    if (this.getBuyingProfit(idx) != null)
      total += Number(this.getBuyingProfit(idx));
    return total;
  }

  percentNHSSales(idx: number): number {
    let total = 0;
    if (this.getNHSSales(idx + 1) != null)
      total = Math.round(
        (this.getNHSSales(idx + 1) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getGrandTotalNHS(idx: number): number {
    let total = 0;
    total += this.getTotalOTC(idx - 1);
    total += this.getNHSSales(idx);
    return total;
  }

  getGross(idx: number): number {
    let gross = 0;
    if (this.getGrandTotalNHS(idx) != null && this.getTotal(idx) != null)
      gross = this.getGrandTotalNHS(idx) - this.getTotal(idx);
    return gross;
  }

  getTotalPersonelCost(idx: number): number {
    let total = 0;
    if (this.directorsalary[idx] != null)
      total += Number(this.directorsalary[idx]);
    if (this.employeesalary[idx] != null)
      total += Number(this.employeesalary[idx]);
    if (this.locumcost[idx] != null) total += Number(this.locumcost[idx]);
    if (this.othercost[idx] != null) total += Number(this.othercost[idx]);
    return total;
  }

  percentTotalPersonelCost(idx: number): number {
    let total = 0;
    if (this.getTotalPersonelCost(idx) != null)
      total = Math.round(
        (this.getTotalPersonelCost(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  getProfitLoss(idx: number): number {
    let total = 0;
    if (this.getGross(idx) != null) total += this.getGross(idx);
    if (this.Expense[idx] != null) total -= Number(this.Expense[idx]);
    return total;
  }

  percentProfitLoss(idx: number): number {
    let total = 0;
    if (this.getProfitLoss(idx) != null)
      total = Math.round(
        (this.getProfitLoss(idx) / this.getGrandTotalNHS(idx + 1)) * 100
      );
    return total;
  }

  saveExpense() {
    let bar: any = [];
    let mur: any = [];
    let sales: any = [];
    let exp: any = [];
    let verset: any = [];
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        verset = {
          startYear: Number(this.startYear),
          noYear: Number(this.noYear),
          volumeDecrease: this.decrease,
          inflationRate: this.rateinflation,
        };
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        if (this.versetab) {
          this.versionService
            .updateVersionSetting(
              Number(this.route.snapshot.paramMap.get('id')),
              verset,
              headers
            )
            .subscribe(() => {});
        } else {
          this.versionService
            .addVersionSetting(
              Number(this.route.snapshot.paramMap.get('id')),
              verset,
              headers
            )
            .subscribe((verset) => {
              console.log(verset);
            });
        }
      });
    for (let i = 1; i < this.InfYears.length - (this.noYear - 1); i++) {
      for (let j = 0; j < 12; j++) {
        if (j > 2 && j < 12) {
          let m = '';
          if (j < 9) m = '0' + Number(j + 1).toString();
          else m = Number(j + 1).toString();
          if (i == 1)
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                bar = {
                  prescMonth: Number(
                    (Number(this.startYear) + i - 1).toString() + m
                  ),
                  prescItems: this.MonthPresc[j - 3][i - 1],
                  prescAvgItem: this.MonthPresc[j - 3][i],
                };
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                if (this.presctab) {
                  this.prescriptionService
                    .updateSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe(() => {});
                } else {
                  this.versionService
                    .addPrescSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe((presc) => {
                      console.log(presc);
                    });
                }
              });
          else
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                bar = {
                  prescMonth: Number(
                    (Number(this.startYear) + i - 1).toString() + m
                  ), // 202204 j = 3
                  prescItems: this.MonthPresc[j - 3][i],
                  prescAvgItem: this.MonthPresc[j - 3][i + 1],
                };
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                if (this.presctab) {
                  this.prescriptionService
                    .updateSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe(() => {});
                } else {
                  this.versionService
                    .addPrescSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe((presc) => {
                      console.log(presc);
                    });
                }
              });
        } else {
          let m = '0' + Number(j + 1).toString();
          if (i == 1)
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                bar = {
                  prescMonth: Number(
                    (Number(this.startYear) + i).toString() + m
                  ), //202201  j = 0
                  prescItems: this.MonthPresc[j + 9][i - 1],
                  prescAvgItem: this.MonthPresc[j + 9][i],
                };
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                if (this.presctab) {
                  this.prescriptionService
                    .updateSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe(() => {});
                } else {
                  this.versionService
                    .addPrescSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe((presc) => {
                      console.log(presc);
                    });
                }
              });
          else
            this.auth
              .getAccessTokenSilently()
              .pipe(take(1))
              .subscribe((token) => {
                bar = {
                  prescMonth: Number(
                    (Number(this.startYear) + i).toString() + m
                  ),
                  prescItems: this.MonthPresc[j + 9][i],
                  prescAvgItem: this.MonthPresc[j + 9][i + 1],
                };
                const headers = new HttpHeaders().set(
                  'Authorization',
                  `Bearer ${token}`
                );
                if (this.presctab) {
                  this.prescriptionService
                    .updateSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe(() => {});
                } else {
                  this.versionService
                    .addPrescSummary(
                      Number(this.route.snapshot.paramMap.get('id')),
                      bar,
                      headers
                    )
                    .subscribe((presc) => {
                      console.log(presc);
                    });
                }
              });
        }
      }
      this.auth
        .getAccessTokenSilently()
        .pipe(take(1))
        .subscribe((token) => {
          mur = {
            murYear: Number(
              (Number(this.startYear) + Number(i - 1)).toString()
            ),
            totalMur: this.mur[i - 1],
          };
          const headers = new HttpHeaders().set(
            'Authorization',
            `Bearer ${token}`
          );
          if (this.murtab) {
            this.murService
            .updateSummary(
              Number(this.route.snapshot.paramMap.get('id')),
              mur,
              headers
            )
            .subscribe(() => {
            });
          } else {
            this.versionService
            .addMur(
              Number(this.route.snapshot.paramMap.get('id')),
              mur,
              headers
            )
            .subscribe((mur) => {
              console.log(mur);
            });
          }
        });
      this.auth
        .getAccessTokenSilently()
        .pipe(take(1))
        .subscribe((token) => {
          sales = {
            salesYear: Number(
              (Number(this.startYear) + Number(i - 1)).toString()
            ),
            zeroRatedOTCSale: this.zeroOTCSale[i - 1],
            vATExclusiveOTCSale: this.vatOTCSale[i - 1],
          };
          const headers = new HttpHeaders().set(
            'Authorization',
            `Bearer ${token}`
          );
          if (this.salestab) {
            this.salessumService
              .updateSalesSummary(
                Number(this.route.snapshot.paramMap.get('id')),
                sales,
                headers
              )
              .subscribe((sale) => {
                console.log(sale);
              });
          } else {
            this.versionService
              .addSaleSummary(
                Number(this.route.snapshot.paramMap.get('id')),
                sales,
                headers
              )
              .subscribe((sale) => {
                console.log(sale);
              });
          }
        });

      this.auth
        .getAccessTokenSilently()
        .pipe(take(1))
        .subscribe((token) => {
          exp = {
            expYear: Number(
              (Number(this.startYear) + Number(i - 1)).toString()
            ),
            directorSalary: Number(this.directorsalary[i - 1]),
            employeeSalary: Number(this.employeesalary[i - 1]),
            locumCost: Number(this.locumcost[i - 1]),
            otherCost: Number(this.othercost[i - 1]),
            rent: Number(this.rent[i - 1]),
            rates: Number(this.rates[i - 1]),
            utilities: Number(this.utilities[i - 1]),
            telephone: Number(this.telephone[i - 1]),
            repair: Number(this.repair[i - 1]),
            communication: Number(this.communication[i - 1]),
            leasing: Number(this.leasing[i - 1]),
            insurance: Number(this.insurance[i - 1]),
            proIndemnity: Number(this.proindemnity[i - 1]),
            computerIt: Number(this.computerit[i - 1]),
            recruitment: Number(this.recruitment[i - 1]),
            registrationFee: Number(this.registrationfee[i - 1]),
            marketing: Number(this.marketing[i - 1]),
            travel: Number(this.travel[i - 1]),
            entertainment: Number(this.entertainment[i - 1]),
            transport: Number(this.transport[i - 1]),
            accountancy: Number(this.accountancy[i - 1]),
            banking: Number(this.banking[i - 1]),
            interest: Number(this.interest[i - 1]),
            otherExpense: Number(this.otherexpense[i - 1]),
            amortalisation: Number(this.amortalisation[i - 1]),
            depreciation: Number(this.depreciation[i - 1]),
          };
          const headers = new HttpHeaders().set(
            'Authorization',
            `Bearer ${token}`
          );
          if (this.expensetab) {
            this.expensesumService
            .updateExpenseSummary(
              Number(this.route.snapshot.paramMap.get('id')),
              exp,
              headers
            )
            .subscribe(() => {
            });
          } else {
            this.versionService
            .addExpSummary(
              Number(this.route.snapshot.paramMap.get('id')),
              exp,
              headers
            )
            .subscribe((exp) => {
              console.log(exp);
            });
          }
        });
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

  loadSalesSummary(year: number, idx: number) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.salessumService
          .getReport(
            year,
            Number(this.route.snapshot.paramMap.get('id')),
            headers
          )
          .subscribe((sched) => {
            if (sched != undefined) {
              this.salestab = true;
              this.zeroOTCSale[idx] = sched.zeroRatedOTCSale;
              this.vatOTCSale[idx] = sched.vatExclusiveOTCSale;
              this.cpZeroOTCSale(idx);
              this.cpVatOTCSale(idx);
            } else {
              this.zeroOTCSale[idx] = 0;
              this.vatOTCSale[idx] = 0;
              this.cpZeroOTCSale(idx);
              this.cpVatOTCSale(idx);
            }
          });
      });
  }

  loadMur(year: number, idx: number) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.murService
          .getSummary(
            year,
            Number(this.route.snapshot.paramMap.get('id')),
            headers
          )
          .subscribe((mur) => {
            if (mur != undefined) {
              this.murtab = true;
              this.mur[idx] = mur.totalMur;
            } else {
              this.mur[idx] = 400;
            }
          });
      });
  }

  loadScheduleReports(year: number, idx: number) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.schedulePayService
          .getReport(
            Number(this.route.snapshot.paramMap.get('id')),
            year,
            headers
          )
          .subscribe((sched) => {
            if (sched != undefined) {
              this.schedulePaymentReports = sched;
              this.nhsother[idx] = sched.total_Others;
              this.nms[idx] = sched.other_Fee_Medicine_Services;
              this.advother[idx] = sched.adv_Others;
              this.nhsenhancedserv[idx] = sched.enhanced_Services.toFixed(2);
              this.nhsundries[idx] = sched.total_Charges;
              this.transitionpay[idx] = sched.transitional_Pay;
              this.cpNhsOther(idx);
              this.cpNms(idx);
              this.cpAdvOther(idx);
              this.cpNHSenhancedServ(idx);
              this.cpNHSundries(idx);
              this.cpTransitionalPay(idx);
              
            } else {
              this.nhsother[idx] = 0;
              this.nms[idx] = 0;
              this.advother[idx] = 0;
              this.nhsenhancedserv[idx] = 0;
              this.nhsundries[idx] = 0;
              this.transitionpay[idx] = 0;
              this.cpNhsOther(idx);
              this.cpNms(idx);
              this.cpAdvOther(idx);
              this.cpNHSenhancedServ(idx);
              this.cpNHSundries(idx);
              this.cpTransitionalPay(idx);
            }
          });
      });
  }

  loadExpenseSummary(year: number, idx: number) {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.expensesumService
          .getReport(
            year,
            Number(this.route.snapshot.paramMap.get('id')),
            headers
          )
          .subscribe((exp) => {
            if (exp != undefined) {
              this.expensetab = true;
              this.directorsalary[idx] = exp.directorSalary;
              this.employeesalary[idx] = exp.employeeSalary;
              this.locumcost[idx] = exp.locumCost;
              this.othercost[idx] = exp.otherCost;
              this.rent[idx] = exp.rent;
              this.rates[idx] = exp.rates;
              this.utilities[idx] = exp.utilities;
              this.telephone[idx] = exp.telephone;
              this.repair[idx] = exp.repair;
              this.communication[idx] = exp.communication;
              this.leasing[idx] = exp.leasing;
              this.insurance[idx] = exp.insurance;
              this.proindemnity[idx] = exp.proIndemnity;
              this.computerit[idx] = exp.computerIt;
              this.recruitment[idx] = exp.recruitment;
              this.registrationfee[idx] = exp.registrationFee;
              this.marketing[idx] = exp.marketing;
              this.travel[idx] = exp.travel;
              this.entertainment[idx] = exp.entertainment;
              this.transport[idx] = exp.transport;
              this.accountancy[idx] = exp.accountancy;
              this.banking[idx] = exp.banking;
              this.interest[idx] = exp.interest;
              this.otherexpense[idx] = exp.otherExpense;
              this.amortalisation[idx] = exp.amortalisation;
              this.depreciation[idx] = exp.depreciation;
              this.cpDirectorSalary(idx);
              this.cpEmployeeSalary(idx);
              this.cpLocumCost(idx);
              this.cpOtherCost(idx);
              this.cpRent(idx);
              this.cpRates(idx);
              this.cpUtilities(idx);
              this.cpTelephone(idx);
              this.cpRepair(idx);
              this.cpCommunication(idx);
              this.cpLeasing(idx);
              this.cpInsurance(idx);
              this.cpProindemnity(idx);
              this.cpComputerit(idx);
              this.cpRecruitment(idx);
              this.cpRegistrationfee(idx);
              this.cpMarketing(idx);
              this.cpTravel(idx);
              this.cpEntertainment(idx);
              this.cpTransport(idx);
              this.cpAccountancy(idx);
              this.cpBanking(idx);
              this.cpInterest(idx);
              this.cpOtherexpense(idx);
              this.cpAmortalisation(idx);
              this.cpDepreciation(idx);
            } else {
              this.directorsalary[idx] = 0;
              this.employeesalary[idx] = 0;
              this.locumcost[idx] = 0;
              this.othercost[idx] = 0;
              this.rent[idx] = 0;
              this.rates[idx] = 0;
              this.utilities[idx] = 0;
              this.telephone[idx] = 0;
              this.repair[idx] = 0;
              this.communication[idx] = 0;
              this.leasing[idx] = 0;
              this.insurance[idx] = 0;
              this.proindemnity[idx] = 0;
              this.computerit[idx] = 0;
              this.recruitment[idx] = 0;
              this.registrationfee[idx] = 0;
              this.marketing[idx] = 0;
              this.travel[idx] = 0;
              this.entertainment[idx] = 0;
              this.transport[idx] = 0;
              this.accountancy[idx] = 0;
              this.banking[idx] = 0;
              this.interest[idx] = 0;
              this.otherexpense[idx] = 0;
              this.amortalisation[idx] = 0;
              this.depreciation[idx] = 0;
              this.cpDirectorSalary(idx);
              this.cpEmployeeSalary(idx);
              this.cpLocumCost(idx);
              this.cpOtherCost(idx);
              this.cpRent(idx);
              this.cpRates(idx);
              this.cpUtilities(idx);
              this.cpTelephone(idx);
              this.cpRepair(idx);
              this.cpCommunication(idx);
              this.cpLeasing(idx);
              this.cpInsurance(idx);
              this.cpProindemnity(idx);
              this.cpComputerit(idx);
              this.cpRecruitment(idx);
              this.cpRegistrationfee(idx);
              this.cpMarketing(idx);
              this.cpTravel(idx);
              this.cpEntertainment(idx);
              this.cpTransport(idx);
              this.cpAccountancy(idx);
              this.cpBanking(idx);
              this.cpInterest(idx);
              this.cpOtherexpense(idx);
              this.cpAmortalisation(idx);
              this.cpDepreciation(idx);
            }
          });
      });
  }

  loadVersionSetting() {
    this.auth
      .getAccessTokenSilently()
      .pipe(take(1))
      .subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.accountService
          .getSetting(Number(this.route.snapshot.paramMap.get('id')), headers)
          .subscribe((verset) => {
            if (verset != undefined) {
              this.versetab = true;
              this.startYear = verset.startYear;
              this.noYear = verset.noYear;
              this.decrease = verset.volumeDecrease;
              this.rateinflation = verset.inflationRate;
            } else {
              this.startYear = new Date().getFullYear();
              this.noYear = 5;
              this.decrease = 0;
              this.rateinflation = 2;
            }
          });
      });
  }
}
