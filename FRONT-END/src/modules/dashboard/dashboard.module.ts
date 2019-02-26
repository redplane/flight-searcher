import {DashboardComponent} from './dashboard.component';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {DashboardRouteModule} from './dashboard.route';
import {SharedModule} from '../shared/shared.module';
import {BannerComponent} from './banner/banner.component';
import {FlightSearchComponent} from './flight-search/flight-search.component';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import {GreaterThanCurrentTimeValidator} from '../../validators/greater-than-current-time.validator';
import {CalendarModule, DateAdapter} from 'angular-calendar';
import {adapterFactory} from 'angular-calendar/date-adapters/date-fns';
import {FlightScheduleComponent} from './flight-schedule/flight-schedule.component';

//#region Module declaration

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    DashboardRouteModule,
    BsDatepickerModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    })
  ],
  declarations: [
    DashboardComponent,
    BannerComponent,
    FlightSearchComponent,
    FlightScheduleComponent,
    GreaterThanCurrentTimeValidator
  ],
  exports: [
    DashboardComponent,
    BannerComponent,
    FlightSearchComponent
  ]
})

export class DashboardModule {
}

//#endregion
