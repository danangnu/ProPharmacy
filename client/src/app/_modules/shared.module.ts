import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    FileUploadModule
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    ModalModule,
    TabsModule,
    FileUploadModule
  ]
})
export class SharedModule { }
