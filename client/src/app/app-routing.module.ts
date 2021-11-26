import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './members/profile/profile.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { HomeComponent } from './home/home.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { PlcMainComponent } from './PLC/plc-main/plc-main.component';
import { PlcVersionComponent } from './PLC/plc-version/plc-version.component';
import { PlcReportComponent } from './plc/plc-report/plc-report.component';
import { PlcHomeComponent } from './PLC/plc-home/plc-home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'profile', component: ProfileComponent },
      { path: 'plc-main/:id', component: PlcMainComponent },
      { path: 'plc-home', component: PlcHomeComponent },
      { path: 'plc-version', component: PlcVersionComponent },
      { path: 'plc-report/:id', component: PlcReportComponent },
    ],
  },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
