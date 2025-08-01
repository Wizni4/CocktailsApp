import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthResult, SignInRequest, SignUpRequest } from './auth.interface';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accessToken: string | null = null;
  private idToken: string | null = null;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(private api: ApiService, private router : Router) { }

  public getAccessToken(): string | null {
    return this.accessToken;
  }

  public isAuthenticated(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  async signUp(data: SignUpRequest): Promise<void> {
    const result = await this.api.withBody(data).post('auth/signup');
  }

  async signIn(data: SignInRequest): Promise<void> {
    const result = await this.api.withBody(data).post('auth/signin');
    this.accessToken = (result as AuthResult).accessToken;
    this.idToken = (result as AuthResult).idtoken;
    this.isAuthenticatedSubject.next(true);
  }

  async refreshToken(): Promise<void> {
    try {
      const result = await this.api.post('auth/refresh-token');
      this.accessToken = (result as any).accessToken;
      this.isAuthenticatedSubject.next(true);
    } catch (error) {
      this.accessToken = null;
      this.isAuthenticatedSubject.next(false);
      throw error; // ⬅️ Important so the interceptor doesn't retry endlessly
    }    
  }

  async signOut(): Promise<void> {
    await this.api.post('auth/signout');
    this.accessToken = null;
    this.idToken = null;
    this.isAuthenticatedSubject.next(false);
    this.router.navigate(['/auth/signin']);
  }
}
