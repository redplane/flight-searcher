import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {CalendarEvent} from 'angular-calendar';
import {IMessageBusService} from '../../interfaces/services/message-bus-service.interface';
import {MessageBusChannelConstant} from '../../constants/message-bus-channel.constant';
import {MessageBusEventConstant} from '../../constants/message-bus-event.constant';
import {IFlightService} from '../../interfaces/services/flight-service.interface';
import {LoadCheapestFlightPriceViewModel} from '../../view-models/load-cheapest-flight-price.view-model';
import {finalize} from 'rxjs/operators';
import {CheapestFlightPriceViewModel} from '../../view-models/cheapest-flight-price.view-model';

@Component({
  selector: 'dashboard',
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss']
})

export class DashboardComponent implements OnInit {

  //#region Properties

  public availableFlights: Array<CheapestFlightPriceViewModel>;

  // Mapping between date & flight.
  public _dateToFlightMap: { [date: string]: CheapestFlightPriceViewModel } = {};

  // Whether flights has been searched or not.
  public bHasFlightSearched = false;

  //#endregion

  //#region Constructor

  public constructor(@Inject('IMessageBusService') public messageBusService: IMessageBusService,
                     @Inject('IFlightService') public flightService: IFlightService) {
    this.bHasFlightSearched = false;
    this.availableFlights = new Array<CheapestFlightPriceViewModel>();
  }

  //#endregion

  //#region Methods

  // Called when component is initialized.
  public ngOnInit(): void {
    this.messageBusService
      .addMessage(MessageBusChannelConstant.uiChannel, MessageBusEventConstant.toggleLoader, false);
  }

  // Called when flight is searched.
  public ngOnFlightSearched(condition: LoadCheapestFlightPriceViewModel): void {

    // Block app ui.
    this.messageBusService
      .addMessage(MessageBusChannelConstant.uiChannel, MessageBusEventConstant.toggleLoader, true);

    this.flightService
      .loadCheapestFlightsAsync(condition)
      .pipe(
        finalize(() => {
          // Block app ui.
          this.messageBusService
            .addMessage(MessageBusChannelConstant.uiChannel, MessageBusEventConstant.toggleLoader, false);

          this.bHasFlightSearched = true;
        })
      )
      .subscribe(cheapestFlights => {
        this.availableFlights = cheapestFlights;
      });
  }


  //#endregion
}
