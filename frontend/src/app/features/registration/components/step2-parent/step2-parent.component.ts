import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { RegistrationFormService } from '../../services/registration-form.service';

@Component({
  selector: 'app-step2-parent',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step2-parent.component.html'
})
export class Step2ParentComponent {
  formService = inject(RegistrationFormService);
  
  @Input() relationships: any[] = [];
  @Input() nationalities: any[] = [];

  get form(): FormGroup {
    return this.formService.form;
  }
}
