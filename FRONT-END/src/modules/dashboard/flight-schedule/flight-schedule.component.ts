import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CalendarEvent} from 'angular-calendar';

@Component({
  selector: 'flight-schedule',
  templateUrl: 'flight-schedule.component.html',
  styleUrls: ['flight-schedule.component.scss']
})
export class FlightScheduleComponent {

  //#region Properties

  // Date which is being viewed.
  @Input('view-date')
  public viewDate: Date = new Date();

  // Locale of calendar
  public readonly locale = 'en';

  // List of events in calendar.
  @Input('events')
  public events: CalendarEvent[];

  public readonly view = 'month';

  //#endregion

  //#region Constructor

  public constructor() {
  }

  //#endregion

  //#region Methods

  //#endregion
}
