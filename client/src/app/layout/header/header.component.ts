import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';
import { Observable } from 'rxjs';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { LayoutService } from '../layout.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  imports: [CommonModule, RouterModule, ButtonModule]
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(
    private authService: AuthService,
    private router: Router,
    public layoutService: LayoutService) {
    this.isAuthenticated$ = this.authService.isAuthenticated();
    this.isAuthenticated$.subscribe(value => console.log('Auth status:', value));
  }
  public signOut(): void {
    this.authService.signOut()
      .then(() => this.router.navigate(['/signIn']));
  }
  public toggleDarkMode() {
    this.layoutService.toggleDarkMode();
  }
}
