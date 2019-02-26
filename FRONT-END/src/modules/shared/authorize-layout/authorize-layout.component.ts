import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ProfileViewModel} from '../../../view-models/profile.view-model';

@Component({
  selector: 'authorize-layout',
  templateUrl: 'authorize-layout.component.html',
  styleUrls: ['authorize-layout.component.scss']
})

export class AuthorizeLayoutComponent implements OnInit {

  //#region Properties

  // Whether loader is shown or not.
  public bIsLoaderShown = false;

  //#endregion

  //#region Constructor

  /*
  * Initiate component with injectors.
  * */
  public constructor(public activatedRoute: ActivatedRoute) {
  }

  //#endregion

  //#region Methods

  /*
  * Event which is called when component has been initiated.
  * */
  public ngOnInit(): void {

  }

  //#endregion
}
