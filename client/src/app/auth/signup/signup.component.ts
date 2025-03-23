import container from '../../../di-container';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, IUser } from '../auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: false,
})
export class SignUpComponent {

  private authService: IAuthService = container.resolve<IAuthService>('AuthService');
  loading: boolean = false;
  isConfirm: boolean = false;
  user: IUser = {} as IUser;

  constructor(private router: Router) {}

  public signUp(): void {
    this.loading = true;
    this.authService.signUp(this.user)
      .then(() => {
        this.loading = false;
        this.isConfirm = true;
      }).catch(() => {
        this.loading = false;
      });
  }

  public confirmSignUp(): void {
    this.loading = true;
    this.authService.confirmSignUp(this.user)
      .then(() => {
        this.router.navigate(['/auth/signin']);
      }).catch(() => {
        this.loading = false;
      });
  }

}
