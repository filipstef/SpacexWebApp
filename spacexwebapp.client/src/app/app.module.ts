import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth/auth.interceptor';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LatestLaunchesComponent } from './latestLaunches/latest-launches.component';
import { RegisterComponent } from './register/register.component';
import { UpcomingLaunchesComponent } from './upcoming-launches/upcoming-launches.component';
import { PastLaunchesComponent } from './past-launches/past-launches.component';
import { AngularMaterialModule } from './angular-material.module';

@NgModule({
  declarations: [
    AppComponent,    
    HomeComponent,
    NavMenuComponent,
    LoginComponent,   
    RegisterComponent,
    LatestLaunchesComponent,
    UpcomingLaunchesComponent,
    PastLaunchesComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,    
    ReactiveFormsModule,
    AngularMaterialModule
    
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
