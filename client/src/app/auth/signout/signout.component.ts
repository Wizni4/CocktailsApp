import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-signout',
  standalone: true,
  templateUrl: './signout.component.html',
  styleUrls: ['./signout.component.css'],
  imports: [FormsModule]
})
export class SignOutComponent {
  constructor(private router: Router, private authService: AuthService) {}

  public signOut(): void {
    this.authService.signOut();
  }
}
