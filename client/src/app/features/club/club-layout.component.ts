import { Component, inject } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from '@angular/router';
import { TabsModule } from 'primeng/tabs';
import { filter } from 'rxjs';

@Component({
  selector: 'app-club-layout',
  standalone: true,
  imports: [
    TabsModule,
    RouterModule,
  ],
  templateUrl: './club-layout.component.html',
  styleUrls: ['./club-layout.component.css'],
})
export class ClubLayoutComponent {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  activeTab: string = '';
  clubId!: string;

  ngOnInit() {
    // set once on load
    this.activeTab = this.setActiveFromUrl(); // on initial load

    this.route.paramMap.subscribe(p => {
      this.clubId = p.get('clubId')!;
    });

    // keep in sync on navigation
    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => {
        this.activeTab = this.setActiveFromUrl();
      });
  }

  private setActiveFromUrl(): string {
    // First child directly under /clubs/:clubId
    const child = this.route.firstChild;

    // Its configured path is the tab key: '' | 'settings' | 'members' | ...
    const path = child?.routeConfig?.path ?? '';

    // Map '' to your "overview" tab; any other path matches its tab value
    return path === '' ? 'overview' : path;
  }
}
