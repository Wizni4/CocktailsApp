import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CardModule } from 'primeng/card';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';
import { Club } from '../../../club/models/club.model';


@Component({
  selector: 'club-card',
  templateUrl: './club-card.component.html',
  styleUrls: ['./club-card.component.html'],
  standalone: true,
  imports: [
    CommonModule,
    BadgeModule,
    CardModule,
    OverlayBadgeModule,
    ProgressBarModule,
    ToastModule,
  ]
})
export class ClubCardComponent {
  @Input() club!: Club;
  @Input() discover = false;
  @Output() delete = new EventEmitter<string>();
}
