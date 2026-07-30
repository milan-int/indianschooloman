import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { RegistrationFormService } from '../../services/registration-form.service';

@Component({
  selector: 'app-step3-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step3-contact.component.html'
})
export class Step3ContactComponent {
  formService = inject(RegistrationFormService);
  
  @Input() postalCodes: any[] = [];

  get form(): FormGroup {
    return this.formService.form;
  }
}
