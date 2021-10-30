import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { FileUploader } from 'ng2-file-upload';
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
              private auth: AuthService) { }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.auth.getAccessTokenSilently().pipe(take(1)).subscribe(response => {
      this.uploader = new FileUploader({
        url: this.baseUrl + 'users/add-file/' + this.route.snapshot.paramMap.get('id'),
        authToken: 'Bearer ' + response,
        isHTML5: true,
        allowedFileType: ['xls'],
        removeAfterUpload: true,
        autoUpload: false
      });
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const doc: Docs = JSON.parse(response);
        this.fileVersion.documents.push(doc);
      }
    };
  }
}
