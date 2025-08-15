import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Observable, map, catchError } from 'rxjs';
import { UserClubs } from './models/user-clubs';

@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(private apiService: ApiService) { }

  public getUserClubs(): Observable<UserClubs> {
    return this.apiService.get<UserClubs>("me/clubs")
  }
}
