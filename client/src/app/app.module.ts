import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginButtonComponent } from './members/login-button/login-button.component';
import { NavComponent } from './nav/nav.component';
import { NavMainComponent } from './nav-main/nav-main.component';
import { LogoutButtonComponent } from './members/logout-button/logout-button.component';
import { ProfileComponent } from './members/profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    LoginButtonComponent,
    NavComponent,
    NavMainComponent,
    LogoutButtonComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot({
      ...env.auth,
    }),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
