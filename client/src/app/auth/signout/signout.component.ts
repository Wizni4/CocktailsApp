import container from '../../../di-container';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthService, IUser } from '../auth.service';


@Component({
  selector: 'app-signout',
  standalone: false,
  templateUrl: './signout.component.html',
  styleUrl: './signout.component.css'
})
export class SignOutComponent {
  private authService: IAuthService = container.resolve<IAuthService>('AuthService');
  user: IUser = {} as IUser;

  constructor(private router: Router) {}

  public signOut(): void {
    this.authService.signOut();
  }
}
