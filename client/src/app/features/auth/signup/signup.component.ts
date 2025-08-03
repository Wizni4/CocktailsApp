import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SignUpRequest } from '../auth.model';
import { AuthService } from '../auth.service';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import { EmailInputComponent } from '../components/email-input/email-input.component';
import { UsernameInputComponent } from '../components/username-input/username-input.component';
import { PasswordInputComponent } from '../components/password-input/password-input.component';
import { SubmitButtonComponent } from '../../../shared/components/submit-button/submit-button.component';

@Component({
  selector: 'app-sign-up',
  templateUrl: './signup.component.html',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    EmailInputComponent,
    UsernameInputComponent,
    PasswordInputComponent,
    SubmitButtonComponent,
  ]
})
export class SignUpComponent {
  authForm: FormGroup;
  loading: boolean = false;
  signUpRequest: SignUpRequest = {} as SignUpRequest;
  signUpError: string = '';

  constructor(
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder) {
    this.authForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.email],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)
      ]]
    })
}

  isInvalid(controlName: string): boolean {
    const control = this.authForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }

  public signUp(): void {
    this.loading = true;
    this.loading = true;

    if (this.authForm.invalid) {
      this.authForm.markAllAsTouched();
      this.loading = false;
      return;
    }

    this.signUpRequest = this.authForm.value;

    this.authService.signUp(this.signUpRequest).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(["/auth/signin"]);
      },
      error: err => {
        this.loading = false;
        this.signUpRequest = err?.error?.detail || 'Sign Up failed. Please try again.';
      }
    });
  }

}
