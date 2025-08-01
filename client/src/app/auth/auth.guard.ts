import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.auth.isAuthenticated().pipe(
      take(1), // take the latest auth value and complete
      map(isAuth => {
        if (!isAuth) {
          this.router.navigate(['/auth/signin']);
          return false;
        }
        return true;
      })
    );
  }
}
