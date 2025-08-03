import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from './features/auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  template: `<router-outlet></router-outlet>`,
})
export class AppComponent { }
