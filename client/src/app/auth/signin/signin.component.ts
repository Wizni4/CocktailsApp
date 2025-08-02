import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SignInRequest } from '../auth.interface';
import { AuthService } from '../auth.service';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-sign-in',
  templateUrl: './signin.component.html',
  standalone: true,
  imports: [FormsModule, ButtonModule, FloatLabelModule]
})
export class SignInComponent {
  loading: boolean = false;
  signInRequest: SignInRequest = {} as SignInRequest;

  constructor(private router: Router, private authService: AuthService) {}

  public signIn(): void {
    this.loading = true;
    this.authService.signIn(this.signInRequest)
      .then(() => {
        this.router.navigate(['/auth/profile']);
      }).catch((reason: any) => {
        console.log(reason);
        this.loading = false;
      });
  }

}
