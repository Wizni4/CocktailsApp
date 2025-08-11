import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CreateClubRequest } from '../models/create-club.model';
import { ClubService } from '../services/club.service';
import { CustomInputComponent } from '../../../shared/components/custom-input/custom-input.component';
import { SubmitButtonComponent } from '../../../shared/components/submit-button/submit-button.component';
import { AddressFormComponent } from '../../../shared/components/address-form/address-form.component';
import { CustomTextAreaInput } from '../../../shared/components/custom-textarea/custom-textarea.component';



@Component({
  selector: 'app-create-club',
  standalone: true,
  imports: [
    AddressFormComponent,
    CustomInputComponent,
    CustomTextAreaInput,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SubmitButtonComponent
 ],
  templateUrl: './create-club.component.html',
  styleUrls: ['./create-club.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class CreateClubComponent {
  createClubForm: FormGroup;
  loading: boolean = false;
  errMessage: string = '';
  createClubRequest: CreateClubRequest = {} as CreateClubRequest;

  constructor(
    private router: Router,
    private clubService: ClubService,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef) {
    this.createClubForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      locationInput: null,
      address: this.fb.group({
        street: [''],
        streetNumber: [''],
        city: [''],
        postalCode: [''],
        state: [''],
        country: [''],
      })
    })
  }

  isInvalid(controlName: string): boolean {
    const control = this.createClubForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }

  createClub(): void {
    this.errMessage = '';
    this.loading = true;

    if (this.createClubForm.invalid) {
      this.createClubForm.markAllAsTouched();
      this.loading = false;
      return;
    }
    this.createClubRequest = this.createClubForm.value;
    this.clubService.createClub(this.createClubRequest).subscribe({
      next: () => {
        this.router.navigate(['/auth/profile']);
        this.loading = false;
      },
      error: err => {
        this.errMessage = err?.error?.detail || 'Creation failed. Please try again.';
        this.loading = false;
      },
    });
  }
 }
