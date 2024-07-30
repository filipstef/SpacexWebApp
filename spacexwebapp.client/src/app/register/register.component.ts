import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn
} from '@angular/forms';
import { BaseFormComponent } from '../base-form.component';
import { RegisterRequest } from './register-request';
import { RegisterResult } from './register-result';
import { RegisterService } from './register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent 
  extends BaseFormComponent implements OnInit {
  title?: string;
  registerResult?: RegisterResult;
  constructor(
    private registerService: RegisterService,
    private router: Router,) {
    super();
  }
  ngOnInit() {
    this.form = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }
  onSubmit() {
    var registerRequest = <RegisterRequest>{};
    registerRequest.firstName = this.form.controls['firstName'].value;
    registerRequest.lastName = this.form.controls['lastName'].value;
    registerRequest.email = this.form.controls['email'].value;
    registerRequest.password = this.form.controls['password'].value;

    this.registerService
      .register(registerRequest)
      .subscribe({
        next: (result) => {
          this.registerResult = result;
          if (result.success) {
            this.router.navigate(["/login"]);
          }
        },
        error: (error) => {
          if (error.status == 409) {
            this.registerResult = error.error;
          } else if (error.minLength) {
            this.registerResult = error.error;
          } else if (error.status != null) {
            this.registerResult = error.error;
          }
        }
      });
  }
}

