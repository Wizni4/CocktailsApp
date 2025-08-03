import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Club } from '../club/models/club.model';
import { Observable, map, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private apiService: ApiService) { }

  public getUserClubs(): Observable<Club[]> {
    return this.apiService.get<Club[]>("users/me/clubs")
  }
}
