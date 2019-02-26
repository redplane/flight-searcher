import {IFlightService} from '../interfaces/services/flight-service.interface';
import {Injectable} from '@angular/core';
import {LoadCheapestFlightPriceViewModel} from '../view-models/load-cheapest-flight-price.view-model';
import {CheapestFlightPriceViewModel} from '../view-models/cheapest-flight-price.view-model';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {AppConfigService} from './app-config.service';

@Injectable()
export class FlightService implements IFlightService {

  //#region Properties

  private _apiUrl = '';

  //#endregion

  //#region Constructor

  public constructor(public appConfigService: AppConfigService, public httpClient: HttpClient) {
    const configuration = appConfigService.loadConfigurationFromCache();
    this._apiUrl = configuration.baseUrl;
  }

  //#endregion

  //#region Methods

  public loadCheapestFlightsAsync(model: LoadCheapestFlightPriceViewModel): Observable<CheapestFlightPriceViewModel[]> {
    const fullUrl = `${this._apiUrl}/api/flight/search`;
    return this.httpClient
      .post<CheapestFlightPriceViewModel[]>(fullUrl, model);
  }

  //#endregion
}
