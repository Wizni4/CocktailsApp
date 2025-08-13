import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenuItemContent, MenuModule } from 'primeng/menu';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-club-settings',
  standalone: true,
  imports: [
    MenuModule,
    RouterModule,
    RippleModule,
  ],
  templateUrl: './club-settings.component.html',
  styleUrls: ['./club-settings.component.css']
})
export class ClubSettingsComponent {


  menuItems: MenuItem[] = [
    { separator: true },
    {
      label: 'Access',
      items: [
        { label: 'Members', icon: 'pi pi-user', routerLink: ['members'] },
        { label: 'Roles', icon: 'pi pi-id-card' },
      ],
    },
    { separator: true },
    {
      label: 'Cocktails',
      items: [
        { label: 'Manage cocktails', icon: 'pi pi-star' },
        { label: 'Prices', icon: 'pi pi-dollar' },
        { label: 'Orders', icon: 'pi pi-shopping-cart' },
        { label: 'Stocks', icon: 'pi pi-box' },
      ]
    },
  ];
}
