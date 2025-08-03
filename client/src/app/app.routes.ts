import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './features/home/home.component';
import { SignInComponent } from './features/auth/signin/signin.component';
import { SignUpComponent } from './features/auth/signup/signup.component';
import { AuthGuard } from './core/auth.guard';
import { CreateClubComponent } from './features/club/create-club/create-club.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'auth/signin', component: SignInComponent },
      { path: 'auth/signup', component: SignUpComponent },
      { path: 'clubs/create', component: CreateClubComponent },
    ]
  },
  { path: '**', redirectTo: '' }
];
