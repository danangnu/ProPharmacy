import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginButtonComponent } from './members/login-button/login-button.component';
import { NavComponent } from './nav/nav.component';
import { NavMainComponent } from './nav-main/nav-main.component';
import { LogoutButtonComponent } from './members/logout-button/logout-button.component';
import { ProfileComponent } from './members/profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PlcMainComponent } from './PLC/plc-main/plc-main.component';
import { PlcVersionComponent } from './PLC/plc-version/plc-version.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { PlcReportComponent } from './plc/plc-report/plc-report.component';
import { FileManagerComponent } from './plc/file-manager/file-manager.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginButtonComponent,
    NavComponent,
    NavMainComponent,
    LogoutButtonComponent,
    ProfileComponent,
    HomeComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    PlcMainComponent,
    PlcVersionComponent,
    TextInputComponent,
    PlcReportComponent,
    FileManagerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot({
      ...env.auth,
      httpInterceptor: {
        allowedList: ['${env.dev.apiUrl}/api/'],
      },
    }),
    BrowserAnimationsModule,
    NgxSpinnerModule,
    FormsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
