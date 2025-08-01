import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUpRequest } from '../auth.interface';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [FormsModule]
})
export class SignUpComponent {
  loading: boolean = false;
  isConfirm: boolean = false;
  signUpRequest: SignUpRequest = {} as SignUpRequest;

  constructor(private router: Router, private authService: AuthService) {}

  public signUp(): void {
    this.loading = true;
    this.authService.signUp(this.signUpRequest)
      .then(() => {
        this.loading = false;
        this.isConfirm = true;
      }).catch(() => {
        this.loading = false;
      });
  }

  //public confirmSignUp(): void {
  //  this.loading = true;
  //  this.authService.confirmSignUp(this.signUpRequest)
  //    .then(() => {
  //      this.router.navigate(['/auth/signin']);
  //    }).catch(() => {
  //      this.loading = false;
  //    });
  //}

}
