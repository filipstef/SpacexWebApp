import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomLaunch } from '../shared-models/custom-launch';
import { PastLaunchesService } from './past-launches.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';



@Component({
  selector: 'app-past-launches',
  templateUrl: './past-launches.component.html',
  styleUrl: './past-launches.component.css'
})
export class PastLaunchesComponent implements OnInit {

  displayedColumns: string[] = ['flight_number', 'name', 'date_utc', 'rocket', 'launchpad', 'success'];
  launches = new MatTableDataSource<CustomLaunch>();
  isLoading = true;

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private pastLaunchesService: PastLaunchesService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(filterValue: string = '') {
    this.isLoading = true;
    this.pastLaunchesService.getPastLaunches().subscribe(
      (data: CustomLaunch[]) => {
        this.launches.data = data.filter(launch =>
          launch.name.toLowerCase().includes(filterValue.toLowerCase())
        );
        this.launches.sort = this.sort;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching past launches:', error);
        this.isLoading = false;
      }
    );
  }

  onFilterTextChanged(filterValue: string) {
    this.launches.filter = filterValue.trim().toLowerCase();
  }

}
