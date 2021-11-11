import { HttpHeaders } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
import { FileVersion } from 'src/app/_models/fileVersion';
import { PrescriptionReport } from 'src/app/_models/prescriptionReport';
import { SchedulePaymentReport } from 'src/app/_models/schedulePaymentReport';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import { SchedulePaymentService } from 'src/app/_services/schedule-payment.service';
import { jqxPivotGridComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxpivotgrid';

@Component({
  selector: 'app-plc-report',
  templateUrl: './plc-report.component.html',
  styleUrls: ['./plc-report.component.css']
})
export class PlcReportComponent implements AfterViewInit {
  fileVersion: FileVersion;
  prescriptionReport: PrescriptionReport[] = [];
  schedulePaymentReports: SchedulePaymentReport[] = [];
  @ViewChild('pivotGridReference') myPivotGrid: jqxPivotGridComponent;
  pivotDataSource: [] = [];

  constructor(private auth: AuthService,
              private prescriptionService: PrescriptionService,
              private schedulePayService: SchedulePaymentService) {
                this.pivotDataSource = this.createPivotDataSource();
               }

  ngAfterViewInit(): void
  {
    this.myPivotGrid.createComponent(this.pivotGridSettings);
  }
  gOnInit() {
    this.loadPrescrptionReports();
    this.loadScheduleReports();
  }

  pivotGridSettings: jqwidgets.PivotGridOptions =
    {
        source: this.pivotDataSource,
        multipleSelectionEnabled: true,
        treeStyleRows: true,
        autoResize: false
    }

  createPivotDataSource(): any {
    // Prepare Sample Data
    let data = new Array();

    const countries =
    [
        'Germany', 'France', 'United States', 'Italy', 'Spain', 'Finland', 'Canada', 'Japan', 'Brazil', 'United Kingdom', 'China', 'India', 'South Korea', 'Romania', 'Greece'
    ];

    const dataPoints =
    [
        '2.25', '1.5', '3.0', '3.3', '4.5', '3.6', '3.8', '2.5', '5.0', '1.75', '3.25', '4.0'
    ];

    for (let i = 0; i < countries.length * 2; i++) {
        let row = {};
        const value = parseFloat(dataPoints[Math.round((Math.random() * 100)) % dataPoints.length]);
        row['country'] = countries[i % countries.length];
        row['value'] = value;
        data[i] = row;
    }

    // Create a Data Source and Data Adapter
    const source =
    {
        localdata: data,
        datatype: 'array',
        datafields:
        [
          { name: 'country', type: 'string' },
          { name: 'value', type: 'number' }
        ]
    };

    const dataAdapter = new jqx.dataAdapter(source);
    dataAdapter.dataBind();

    // Create a Pivot Data Source from the DataAdapter
    const pivotDataSource = new jqx.pivot(
        dataAdapter,
        {
            pivotValuesOnRows: false,
            rows: [{ dataField: 'country', width: 190 }],
            columns: [],
            values: 
            [
                { dataField: 'value', width: 200, 'function': 'min', text: 'cells left alignment', formatSettings: { align: 'left', prefix: '', decimalPlaces: 2 } },
                { dataField: 'value', width: 200, 'function': 'max', text: 'cells center alignment', formatSettings: { align: 'center', prefix: '', decimalPlaces: 2 } },
                { dataField: 'value', width: 200, 'function': 'average', text: 'cells right alignment', formatSettings: { align: 'right', prefix: '', decimalPlaces: 2 } }
            ]
        }
    );

    return pivotDataSource;
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
