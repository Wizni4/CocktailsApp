import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { ControlContainer, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { MessageModule } from 'primeng/message';


@Component({
  selector: 'custom-input',
  templateUrl: './custom-input.component.html',
  styleUrls: ['./custom-input.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    InputTextModule,
    FloatLabelModule,
    MessageModule,
    FormsModule,
    ReactiveFormsModule
  ],
})
export class CustomInputComponent {
  @ViewChild('realInput', { static: true }) realInput!: ElementRef<HTMLInputElement>;
  @Input() label!: string;
  @Input() id = '';
  @Input() name = '';
  @Input() type = 'text';
  @Input() required: string = "false";

  value: string = '';
  disabled = false;

  constructor(private controlContainer: ControlContainer) { }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }

  get isRequired(): boolean {
    return this.required.toLowerCase() === "true"; 
  }

  get nativeElement(): HTMLInputElement {
    return this.realInput.nativeElement;
  }

  get isInvalid(): boolean {
    return (!!this.control?.invalid && this.control?.touched) || false;
  }
}
