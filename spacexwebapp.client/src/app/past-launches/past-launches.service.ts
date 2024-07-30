import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { CustomLaunch } from '../shared-models/custom-launch';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class PastLaunchesService extends BaseService {

  constructor(
    http: HttpClient) {
    super(http);
  }

  private url = this.getUrl("api/Launches/Past");

  getPastLaunches(): Observable<CustomLaunch[]> {
    return this.http.get<CustomLaunch[]>(this.url).pipe(
      map(response => response.map(item => ({
        flight_number: item.flight_number,
        name: item.name,
        date_utc: item.date_utc,
        rocket: item.rocket,
        launchpad: item.launchpad,
        success: item.success,
        links: item.links,
      } as CustomLaunch)))
    );
  }
}
