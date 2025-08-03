import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';


@Component({
  selector: 'submit-button',
  templateUrl: './submit-button.component.html',
  styleUrls: ['./submit-button.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    MessageModule,
  ],
})
export class SubmitButtonComponent {
  @Input() label!: string;
  @Input() loading: boolean = false;
  @Input() isInvalid: boolean = false;
  @Input() errorMessage: string = '';
}
