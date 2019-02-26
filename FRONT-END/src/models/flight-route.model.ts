export class FlightRouteModel {

  //#region Properties

  // Route name
  public name: string;

  // Route value
  public value: string;

  //#endregion

  //#region Constructor

  constructor(name: string, value: string) {
    this.name = name;
    this.value = value;
  }

  //#endregion
}
