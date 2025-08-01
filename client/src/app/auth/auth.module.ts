import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './auth.guard';
import { SignInComponent } from './signin/signin.component';
import { SignOutComponent } from './signout/signout.component';
import { SignUpComponent } from './signup/signup.component';
import { ApiModule } from '../api/api.module';
import { ApiService } from '../api/api.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SignInComponent,
    SignOutComponent,
    SignUpComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ApiModule,
  ],
  providers: [
    AuthGuard,
    ApiService
  ],
})
export class AuthModule { }
