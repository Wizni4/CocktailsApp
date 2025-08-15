import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClubCardComponent } from '../club-card/club-card.component';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { UserClubItem } from '../../../user/models/user-clubs';

@Component({
  selector: 'app-clubs-cards',
  imports: [
    ClubCardComponent,
    CommonModule,
    ButtonModule,
    RouterLink,
 ],
  templateUrl: './clubs-cards.component.html',
  styleUrl: './clubs-cards.component.css'
})
export class ClubsCardsComponent {
  @Input() clubs: UserClubItem[] = [];
}
