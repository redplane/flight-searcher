import {IMessageBusService} from '../interfaces/services/message-bus-service.interface';
import {EventEmitter} from '@angular/core';

export class MessageBusService implements IMessageBusService {

  //#region Properties

  // Map of channels & event emitter.
  private _mChannel: Map<string, Map<string, EventEmitter<any>>>;

  //#endregion

  //#region Constructor

  /*
  * Initialize service with injectors.
  * */
  public constructor() {
    this._mChannel = new Map<string, Map<string, EventEmitter<any>>>();
  }

  //#endregion

  //#region Methods

  /*
  * Add message channel event emitter.
  * */
  public addMessageChannel<T>(channelName: string, eventName: string): EventEmitter<T> {

    // Find channel mapping.
    const mChannel = this._mChannel;

    // Channel is not available.
    let mEventMessageEmitter: Map<string, EventEmitter<any>>;

    if (mChannel.has(channelName)) {
      mEventMessageEmitter = mChannel.get(channelName);
    } else {
      mEventMessageEmitter = new Map<string, EventEmitter<any>>();
      this._mChannel.set(channelName, mEventMessageEmitter);
    }

    if (mEventMessageEmitter.has(eventName)) {
      return mEventMessageEmitter.get(eventName);
    }

    const emitter = new EventEmitter();
    mEventMessageEmitter.set(eventName, emitter);
    return <EventEmitter<T>>emitter;
  }

  /*
  * Hook message event.
  * */
  public hookMessageChannel<T>(channelName: string, eventName: string): EventEmitter<T> {

    const mChannel = this._mChannel;

    if (mChannel == null || !mChannel.has(channelName)) {
      mChannel.set(channelName, null);
    }

    let mEventMessageEmitter = mChannel.get(channelName);
    if (mEventMessageEmitter == null) {
      mEventMessageEmitter = new Map<string, EventEmitter<any>>();
      mChannel.set(channelName, mEventMessageEmitter);
    }

    let emitter = mEventMessageEmitter.get(eventName);
    if (emitter == null) {
      emitter = new EventEmitter<any>();
      mEventMessageEmitter.set(eventName, emitter);
    }

    return emitter;
  }

  /*
  * Publish message to event stream.
  * */
  public addMessage<T>(channelName: string, eventName: string, data: T): void {

    // Find the existing channel.
    const mChannel = this._mChannel;
    if (!mChannel) {
      return;
    }
    let mEventMessageEmitter = mChannel.get(channelName);
    let emitter: EventEmitter<any>;

    if (!mEventMessageEmitter) {
      emitter = new EventEmitter();
      mEventMessageEmitter = new Map<string, EventEmitter<any>>();
      mEventMessageEmitter.set(eventName, emitter);
    } else {
      emitter = mEventMessageEmitter.get(eventName);
    }

    emitter.emit(data);
  }


  //#endregion

}
