import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { UserService } from '../user/user.service';
import { Club } from '../club/models/club.model';
import { Observable } from 'rxjs';
import { Router, RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ClubCardComponent } from './components/club-card/club-card.component';
import { MessageModule } from 'primeng/message';
import { TabsModule } from 'primeng/tabs';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { SkeletonModule } from 'primeng/skeleton';
import { SearchBarComponent } from './components/search-bar/search-bar.component';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [
    BadgeModule,
    OverlayBadgeModule,
    CardModule,
    CommonModule,
    ButtonModule,
    RouterModule,
    TableModule,
    MessageModule,
    ClubCardComponent,
    TabsModule,
    SkeletonModule,
    SearchBarComponent,
 ]
})
export class HomeComponent {
  clubs$!: Observable<Club[]>;
  discoverClubs: Club[] = [];
  lowStock: any[] = [];
  discoverLoading = false;
  activeTabIndex = 0;
  constructor(private userService: UserService, public router: Router) {
    
  }
  ngOnInit(): void {
    this.clubs$ = this.userService.getUserClubs();
  }

  onSearch(query: string) {
    this.discoverLoading = true;
    //this.clubsSvc.searchPublic(query).subscribe((data) => {
    //  this.discoverClubs = data;
    //  this.discoverLoading = false;
    //});
  }

  trackById = (_: number, c: Club) => c.id;

  deleteClub(id: string) {
    // optimistic update
    //this.myClubs = this.myClubs.filter((c) => c.id !== id);
    //this.clubsSvc.deleteClub(id).subscribe();
  }
}
