import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, from } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from '../features/auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // 1. Skip token refesh logic for auth endpoints
    if (req.url.includes('auth/refresh-token') || req.url.includes('auth/signin')) {
      return next.handle(req);
    }

    // 2. Attach token if present
    const token = this.authService.getAccessToken();
    let authReq = req;

    if (token) {
      authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        },
        withCredentials: true
      });
    }

    // 3. Handle potential refresh
    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          return from(this.authService.refreshToken()).pipe(
            switchMap(() => {
              const newToken = this.authService.getAccessToken();
              const retryReq = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${newToken}`
                },
                withCredentials: true
              });
              return next.handle(retryReq);
            })
          );
        }

        return throwError(() => error);
      })
    );
  }
}
