import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export abstract class BaseService{
  constructor(
    protected http: HttpClient) {
  }

  protected getUrl(url: string) {
    return environment.baseUrl + url;
  }
}
