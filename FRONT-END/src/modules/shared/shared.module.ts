import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {AuthorizeLayoutComponent} from './authorize-layout/authorize-layout.component';
import {NavigationBarComponent} from './navigation-bar/navigation-bar.component';
import {SideBarComponent} from './side-bar/side-bar.component';
import {RouterModule} from '@angular/router';
import {MomentModule} from 'ngx-moment';
import {TranslateLoader, TranslateModule, TranslateService} from '@ngx-translate/core';
import {HttpLoaderFactory} from '../../factories/ngx-translate.factory';
import {HttpClient} from '@angular/common/http';

//#region Module declaration

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MomentModule,
    RouterModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  declarations: [
    AuthorizeLayoutComponent,
    NavigationBarComponent,
    SideBarComponent
  ],
  exports: [
    TranslateModule
  ]
})

export class SharedModule {

  public constructor(translateService: TranslateService) {
    translateService.use('en-US');
  }
}

//#endregion
