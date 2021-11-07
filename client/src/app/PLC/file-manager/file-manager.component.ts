import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { FileUploader } from 'ng2-file-upload';
import { NgxSpinnerService, Spinner } from 'ngx-spinner';
import { take } from 'rxjs/operators';
import { Docs } from 'src/app/_models/docs';
import { FileVersion } from 'src/app/_models/fileVersion';
import { User } from 'src/app/_models/user';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-file-manager',
  templateUrl: './file-manager.component.html',
  styleUrls: ['./file-manager.component.css']
})
export class FileManagerComponent implements OnInit {
  @Input() fileVersion: FileVersion;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.backendUrl;
  user: User;
  
  constructor(private route: ActivatedRoute,
              private auth: AuthService,
              private spinnerService: NgxSpinnerService) { }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(token => {
        this.uploader = new FileUploader({
          url: this.baseUrl + 'versions/add-docs/' + + this.route.snapshot.paramMap.get('id'),
          authToken: 'Bearer ' + token,
          isHTML5: true,
          allowedFileType: ['xls','pdf'],
          removeAfterUpload: true,
          autoUpload: false
        });

        this.uploader.onProgressAll = () => {
          this.spinnerService.show(undefined, {
            type: 'line-scale-party',
            bdColor: 'rgba(255,255,255,0)',
            color: '#333333'
          });
        };
  
        this.uploader.onAfterAddingFile = (file) => {
          file.withCredentials = false;
        };
  
        this.uploader.onCompleteItem = (item: any, status: any) => {
          this.spinnerService.hide();
          console.log('Uploaded File Details:', item?.file?.name);
        };
    });
  }
}
