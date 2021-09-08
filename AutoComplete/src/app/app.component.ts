import { Component } from '@angular/core';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  myControl = new FormControl();
  options1: any;
  option: any;
  options = [
    {
      "regionId": "7f2f06a8-7c88-4d3c-8a0a-8abbccf493b6",
      "regionName": "Africa"
    },
    {
      "regionId": "d7615971-e06b-40dd-94f2-15cdda6c34be",
      "regionName": "Asia Pacific"
    },
    {
      "regionId": "55459d41-58c2-4f29-a633-a82d4e6ebc46",
      "regionName": "Europe"
    },
    {
      "regionId": "88251d45-d2a7-4e10-a014-8cebaf586f5b",
      "regionName": "Latin America"
    },
    {
      "regionId": "13a67026-21a3-40a2-a31f-02b3ca7cbcbe",
      "regionName": "Middle East"
    },
    {
      "regionId": "2f15a0d5-46fc-42db-9d81-57251cab454b",
      "regionName": "North America"
    }
  ]
  filteredOptions: Observable<string[]>;

  constructor(private appService: AppService) {
    this.filteredOptions = this.myControl.valueChanges.pipe(startWith(''),
    map(value => this._filter(value))
  );
  }
  
  ngOnInit() {
  //  this.options = this.appService.getList();
    console.log('this.options', this.options);
    this.options1 = this.options.map(value=> value.regionName)
    console.log('this.options1', this.options1);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options1.filter(option => option.toLowerCase().includes(filterValue));
  }

  
}