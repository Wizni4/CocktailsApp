import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { SearchResult } from '../../../../shared/models/search-result.model';
import { SearchService } from '../../../../shared/services/search.service';

@Component({
  selector: 'search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    InputTextModule,
    TableModule
  ]
})
export class SearchBarComponent {
  @Input() placeholder = '';
  loading: boolean = false;
  search$ = new Subject<string>();
  searchResults: SearchResult[] = [];

  constructor(private searchService: SearchService) {
    this.search$
      .pipe(debounceTime(1000), )
      .subscribe((term) => {
        this.loading = true;
        if (term != "") {
          this.searchService.search(term).subscribe((data) => {
            this.searchResults = data;
          });
        } else {
          this.searchResults = [];
        }
        this.loading = false;
      });
  }

  onChange(e: Event) {
    const term = (e.target as HTMLInputElement).value.trim();
    this.search$.next(term);
  }
}
