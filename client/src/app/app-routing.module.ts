import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard'
import { ProfileComponent } from './auth/profile/profile.component';
import { SignInComponent } from './auth/signin/signin.component';
import { SignUpComponent } from './auth/signup/signup.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'auth/profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'auth/signin',
    component: SignInComponent,
  },
  {
    path: 'auth/signup',
    component: SignUpComponent,
  },
  {
    path: '**',
    redirectTo: ''
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class AppRoutingModule {
}
