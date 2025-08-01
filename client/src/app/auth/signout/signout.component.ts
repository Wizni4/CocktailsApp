import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-signout',
  standalone: false,
  templateUrl: './signout.component.html',
  styleUrl: './signout.component.css'
})
export class SignOutComponent {
  constructor(private router: Router, private authService: AuthService) {}

  public signOut(): void {
    this.authService.signOut();
  }
}
