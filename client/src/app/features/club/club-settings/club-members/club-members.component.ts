import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClubService } from '../../services/club.service';
import { ClubMember } from '../../models/club-member.model';
import { map, Observable, switchMap } from 'rxjs';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-club-members',
  imports: [
    CommonModule,
    TableModule,
  ],
  templateUrl: './club-members.component.html',
  styleUrl: './club-members.component.css'
})
export class ClubMembersComponent {
  private route = inject(ActivatedRoute);
  clubId!: string;
  clubMembers$ = new Observable<ClubMember[]>();

  constructor(private clubService: ClubService) { }

  ngOnInit() {
    // /clubs/:clubId  ->  settings  ->  members  (this component)
    const clubsRoute = this.route.parent!.parent!;

    this.clubMembers$ = clubsRoute.paramMap.pipe(
      map(p => p.get('clubId')!),                // read :clubId from ancestor
      switchMap(id => this.clubService.getMembers(id))
    );
  }
}
