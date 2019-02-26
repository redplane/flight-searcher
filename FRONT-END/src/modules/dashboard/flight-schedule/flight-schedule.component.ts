import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CalendarEvent} from 'angular-calendar';
import {CheapestFlightPriceViewModel} from '../../../view-models/cheapest-flight-price.view-model';

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

  // Map between key & flights.
  private _dateToFlightMap: { [key: string]: CheapestFlightPriceViewModel } = {};

  // Get map between key & flights.
  public get dateToFlightMap() {
    return this._dateToFlightMap;
  }

  @Input('available-flights')
  public set availableFlights(cheapestFlights: CheapestFlightPriceViewModel[]) {
    const dateToFlightMap: { [date: string]: CheapestFlightPriceViewModel } = {};

    if (cheapestFlights == null || !cheapestFlights.length) {
      this._dateToFlightMap = {};
      return;
    }

    for (const cheapestFlight of cheapestFlights) {
      const outboundTime = new Date(cheapestFlight.outboundTime);
      const key = this.loadDateFormat(outboundTime);
      dateToFlightMap[key] = cheapestFlight;
    }

    this._dateToFlightMap = dateToFlightMap;
  }

  //#endregion

  //#region Constructor

  public constructor() {
  }

  //#endregion

  //#region Methods

  // Load date format.
  protected loadDateFormat(date: Date): string {
    return `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`;
  }

  // Whether flight is available or not.
  public hasFlight(date: Date): boolean {

    if (this._dateToFlightMap == null) {
      return false;
    }

    const key = this.loadDateFormat(date);
    if (!this._dateToFlightMap[key]) {
      return false;
    }

    return true;
  }

  // Load flight information.
  public getFlight(date: Date): CheapestFlightPriceViewModel {

    if (!this.hasFlight(date)) {
      return null;
    }

    const key = this.loadDateFormat(date);
    return this._dateToFlightMap[key];
  }

  public ngOnPreviousMonthClicked(): void {
    const date = this.viewDate;
    date.setMonth(date.getMonth() - 1);
    this.viewDate = date;
  }

  //#endregion
}
