import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFormService {
  readonly form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      // Pupil Details (Step 1)
      pupilFirstName: ['', Validators.required],
      pupilSurname: [''],
      passportNumber: ['', Validators.required],
      passportExpiryDate: [''],
      classSought: ['', Validators.required],
      sex: ['Male'],
      nationality: ['India'],
      visaNumber: [''],
      visaExpiryDate: [''],
      motherTongue: ['', Validators.required],
      
      // Previous School & Birth Info
      previousSchool: [''],
      country: [''],
      classLastAttended: [''],
      dateOfBirth: [''],
      placeOfBirth: ['', Validators.required],

      // Parent Details (Step 2)
      parentName: ['', Validators.required],
      parentSurname: [''],
      relationship: ['Father'],
      parentPassportNo: ['', Validators.required],
      civilNo: ['', Validators.required],
      employer: ['', Validators.required],
      occupation: ['', Validators.required],
      parentNationality: ['India'],
      
      // Contact & Address Info (Step 3)
      email: ['', [Validators.required, Validators.email]],
      gsm: ['', Validators.required],
      residentialPhone: [''],
      officePhone: [''],
      postalCode: ['Muscat-100'],
      poBox: ['', Validators.required],
      permanentAddress: ['', Validators.required],
      houseFlatNo: ['', Validators.required],
      wayNo: ['', Validators.required],
      streetName: ['', Validators.required],
      locality: ['', Validators.required],

      // Siblings & Preferences (Step 4)
      schoolPreferences: this.fb.array([]),
      siblingsStudyingCount: [0],
      siblingsSeekingAdmissionCount: [0],
      existingSiblings: this.fb.array([]),
      newApplicantSiblings: this.fb.array([]),

      // Declaration
      declaration: [false, Validators.requiredTrue]
    });
  }

  get existingSiblings() {
    return this.form.get('existingSiblings') as import('@angular/forms').FormArray;
  }

  get newApplicantSiblings() {
    return this.form.get('newApplicantSiblings') as import('@angular/forms').FormArray;
  }

  get schoolPreferences() {
    return this.form.get('schoolPreferences') as import('@angular/forms').FormArray;
  }

  updateSchoolPreferences(count: number) {
    // This is called when class changes. We just reset to 1 preference.
    this.schoolPreferences.clear();
    if (count > 0) {
      this.addSchoolPreference(true);
    }
  }

  addSchoolPreference(isRequired: boolean = false) {
    this.schoolPreferences.push(this.fb.control('', isRequired ? Validators.required : null));
  }

  removeSchoolPreference(index: number) {
    if (this.schoolPreferences.length > 1) {
      this.schoolPreferences.removeAt(index);
    }
  }

  resetForm() {
    this.existingSiblings.clear();
    this.newApplicantSiblings.clear();
    this.schoolPreferences.clear();
    this.form.reset({
      sex: 'Male',
      nationality: 'India',
      relationship: 'Father',
      parentNationality: 'India',
      postalCode: 'Muscat-100',
      siblingsStudyingCount: 0,
      siblingsSeekingAdmissionCount: 0
    });
  }
}
