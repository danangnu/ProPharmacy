import { HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-plc-user',
  templateUrl: './plc-user.component.html',
  styleUrls: ['./plc-user.component.css'],
})
export class PlcUserComponent implements OnInit {
  @Output() messageEvent = new EventEmitter<string>();
  registerForm: FormGroup;
  validationErrors: string[] = [];
  message = 'Hello!';

  constructor(
    private auth: AuthService,
    private memberService: MembersService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      email: [''],
      reportName: ['', Validators.required],
    });
  }

  async register() {
    await this.auth.idTokenClaims$.pipe(take(1)).subscribe((response) => {
      this.registerForm.patchValue({
        email: response.email,
      });
      this.auth.getAccessTokenSilently().subscribe((token) => {
        const headers = new HttpHeaders().set(
          'Authorization',
          `Bearer ${token}`
        );
        this.memberService
          .addReport(this.registerForm.value, headers)
          .subscribe(
            (response) => {
              this.router
                .navigateByUrl('/', { skipLocationChange: true })
                .then(() => this.router.navigate(['/plc-home']));
            },
            (error) => {
              this.validationErrors = error;
            }
          );
      });
    });
  }

  cancel() {
    this.messageEvent.emit(this.message);
  }
}
