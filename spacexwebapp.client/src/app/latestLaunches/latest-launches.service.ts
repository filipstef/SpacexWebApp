import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomLaunch } from '../shared-models/custom-launch';
import { map } from 'rxjs/operators';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root',
})

export class LaunchService extends BaseService {
  constructor(
    http: HttpClient) {
    super(http);
  }

  private url = this.getUrl("api/Launches/Latest")

  getLatestLaunches(): Observable<CustomLaunch> {
    return this.http.get<any>(this.url).pipe(
      map(response => ({
        flight_number: response.flight_number,
        name: response.name,
        date_utc: response.date_utc,
        rocket: response.rocket,
        launchpad: response.launchpad.full_name,
        success: response.success,
        links: response.links,
        crew: response.crew
      } as CustomLaunch))
    );
  }
}
