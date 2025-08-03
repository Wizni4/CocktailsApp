/// <reference types="google.maps" />
import { Component, ViewChild, AfterViewInit, NgZone,Input} from '@angular/core';
import { ControlContainer, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { environment } from '../../../../environments/environment';
import { CustomInputComponent } from '../custom-input/custom-input.component';

@Component({
  selector: 'address-input',
  standalone: true,
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.css'],
  imports: [
    CustomInputComponent,
    ReactiveFormsModule,
  ]
})
export class AddressFormComponent implements AfterViewInit {
  @ViewChild("locationInput") locationInput!: CustomInputComponent;
  @Input() name: string = '';

  
  constructor(
    private controlContainer: ControlContainer,
    private ngZone: NgZone) { }


  get addressGroup(): FormGroup | null {
    return this.controlContainer?.control?.get(this.name) as FormGroup | null;
  }

  async ngAfterViewInit(): Promise<void> {
    await this.loadGoogleMapsScript();

    const autocomplete = new google.maps.places.Autocomplete(this.locationInput.nativeElement, {
      fields: ['address_components', 'geometry', 'name'],
      types: ['address']
    });

    autocomplete.addListener('place_changed', () => {
      this.ngZone.run(() => {
        const place = autocomplete.getPlace();
        this.fillAddressFields(place);
      });
    });
  }

  private fillAddressFields(place: google.maps.places.PlaceResult): void {
    const get = (type: string) =>
      place.address_components?.find(c => c.types.includes(type))?.long_name ?? '';

    const getShort = (type: string) =>
      place.address_components?.find(c => c.types.includes(type))?.short_name ?? '';

    // Assume parent form has an 'address' nested group
    this.addressGroup?.patchValue({
      street: get('route'),
      streetNumber: getShort('street_number'),
      city: get('locality') || get('administrative_area_level_2'),
      postalCode: getShort('postal_code'),
      state: getShort('administrative_area_level_1'),
      country: get('country'),
    });
  }

  private loadGoogleMapsScript(): Promise<void> {
    if (window['google'] && window['google'].maps) {
      return Promise.resolve();
    }

    return new Promise((resolve) => {
      const script = document.createElement('script');
      script.src = `https://maps.googleapis.com/maps/api/js?key=${environment.googleMapsApiKey}&libraries=places`;
      script.async = true;
      script.defer = true;
      script.onload = () => resolve();
      document.head.appendChild(script);
    });
  }
}
