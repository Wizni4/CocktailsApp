import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { routes } from './app.routes';
import { definePreset, palette, ColorScale } from '@primeng/themes'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './core/auth.interceptor';
import { AuthService } from './features/auth/auth.service';
import { of, firstValueFrom } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';

export function initializeApp(authService: AuthService): () => Promise<void> {
  return() =>
  firstValueFrom(
    authService.refreshToken().pipe(
      catchError(() => {
        return of(void 0);
      })
    )
  );
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptorsFromDi()),
    providePrimeNG({
      theme: {
        preset: definePreset(Aura, {
          semantic: {
            primary: palette('{slate}') as ColorScale,
          },
        }),
        options: {
          darkModeSelector: '.my-app-dark'
        }
      }
    }),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AuthService],
      multi: true
    },
    // bring in the ngx-loading-bar modules as providers
    importProvidersFrom(
      LoadingBarRouterModule,
      LoadingBarHttpClientModule
    )
  ],
};
