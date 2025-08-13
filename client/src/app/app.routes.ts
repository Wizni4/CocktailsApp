import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './features/home/home.component';
import { SignInComponent } from './features/auth/signin/signin.component';
import { SignUpComponent } from './features/auth/signup/signup.component';
import { AuthGuard } from './core/auth.guard';
import { CreateClubComponent } from './features/club/create-club/create-club.component';
import { ClubLayoutComponent } from './features/club/club-layout.component';
import { CreateCocktailComponent } from './features/cocktail/create-cocktail/create-cocktail.component';
import { ClubSettingsComponent } from './features/club/club-settings/club-settings.component';
import { ClubOverviewComponent } from './features/club/club-overview/club-overview.component';
import { ClubMembersComponent } from './features/club/club-settings/club-members/club-members.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'auth/signin', component: SignInComponent },
      { path: 'auth/signup', component: SignUpComponent },
      { path: 'cocktails/create', component: CreateCocktailComponent, canActivate: [AuthGuard] },
      { path: 'clubs/create', component: CreateClubComponent, canActivate: [AuthGuard] },
      {
        path: 'clubs/:clubId', component: ClubLayoutComponent, canActivate: [AuthGuard],
        children: [
          { path: '', component: ClubOverviewComponent, canActivate: [AuthGuard] },
          {
            path: 'settings', component: ClubSettingsComponent, canActivate: [AuthGuard],
            children: [
              { path: 'members', component: ClubMembersComponent, canActivate: [AuthGuard], }
            ]
          },
        ]
      },
    ]
  },
  { path: '**', redirectTo: '' }
];
