import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LayoutService } from './layout/layout.service';

const THEME_KEY = 'app-light';
const DARK_CLASS = 'app-dark';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  template: `<router-outlet></router-outlet>`,
})
export class AppComponent {}
