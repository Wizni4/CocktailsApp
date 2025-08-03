import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { BehaviorSubject, Observable, map, tap, catchError, throwError } from 'rxjs';
import { AuthResult, SignInRequest, SignUpRequest } from './auth.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accessToken: string | null = null;
  private idToken: string | null = null;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(private api: ApiService, private router: Router) { }

  public getAccessToken(): string | null {
    return this.accessToken;
  }

  public isAuthenticated(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  signUp(data: SignUpRequest): Observable<void> {
    return this.api.withBody(data).post<void>('auth/signup');
  }

  signIn(data: SignInRequest): Observable<AuthResult> {
    return this.api.withBody(data).post<AuthResult>('auth/signin').pipe(
      tap(result => {
        this.accessToken = result.accessToken;
        this.idToken = result.idtoken;
        this.isAuthenticatedSubject.next(true);
      })
    );
  }

  refreshToken(): Observable<void> {
    return this.api.post<AuthResult>('auth/refresh-token').pipe(
      tap(result => {
        this.accessToken = result.accessToken;
        this.isAuthenticatedSubject.next(true);
      }),
      map(() => void 0), // 👈 Convert AuthResult to void
      catchError(err => {
        this.accessToken = null;
        this.isAuthenticatedSubject.next(false);
        return throwError(() => err);
      })
    );
  }

  signOut(): Observable<void> {
    return this.api.post<void>('auth/signout').pipe(
      tap(() => {
        this.accessToken = null;
        this.idToken = null;
        this.isAuthenticatedSubject.next(false);
        this.router.navigate(['/auth/signin']);
      })
    );
  }
}
