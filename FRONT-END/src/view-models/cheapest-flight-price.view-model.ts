export class CheapestFlightPriceViewModel {

  //#region Properties

  public outboundTime: number;

  public price: number;

  //#endregion

  //#region Constructor

  public constructor(outboundTime: number, price: number) {
    this.outboundTime = outboundTime;
    this.price = price;
  }

  //#endregion

}
