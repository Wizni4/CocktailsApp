import { Component} from '@angular/core';
import { MessageModule } from 'primeng/message';
import { CustomInputComponent } from '../../../../shared/components/custom-input/custom-input.component';
import { ControlContainer, FormControl } from '@angular/forms';


@Component({
  selector: 'app-username-input',
  templateUrl: './username-input.component.html',
  standalone: true,
  imports: [CustomInputComponent, MessageModule],
})
export class UsernameInputComponent {
  label: string = "Username";
  name: string = "username";

  constructor(private controlContainer: ControlContainer) { }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }
}
