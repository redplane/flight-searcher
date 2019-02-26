import {LoadCheapestFlightPriceViewModel} from '../../view-models/load-cheapest-flight-price.view-model';
import {Observable} from 'rxjs';
import {CheapestFlightPriceViewModel} from '../../view-models/cheapest-flight-price.view-model';

export interface IFlightService {

  //#region Methods

  // Load cheapest flights asynchronously.
  loadCheapestFlightsAsync(model: LoadCheapestFlightPriceViewModel): Observable<CheapestFlightPriceViewModel[]>;

  //#endregion
}
