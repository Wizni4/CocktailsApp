import { Component, Input } from '@angular/core';
import { Club } from '../../../club/models/club.model';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-empty-state',
  templateUrl: './empty-state.component.html',
  styleUrls: ['./empty-state.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    RouterLink,
 ]
})
export class EmptyStateComponent {
  @Input() clubs: Club[] = [];
}
