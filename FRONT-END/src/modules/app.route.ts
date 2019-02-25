import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {RouterModule, Routes} from '@angular/router';

//#region Properties

// Application routes configuration.
export const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: '/dashboard'
      },
      {
        path: 'dashboard',
        loadChildren: 'modules/dashboard/dashboard.module#DashboardModule',
      }
    ]
  }
];

//#endregion

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    RouterModule.forRoot(routes, {enableTracing: true})
  ],
  exports: [
    RouterModule
  ],
  bootstrap: [AppComponent]
})

export class AppRouteModule {
}
