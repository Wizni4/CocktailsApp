import { Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChipModule } from 'primeng/chip';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ReactiveFormsModule, FormControl, ControlContainer, FormsModule } from '@angular/forms';
import { CustomInputComponent } from '../custom-input/custom-input.component';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'custom-chips',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ChipModule,
    FloatLabelModule,
    ReactiveFormsModule,
    CustomInputComponent,
  ],
  templateUrl: './custom-chips.component.html',
  styleUrls: ['./custom-chips.component.css']
})
export class CustomChipsComponent {
  @ViewChild(CustomInputComponent, { static: true }) customInput!: CustomInputComponent;
  @Input() label!: string;
  @Input() id = '';
  @Input() name = '';
  @Input() type = 'text';
  @Input() required: string = "false";

  disabled = false;

  constructor(private controlContainer: ControlContainer) { }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }

  addChip(): void {
    const val = this.customInput.nativeElement.value.trim();
    if (!val) return;
    const raw = this.control?.value;
    const current = Array.isArray(raw) ? raw : [];
    if (!current.includes(val)) {
      this.control!.setValue([...current, val]);
    }
    this.customInput.nativeElement.value = '';
    console.log(this.control?.value);
  }

  removeChip(value: string): void {
    const raw = this.control?.value;
    const current = Array.isArray(raw) ? raw : [];

    this.control!.setValue(current.filter(v => v !== value));
  }

  onKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      event.preventDefault();
      this.addChip();
    }
  }

  isArray(value: any): value is any[] {
    return Array.isArray(value);
  }
}
