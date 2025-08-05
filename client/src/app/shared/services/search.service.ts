import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { SearchResult } from '../models/search-result.model'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private apiService: ApiService) { }

  public search(term: string): Observable<SearchResult[]> {
    return this.apiService
      .withQueryParam({ "term": term })
      .get<SearchResult[]>('search')
  }
}
