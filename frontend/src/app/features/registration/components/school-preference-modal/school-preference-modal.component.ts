import { Component, Input, Output, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormArray } from '@angular/forms';
import { RegistrationFormService } from '../../services/registration-form.service';

@Component({
  selector: 'app-school-preference-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './school-preference-modal.component.html'
})
export class SchoolPreferenceModalComponent {
  formService = inject(RegistrationFormService);

  @Input() availableSchools: string[] = [];
  @Input() preferenceCount: number = 0;
  
  @Output() close = new EventEmitter<boolean>();

  get schoolPreferences(): FormArray {
    return this.formService.schoolPreferences;
  }

  addPreference() {
    if (this.schoolPreferences.length < this.preferenceCount) {
      this.formService.addSchoolPreference(false);
    }
  }

  removePreference(index: number) {
    this.formService.removeSchoolPreference(index);
  }

  isSchoolSelected(schoolName: string, currentIndex: number): boolean {
    const values = this.schoolPreferences.value as string[];
    return values.some((val, idx) => val === schoolName && idx !== currentIndex);
  }

  onSave() {
    const values = this.schoolPreferences.value as string[];
    
    // Check for empty selections
    if (values.some(v => !v)) {
      this.schoolPreferences.markAllAsTouched();
      return;
    }
    
    // Check for duplicates
    if (new Set(values).size !== values.length) {
      alert('You cannot select the same school more than once.');
      return;
    }

    if (this.schoolPreferences.valid) {
      this.close.emit(true);
    } else {
      this.schoolPreferences.markAllAsTouched();
    }
  }

  onCancel() {
    this.close.emit(false);
  }
}
