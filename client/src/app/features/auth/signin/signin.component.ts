import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SignInRequest } from '../auth.model';
import { AuthService } from '../auth.service';
import { FormBuilder, FormsModule, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PasswordInputComponent } from '../components/password-input/password-input.component'
import { UsernameInputComponent } from '../components/username-input/username-input.component'
import { SubmitButtonComponent } from '../../../shared/components/submit-button/submit-button.component'


@Component({
  selector: 'app-sign-in',
  templateUrl: './signin.component.html',
  styleUrls: ['../auth.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    PasswordInputComponent,
    UsernameInputComponent,
    SubmitButtonComponent
]
})
export class SignInComponent {
  authForm: FormGroup;
  loading: boolean = false;
  signInError: string = '';
  signInRequest: SignInRequest = {} as SignInRequest;

  constructor(
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder) {
    this.authForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)
      ]]
    })
  }

  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe(
      () => this.router.navigate(['/']),
    )
  }

  isInvalid(controlName: string): boolean {
    const control = this.authForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }

  public signIn(): void {
    this.signInError = '';
    this.loading = true;

    if (this.authForm.invalid) {
      this.authForm.markAllAsTouched();
      this.loading = false;
      return;
    }
    this.signInRequest = this.authForm.value;
    this.authService.signIn(this.signInRequest).subscribe({
      next: () => {
        this.router.navigate(['/auth/profile']);
        this.loading = false;
      },
      error: err => {
        this.signInError = err?.error?.detail || 'Sign In failed. Please try again.';
        this.loading = false;
      },
    });
  }

}
