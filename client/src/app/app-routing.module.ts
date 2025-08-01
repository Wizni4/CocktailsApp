import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard'
import { SignInComponent } from './auth/signin/signin.component';
import { SignUpComponent } from './auth/signup/signup.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
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
