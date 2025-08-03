import { Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { ControlContainer, FormGroupDirective, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FloatLabelModule } from 'primeng/floatlabel';
import { MessageModule } from 'primeng/message';
import { TextareaModule } from 'primeng/textarea';


@Component({
  selector: 'custom-textarea',
  templateUrl: './custom-textarea.component.html',
  styleUrls: ['./custom-textarea.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FloatLabelModule,
    MessageModule,
    FormsModule,
    ReactiveFormsModule,
    TextareaModule,
  ],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class CustomTextAreaInput {
  @ViewChild('realInput', { static: true }) realInput!: ElementRef<HTMLTextAreaElement>;
  @Input() label!: string;
  @Input() id: string = '';
  @Input() name: string = '';
  @Input() required: string = "false";
  @Input() minHeight: string = '8rem';
  @Input() maxHeight: string = '25rem';

  value: string = '';
  disabled = false;
  constructor(private controlContainer: ControlContainer) { }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }

  get isRequired(): boolean {
    return this.required.toLowerCase() === "true";
  }


  get nativeElement(): HTMLTextAreaElement {
    return this.realInput.nativeElement;
  }

  get isInvalid(): boolean {
    return (!!this.control?.invalid && this.control?.touched) || false;
  }

  get styleObject(): { [key: string]: string } {
    return {
      'min-height': this.minHeight,
      'max-height': this.maxHeight
    };
  }
}
