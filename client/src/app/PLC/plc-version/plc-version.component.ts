import { HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-plc-version',
  templateUrl: './plc-version.component.html',
  styleUrls: ['./plc-version.component.css']
})
export class PlcVersionComponent implements OnInit {
  @Output() messageEvent = new EventEmitter<string>();
  registerForm: FormGroup;
  validationErrors: string[] = [];
  message = 'Hello!';

  constructor(private memberService: MembersService,
              private router: Router,
              private auth: AuthService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      email: [''],
      versionName: ['', Validators.required]
    });
  }

  async register() {
    let i = 0;
    await this.auth.idTokenClaims$.subscribe(response => {
      if (i === 0) {
        this.registerForm.patchValue({
          email: response.email
        });
        this.auth.getAccessTokenSilently().subscribe(token => {
          const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
          this.memberService.addVersion(this.registerForm.value, headers).subscribe(response => {
            this.router.navigateByUrl('/', {skipLocationChange: true}).then(() =>
              this.router.navigate(['/plc-main']));
          }, error => {
            this.validationErrors = error;
          });
        });
      }
      i++;
    });
  }

  cancel() {
    this.messageEvent.emit(this.message);
  }
}
