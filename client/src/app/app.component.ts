import container from '../di-container';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false,
})
export class AppComponent implements OnInit {
  private authService: IAuthService = container.resolve<IAuthService>('AuthService');
  isAuthenticated: boolean = false;

  constructor(private router: Router) {}

  public ngOnInit(): void {
    this.authService.isAuthenticated()
      .then((success: boolean) => {
        this.isAuthenticated = success;
      });
  }

  public signOut(): void {
    this.authService.signOut()
      .then(() => {
        this.router.navigate(['/signIn']);
      });
  }

}
