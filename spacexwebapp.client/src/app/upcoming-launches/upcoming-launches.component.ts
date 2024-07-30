import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomLaunch } from '../shared-models/custom-launch';
import { Launchpad } from '../shared-models/launchpad';
import { UpcomingLaunchesService } from './upcoming-launches.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-upcoming-launches',
  templateUrl: './upcoming-launches.component.html',
  styleUrl: './upcoming-launches.component.css'
})
export class UpcomingLaunchesComponent implements OnInit {
  displayedColumns: string[] = ['flight_number', 'name', 'date_utc', 'rocket', 'launchpad'];
  launches = new MatTableDataSource<CustomLaunch>();
  launchpad = new MatTableDataSource<Launchpad>();
  isLoading = true;

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private upcomingLaunchesService: UpcomingLaunchesService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(filterValue: string = '') {
    this.isLoading = true;
    this.upcomingLaunchesService.getUpcomingLaunches().subscribe(
      (data: CustomLaunch[]) => {
        this.launches.data = data.filter(launch =>
          launch.name.toLowerCase().includes(filterValue.toLowerCase())
        );
        this.launches.sort = this.sort;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching upcomming launches:', error);
        this.isLoading = false;
      }
    );
  }

  onFilterTextChanged(filterValue: string) {
    this.launches.filter = filterValue.trim().toLowerCase();
  }
}
