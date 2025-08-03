import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Observable, map, catchError } from 'rxjs';
import { CreateClubRequest } from './models/create-club.model';
import { Club } from './models/club.model';

@Injectable({
  providedIn: 'root',
})
export class ClubService {

  constructor(private apiService: ApiService) { }

  public createClub(club: CreateClubRequest): Observable<Club> {
    return this.apiService.withBody(club).post<Club>("clubs")
  }
}
