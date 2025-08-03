import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    InputTextModule,
  ]
})
export class SearchBarComponent {
  @Input() placeholder = '';
  @Output() search = new EventEmitter<string>();

  private input$ = new Subject<string>();

  constructor() {
    this.input$
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((v) => this.search.emit(v));
  }

  onChange(e: Event) {
    this.input$.next((e.target as HTMLInputElement).value.trim());
  }
}
