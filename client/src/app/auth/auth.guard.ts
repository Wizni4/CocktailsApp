import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { IAuthService } from './auth.service';
import container from '../../di-container';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private authService: IAuthService = container.resolve<IAuthService>('AuthService');

  constructor(private router: Router) { }

  async canActivate(): Promise<boolean> {
    try {
      const isAuthenticated = await this.authService.isAuthenticated();
      if (!isAuthenticated) {
        // Redirect to login page if not authenticated
        this.router.navigate(['/auth/signin']);
      }
      return isAuthenticated;
    } catch (error) {
      console.error('Error checking authentication:', error);
      // Redirect to login page in case of an error
      this.router.navigate(['/auth/signin']);
      return false;
    }
  }
}
