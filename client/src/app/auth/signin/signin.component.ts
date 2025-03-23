import container from '../../../di-container';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, IUser  } from '../auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  standalone: false,
})
export class SignInComponent {
  private authService: IAuthService = container.resolve<IAuthService>('AuthService');
  loading: boolean = false;
  user: IUser = {} as IUser;

  constructor(private router: Router) {}

  public signIn(): void {
    this.loading = true;
    this.authService.signIn(this.user)
      .then(() => {
        this.router.navigate(['/auth/profile']);
      }).catch((reason: any) => {
        console.log(reason);
        this.loading = false;
      });
  }

}
