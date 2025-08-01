import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';
import { Observable } from 'rxjs';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule, ButtonModule]
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(
    private authService: AuthService,
    private router: Router) {
    this.isAuthenticated$ = this.authService.isAuthenticated();
    this.isAuthenticated$.subscribe(value => console.log('Auth status:', value));
  }
  public signOut(): void {
    this.authService.signOut()
      .then(() => this.router.navigate(['/signIn']));
  }
}
