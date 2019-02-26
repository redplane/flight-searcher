import {AbstractControl, NG_VALIDATORS, Validator, ValidatorFn} from '@angular/forms';
import {Directive, Input} from '@angular/core';

@Directive({
  selector: '[greater-than-current-time]',
  providers: [{provide: NG_VALIDATORS, useExisting: GreaterThanCurrentTimeValidator, multi: true}]
})
export class GreaterThanCurrentTimeValidator implements Validator {

  //#region Properties

  // Whether validator is enabled or not.
  @Input('greater-than-current-time')
  private _bIsValidatorEnabled: boolean;

  //#endregion

  //#region Methods

  public validate(control: AbstractControl): { [key: string]: any } | null {

    if (this._bIsValidatorEnabled !== true) {
      return null;
    }

    const validator = validateCurrentTime();
    return validator(control);
  }

  //#endregion
}

// validation function
function validateCurrentTime(): ValidatorFn {
  return (control: AbstractControl) => {

    // Get current time.
    const currentTime = new Date().getTime();

    // Get input date.
    const inputDate = control.value;

    // Get input time.
    if (inputDate == null) {
      return null;
    }

    // Get input time.
    try {
      const inputTime = new Date(inputDate).getTime();
      return (currentTime > inputTime) ? {'greaterThanCurrentTime': true} : null;
    } catch {
      return null;
    }
  };
}
