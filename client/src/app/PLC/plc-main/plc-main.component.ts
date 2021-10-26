import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-plc-main',
  templateUrl: './plc-main.component.html',
  styleUrls: ['./plc-main.component.css']
})
export class PlcMainComponent implements OnInit {
  files: Filedet[] = [];

  constructor() {
    this.files = [
      {name: 'Report 3', date: '30 September 2021'},
      {name: 'Report 2', date: '31 August 2021'},
      {name: 'Report 1', date: '31 July 2021'}
    ];
   }

  ngOnInit(): void {
  }

  removeItem() {
    this.files = [
      {name: 'Report 3', date: '30 September 2021'},
      {name: 'Report 2', date: '31 August 2021'}
    ];
  }
}

interface Filedet {
  name: string;
  date: string;
}
