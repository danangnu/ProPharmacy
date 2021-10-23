import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './members/profile/profile.component';
import { AuthGuard } from '@auth0/auth0-angular';

const routes: Routes = [
  {
    path: 'profile', component: ProfileComponent, canActivate: [AuthGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
