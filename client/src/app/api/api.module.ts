import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from './api.service';

@NgModule({
  imports: [
    HttpClientModule, // Import HttpClientModule here
  ],
  providers: [
    ApiService, // Provide ApiService here
  ],
})
export class ApiModule { }
