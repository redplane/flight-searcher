import {Component} from '@angular/core';
import {FlightRouteModel} from '../../../models/flight-route.model';
import {LoadCheapestFlightPriceViewModel} from '../../../view-models/load-cheapest-flight-price.view-model';
import {FormGroup, NgForm} from '@angular/forms';

@Component({
  selector: 'flight-search',
  templateUrl: 'flight-search.component.html',
  styleUrls: ['flight-search.component.scss']
})
export class FlightSearchComponent {

  //#region Properties

  // Time when user wants to depart.
  private _departureTime: Date | null;

  // Get time when user wants to depart.
  public get departureTime(): Date | null {
    return this._departureTime;
  }

  // Set time when user wants to depart.
  public set departureTime(time: Date) {
    this._departureTime = time;
  }

  // Available departure flight routes.
  public availableDepartureFlightRoutes: Array<FlightRouteModel>;

  // Available arrival flight route.
  public availableArrivalFlightRoutes: Array<FlightRouteModel>;

  // Condition for searching cheapest flight prices.
  public loadCheapestFlightPriceConditions: LoadCheapestFlightPriceViewModel;

  // Get departure flight route.
  public get departureFlightRoute(): string {
    return this.loadCheapestFlightPriceConditions.departure;
  }

  // Set departure flight route.
  public set departureFlightRoute(value: string) {
    this.loadCheapestFlightPriceConditions.departure = value;

    // Departure place is the same as the arrival.
    if (value && value.length && value === this.arrivalFlightRoute) {
      // Make arrival to null.
      this.arrivalFlightRoute = null;
    }
  }

  // Get arrival flight route.
  public get arrivalFlightRoute(): string {
    return this.loadCheapestFlightPriceConditions.arrival;
  }

  // Set arrival flight route
  public set arrivalFlightRoute(value: string) {
    this.loadCheapestFlightPriceConditions.arrival = value;

    // Arrival is the same as departure.
    if (value && value.length && value === this.departureFlightRoute) {
      this.departureFlightRoute = null;
    }
  }

  //#endregion

  //#region Constructor

  public constructor() {
    const availableFlightRoutes = new Array<FlightRouteModel>();
    availableFlightRoutes.push(new FlightRouteModel('KUL', 'KUL-sky'));
    availableFlightRoutes.push(new FlightRouteModel('SIN', 'SIN-sky'));
    availableFlightRoutes.push(new FlightRouteModel('SFO', 'SFO-sky'));
    this.availableDepartureFlightRoutes = Object.assign([], availableFlightRoutes);
    this.availableArrivalFlightRoutes = Object.assign([], availableFlightRoutes);

    // Initialize search condition.
    this.loadCheapestFlightPriceConditions = new LoadCheapestFlightPriceViewModel();
  }

  //#endregion

  //#region Methods

  // Called when flight search is clicked.
  public ngOnFlightSearchClicked(flightSearchForm: NgForm): void {

    // Mark form controls to be dirty.
    this.markControlToBeDirty(flightSearchForm.form);

    // Flight search form is invalid.
    if (!flightSearchForm.valid) {
      return;
    }
  }

  /**
   * Marks all controls in a form group as touched
   * @param formGroup - The form group to touch
   */
  private markControlToBeDirty(formGroup: FormGroup) {
    (<any>Object).values(formGroup.controls).forEach(control => {
      control.markAsDirty();

      if (control.controls) {
        this.markControlToBeDirty(control);
      }
    });
  }

  //#endregion
}
