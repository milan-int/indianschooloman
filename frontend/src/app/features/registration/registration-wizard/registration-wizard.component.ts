import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { RegistrationFormService } from '../services/registration-form.service';
import { MasterDataService } from '../../../core/services/master-data.service';
import { RegistrationService } from '../../../core/services/registration.service';
import { GradeMaster } from '../../../core/models/master-data.model';
import { Step1PupilComponent } from '../components/step1-pupil/step1-pupil.component';
import { Step2ParentComponent } from '../components/step2-parent/step2-parent.component';
import { Step3ContactComponent } from '../components/step3-contact/step3-contact.component';
import { Step4ApplicationComponent } from '../components/step4-application/step4-application.component';
import { SchoolPreferenceModalComponent } from '../components/school-preference-modal/school-preference-modal.component';
import { PreviewModalComponent } from '../components/preview-modal/preview-modal.component';

@Component({
  selector: 'app-registration-wizard',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule,
    Step1PupilComponent,
    Step2ParentComponent,
    Step3ContactComponent,
    Step4ApplicationComponent,
    SchoolPreferenceModalComponent,
    PreviewModalComponent
  ],
  templateUrl: './registration-wizard.component.html'
})
export class RegistrationWizardComponent implements OnInit {
  formService = inject(RegistrationFormService);
  masterData = inject(MasterDataService);
  registrationService = inject(RegistrationService);

  currentStep = 1;
  successMessage = '';
  errorMessage = '';
  isSubmitting = false;
  showPreferencesModal = false;
  showPreviewModal = false;

  grades: GradeMaster[] = [];
  classes: string[] = [];
  availableSchools: string[] = [];
  allSchoolsMaster: string[] = [];
  siblingSchools: any[] = [];
  siblingClasses: any[] = [];
  preferenceCount = 2;

  // Master Data
  postalCodes: any[] = [];
  languages: any[] = [];
  relationships: any[] = [];
  siblingRelationships: any[] = [];
  nationalities: any[] = [];
  countries: any[] = [];
  genders: any[] = [];

  get form(): FormGroup {
    return this.formService.form;
  }

  ngOnInit() {
    this.masterData.getGrades().subscribe(data => {
      this.grades = data;
      this.classes = data.map(g => g.gradeDisplay);
      
      const uniqueSchools = new Set<string>();
      data.forEach(grade => {
        if (grade.schools) {
          grade.schools.forEach(s => uniqueSchools.add(s.schoolName));
        }
      });
      this.allSchoolsMaster = Array.from(uniqueSchools).sort();
    });

    this.form.get('classSought')?.valueChanges.subscribe(selectedClass => {
      if (!selectedClass) {
        this.availableSchools = [];
        this.preferenceCount = 2;
        this.formService.updateSchoolPreferences(this.preferenceCount);
        return;
      }
      
      const grade = this.grades.find(g => g.gradeDisplay === selectedClass);
      if (grade) {
        this.availableSchools = grade.schools ? grade.schools.map((s: any) => s.schoolName) : [];
        this.preferenceCount = grade.preferenceType || 2;
      } else {
        this.availableSchools = [];
        this.preferenceCount = 2;
      }
      
      // Update form service to instantiate 1 required preference
      this.formService.updateSchoolPreferences(this.preferenceCount);
      // Immediately popup the modal (using setTimeout to avoid ExpressionChangedAfterItHasBeenCheckedError)
      setTimeout(() => {
        this.showPreferencesModal = true;
      });
    });

    this.masterData.getCountries().subscribe(data => this.countries = data);
    this.masterData.getMotherTongues().subscribe(data => this.languages = data);
    this.masterData.getRelationships().subscribe(data => {
      const allowed = ['Father', 'Mother', 'GrandFather', 'GrandMother'];
      this.relationships = data.filter(r => allowed.includes(r.name));
    });
    this.masterData.getSiblingRelationships().subscribe(data => this.siblingRelationships = data);
    this.masterData.getNationalities().subscribe(data => this.nationalities = data);
    this.masterData.getPostalCodes().subscribe(data => {
      this.postalCodes = data;
      if (data && data.length > 0 && !this.form.get('postalCode')?.value) {
        this.form.patchValue({ postalCode: data[0].code });
      }
    });
    this.masterData.getSiblingSchools().subscribe(data => this.siblingSchools = data);
    this.masterData.getSiblingClasses().subscribe(data => this.siblingClasses = data);
    this.masterData.getGenders().subscribe(data => {
      this.genders = data;
      // Default to first gender if not set
      if (data && data.length > 0 && !this.form.get('sex')?.value) {
        this.form.patchValue({ sex: data[0].name });
      }
    });
  }

  onPreferencesModalClose(saved: boolean) {
    this.showPreferencesModal = false;
    if (!saved) {
      // Revert class selection since preference selection is mandatory
      this.form.patchValue({ classSought: '' }, { emitEvent: false });
      this.formService.updateSchoolPreferences(2);
      this.availableSchools = [];
    }
  }

  nextStep() {
    if (this.currentStep < 4) {
      this.form.markAllAsTouched();
      this.currentStep++;
      window.scrollTo(0, 0);
    }
  }

  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
      window.scrollTo(0, 0);
    }
  }

  onSubmit() {
    if (this.form.valid) {
      this.showPreviewModal = true;
    } else {
      this.form.markAllAsTouched();
    }
  }

  onConfirmSubmit() {
    this.isSubmitting = true;
    this.successMessage = '';
    this.errorMessage = '';
    
    this.registrationService.submitRegistration(this.form.value).subscribe({
      next: () => {
        this.successMessage = 'Registration submitted successfully!';
        this.formService.resetForm();
        this.currentStep = 1;
        this.isSubmitting = false;
        this.showPreviewModal = false;
        window.scrollTo(0, 0);
      },
      error: (err: any) => {
        console.error(err);
        this.errorMessage = 'There was an error submitting the registration.';
        this.isSubmitting = false;
        this.showPreviewModal = false;
        window.scrollTo(0, 0);
      }
    });
  }
}
