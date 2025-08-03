import { Component } from '@angular/core';
import { MessageModule } from 'primeng/message';
import { CustomInputComponent } from '../../../../shared/components/custom-input/custom-input.component';
import { ControlContainer, FormControl } from '@angular/forms';


@Component({
  selector: 'app-email-input',
  templateUrl: './email-input.component.html',
  standalone: true,
  imports: [CustomInputComponent, MessageModule],
})
export class EmailInputComponent {
  label: string = "Email";
  name: string = "email";

  constructor(private controlContainer: ControlContainer) { }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }
}
