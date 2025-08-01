import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { definePreset, palette, ColorScale } from '@primeng/themes'


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    providePrimeNG({
      theme: {
        preset: definePreset(Aura, {
          semantic: {
            primary: palette('{rose}') as ColorScale,
          },
          foundation: {
            surface: palette('{rose}') as ColorScale
          }
        }),
        options: {
          darkModeSelector: 'none'
        }
      }
    })
  ]
};
