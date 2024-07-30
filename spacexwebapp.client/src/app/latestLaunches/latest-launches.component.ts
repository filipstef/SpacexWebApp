import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef } from '@angular/core';
import { LaunchService } from './latest-launches.service';
import { CustomLaunch } from '../shared-models/custom-launch';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Observable, of } from 'rxjs';
import { Rocket } from '../shared-models/rocket';
import { Crew } from '../shared-models/crew';

@Component({
  selector: 'app-latest-launches',
  templateUrl: './latest-launches.component.html',
  styleUrls: ['./latest-launches.component.css']
})
export class LatestLaunchesComponent implements OnInit, AfterViewInit {
  launch?: CustomLaunch;
  rocket?: Rocket;
  crew?: Crew[] = [];
  isLoading = true;
  videoUrl?: SafeResourceUrl;
  patchUrl?: SafeResourceUrl;
  asyncTabs!: Observable<any[]>;

  @ViewChild('overviewTemplate') overviewTemplate!: TemplateRef<any>;
  @ViewChild('rocketTemplate') rocketTemplate!: TemplateRef<any>;
  @ViewChild('crewTemplate') crewTemplate!: TemplateRef<any>;

  constructor(private launchService: LaunchService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.launchService.getLatestLaunches().subscribe((data: CustomLaunch) => {
      this.launch = data;
      this.rocket = data.rocket;

      this.crew = data.crew;

      this.patchUrl = data.links?.patch?.small;
      if (data.links && data.links.webcast) {
        this.videoUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.convertToEmbedUrl(data.links.webcast));
      }
      this.isLoading = false;
    },
      (error) => {
        console.error('Error fetching launch data:', error);
        this.isLoading = false;
      });
  }

  ngAfterViewInit(): void {
    this.asyncTabs = of([
      { label: 'Overview', template: this.overviewTemplate },
      { label: 'Rocket', template: this.rocketTemplate },
      { label: 'Crew', template: this.crewTemplate }
    ]);
  }

  convertToEmbedUrl(url: string | undefined): string {
    if (!url) {
      return '';
    }
    const videoId = this.extractVideoId(url);
    if (videoId) {
      return `https://www.youtube.com/embed/${videoId}`;
    }
    return '';
  }

  extractVideoId(url: string): string | null {
    const regex = /(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^"&?\/\s]{11})/i;
    const match = url.match(regex);
    return match ? match[1] : null;
  }
}
