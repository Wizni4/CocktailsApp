import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ControlContainer, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { FloatLabelModule } from 'primeng/floatlabel';


@Component({
  selector: 'custom-dropdown',
  imports: [
    FormsModule,
    CommonModule,
    AutoCompleteModule,
    FloatLabelModule,
    ReactiveFormsModule,
  ],
  templateUrl: './custom-dropdown.component.html',
  styleUrls: ['./custom-dropdown.component.css']
})
export class CustomDropdownComponent {
  @Input() items: string[] = [];
  @Input() label!: string;
  @Input() id = '';
  @Input() name = '';
  @Input() required: string = "false";
  @Input() appendTo: string = "";

  value: string = "";
  disabled = false;
  filteredItems: string[] = [];

  @Output() search = new EventEmitter<string>();

  constructor(private controlContainer: ControlContainer) { }

  onSearch(event: any): void {
    const query = event.query.toLowerCase();
    this.filteredItems = this.items.filter(item =>
      item.toLowerCase().includes(query)
    );
  }

  get control(): FormControl | null {
    return this.controlContainer?.control?.get(this.name) as FormControl | null;
  }

  get isRequired(): boolean {
    return this.required.toLowerCase() === "true";
  }

  get isInvalid(): boolean {
    return (!!this.control?.invalid && this.control?.touched) || false;
  }

}
