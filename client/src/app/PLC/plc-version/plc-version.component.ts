import { HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { MembersService } from 'src/app/_services/members.service';
import { VersionsService } from 'src/app/_services/versions.service';

@Component({
  selector: 'app-plc-version',
  templateUrl: './plc-version.component.html',
  styleUrls: ['./plc-version.component.css'],
})
export class PlcVersionComponent implements OnInit {
  @Output() messageEvent = new EventEmitter<string>();
  registerForm: FormGroup;
  validationErrors: string[] = [];
  message = 'Hello!';
  childId: number;

  constructor(
    private versionService: VersionsService,
    private router: Router,
    private route: ActivatedRoute,
    private auth: AuthService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      // tslint:disable-next-line: no-string-literal
      this.childId = Number(params.get('id'));
      // tslint:disable-next-line: no-string-literal
      console.log(params.get('id'));
    });
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      email: [''],
      versionName: ['', Validators.required],
    });
  }

  async register() {
    let i = 0;
    await this.auth.idTokenClaims$.subscribe((response) => {
      if (i === 0) {
        this.registerForm.patchValue({
          email: response.email,
        });
        this.auth.getAccessTokenSilently().subscribe((token) => {
          const headers = new HttpHeaders().set(
            'Authorization',
            `Bearer ${token}`
          );
          this.versionService
            .addVersion(this.childId, this.registerForm.value, headers)
            .subscribe(
              (response) => {
                this.router
                  .navigateByUrl('/', { skipLocationChange: true })
                  .then(() => this.router.navigate(['/plc-main/' + this.childId]));
              },
              (error) => {
                this.validationErrors = error;
              }
            );
        });
      }
      i++;
    });
  }

  cancel() {
    this.messageEvent.emit(this.message);
  }
}
