import {ChangeDetectorRef, Component, Inject, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ProfileViewModel} from '../../../view-models/profile.view-model';
import {IMessageBusService} from '../../../interfaces/services/message-bus-service.interface';
import {MessageBusChannelConstant} from '../../../constants/message-bus-channel.constant';
import {MessageBusEventConstant} from '../../../constants/message-bus-event.constant';

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
  public constructor(public activatedRoute: ActivatedRoute,
                     @Inject('IMessageBusService') public messageBusService: IMessageBusService,
                     public changeDetectorRef: ChangeDetectorRef) {

  }

  //#endregion

  //#region Methods

  /*
  * Event which is called when component has been initiated.
  * */
  public ngOnInit(): void {
    this.messageBusService
      .hookMessageChannel(MessageBusChannelConstant.uiChannel, MessageBusEventConstant.toggleLoader)
      .subscribe((status) => {
        this.bIsLoaderShown = status;
        this.changeDetectorRef.detectChanges();
      });
  }

  //#endregion
}
