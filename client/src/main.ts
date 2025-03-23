import container from './di-container';
import './polyfills'
import { platformBrowser } from '@angular/platform-browser';
import { AppModule } from './app/app.module';
import { AmplifyService } from './app/auth/amplify.service';

container.register('AuthService', new AmplifyService());
platformBrowser().bootstrapModule(AppModule)
  .catch(err => console.error(err));
