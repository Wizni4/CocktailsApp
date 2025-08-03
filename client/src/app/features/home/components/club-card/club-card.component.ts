import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CardModule } from 'primeng/card';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';
import { Club } from '../../../club/models/club.model';
import { FieldsetModule } from 'primeng/fieldset';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-club-card',
  templateUrl: './club-card.component.html',
  styleUrls: ['./club-card.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    BadgeModule,
    CardModule,
    OverlayBadgeModule,
    ProgressBarModule,
    ToastModule,
    FieldsetModule,
    RouterLink,
    ButtonModule,
  ]
})
export class ClubCardComponent {
  @Input() club!: Club;
  @Input() discover = false;
  @Output() delete = new EventEmitter<string>();

  get clubAddress(): string {
    const { streetNumber, street, postalCode, city, country } = this.club.address;
    return `${streetNumber} ${street}
${postalCode} ${city}, ${country}`;

  }
}
