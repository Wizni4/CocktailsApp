import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { UserService } from '../user/user.service';
import { Club } from '../club/models/club.model';
import { Observable } from 'rxjs';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { EmptyStateComponent} from './components/empty-state/empty-state.component';
import { ClubsCardsComponent } from './components/clubs-cards/clubs-cards.component';
import { SearchResult } from '../../shared/models/search-result.model';


@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [
    CommonModule,
    ClubsCardsComponent,
    SearchBarComponent,
    EmptyStateComponent,
 ]
})
export class HomeComponent {
  clubs$!: Observable<Club[]>;
  searchResults: SearchResult[] = [];
  lowStock: any[] = [];
  searchLoading = false;
  activeTabIndex = 0;
  constructor(
    private userService: UserService) {
  }
  ngOnInit(): void {
    this.clubs$ = this.userService.getUserClubs();
      this.clubs$.subscribe(
    )
  }
}
