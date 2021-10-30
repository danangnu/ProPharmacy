import { Component, OnInit } from '@angular/core';
import { FileVersion } from 'src/app/_models/fileVersion';

@Component({
  selector: 'app-plc-report',
  templateUrl: './plc-report.component.html',
  styleUrls: ['./plc-report.component.css']
})
export class PlcReportComponent implements OnInit {
  fileVersion: FileVersion;
  
  constructor() { }

  ngOnInit(): void {
  }

}
