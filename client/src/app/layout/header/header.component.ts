import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../features/auth/auth.service';
import { Observable } from 'rxjs';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { LayoutService } from '../layout.service';
import { MenuModule } from 'primeng/menu';
import { MenuItem,  } from 'primeng/api';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { SearchBarComponent } from '../../features/home/components/search-bar/search-bar.component';


@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  imports: [
    AvatarModule,
    AvatarGroupModule,
    BadgeModule,
    ButtonModule,
    CommonModule,
    MenuModule,
    OverlayBadgeModule,
    RouterModule,
    SearchBarComponent,
  ]
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(
    private authService: AuthService,
    private router: Router,
    public layoutService: LayoutService
  ) {
    this.isAuthenticated$ = this.authService.isAuthenticated();
  }

  public signOut(): void {
    this.authService.signOut().subscribe(() => {
      this.router.navigate(['/auth/signin']);
    });
  }

  public toggleDarkMode(): void {
    this.layoutService.toggleDarkMode();
  }

  menuItems: MenuItem[] = [
    { label: 'Profile', icon: 'pi pi-user' },
    { separator: true },
    { label: 'Settings', icon: 'pi pi-cog' },
    { separator: true },
    { label: 'Logout', icon: 'pi pi-sign-out', command: () => this.signOut() }
  ];
}
