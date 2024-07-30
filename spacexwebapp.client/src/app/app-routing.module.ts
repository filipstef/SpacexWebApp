import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login.component';
import { AuthGuard } from './auth/auth.guard';
import { LatestLaunchesComponent } from './latest-launches/latest-launches.component';
import { UpcomingLaunchesComponent } from './upcoming-launches/upcoming-launches.component';
import { RegisterComponent } from './register/register.component';
import { PastLaunchesComponent } from './past-launches/past-launches.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },  
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'latest-launches', component: LatestLaunchesComponent, canActivate: [AuthGuard] },
  { path: 'upcoming-launches', component: UpcomingLaunchesComponent, canActivate: [AuthGuard] },
  { path: 'past-launches', component: PastLaunchesComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
