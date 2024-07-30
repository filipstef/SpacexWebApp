import { HttpClient } from "@angular/common/http";
import { RegisterRequest } from "./register-request";
import { Observable } from "rxjs";
import { RegisterResult } from "./register-result";
import { environment } from './../../environments/environment';
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root',
})

export class RegisterService{
  constructor(protected http: HttpClient) { }

  register(item: RegisterRequest): Observable<RegisterResult> {
    var url = environment.baseUrl + "api/Account/Register";
    return this.http.post<RegisterResult>(url, item);
  }
}
