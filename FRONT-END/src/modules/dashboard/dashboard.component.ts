import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CalendarEvent} from 'angular-calendar';

@Component({
  selector: 'dashboard',
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss']
})

export class DashboardComponent {

  //#region Properties
  public events: CalendarEvent[] = [
    {
      title: '1222',
      id: 1,
      start: new Date(2019, 2, 26),
      end: new Date(2019, 2, 26)
    }
  ];

  //#endregion

  //#region Constructor

  public constructor() {

  }

  //#endregion
}
