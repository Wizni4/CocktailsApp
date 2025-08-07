import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnitService {
  constructor(private apiService: ApiService) { }

  public getUnitsOfMeasure(): Observable<string[]> {
    return this.apiService.get<string[]>('shared/units')
  }
}
