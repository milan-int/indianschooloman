import { Component, Input, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { RegistrationFormService } from '../../services/registration-form.service';
import { MasterDataResponse } from '../../../../core/models/master-data.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-step4-application',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step4-application.component.html'
})
export class Step4ApplicationComponent implements OnInit, OnDestroy {
  formService = inject(RegistrationFormService);
  fb = inject(FormBuilder);
  
  @Input() availableSchools: string[] = [];
  @Input() allSchools: string[] = [];
  @Input() relationships: MasterDataResponse[] = [];
  @Input() siblingRelationships: MasterDataResponse[] = [];
  @Input() preferenceCount: number = 0;
  @Input() availableClasses: string[] = [];
  @Input() siblingSchools: MasterDataResponse[] = [];
  @Input() siblingClasses: MasterDataResponse[] = [];
  
  divisions = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

  private sub1!: Subscription;
  private sub2!: Subscription;

  get form(): FormGroup {
    return this.formService.form;
  }

  get existingSiblings(): FormArray {
    return this.formService.existingSiblings;
  }

  get newApplicantSiblings(): FormArray {
    return this.formService.newApplicantSiblings;
  }

  get schoolPreferences(): FormArray {
    return this.formService.schoolPreferences;
  }

  ngOnInit() {
    this.sub1 = this.form.get('siblingsStudyingCount')!.valueChanges.subscribe(count => {
      this.updateExistingSiblings(Number(count));
    });

    this.sub2 = this.form.get('siblingsSeekingAdmissionCount')!.valueChanges.subscribe(count => {
      this.updateNewApplicantSiblings(Number(count));
    });
    
    // Initialize if they already have a value
    this.updateExistingSiblings(Number(this.form.get('siblingsStudyingCount')!.value));
    this.updateNewApplicantSiblings(Number(this.form.get('siblingsSeekingAdmissionCount')!.value));
  }

  ngOnDestroy() {
    if (this.sub1) this.sub1.unsubscribe();
    if (this.sub2) this.sub2.unsubscribe();
  }

  private updateExistingSiblings(count: number) {
    if (isNaN(count)) return;
    
    // If the count is 8+, let's cap it at 8 rows for the form
    const targetCount = count >= 8 ? 8 : count;
    
    const currentLength = this.existingSiblings.length;
    
    if (targetCount > currentLength) {
      for (let i = currentLength; i < targetCount; i++) {
        this.existingSiblings.push(this.fb.group({
          siblingName: ['', Validators.required],
          schoolName: ['', Validators.required],
          grNumber: ['', Validators.required],
          className: ['', Validators.required],
          division: ['', Validators.required]
        }));
      }
    } else if (targetCount < currentLength) {
      for (let i = currentLength - 1; i >= targetCount; i--) {
        this.existingSiblings.removeAt(i);
      }
    }
  }

  private updateNewApplicantSiblings(count: number) {
    if (isNaN(count)) return;
    
    const targetCount = count >= 8 ? 8 : count;
    
    const currentLength = this.newApplicantSiblings.length;
    
    if (targetCount > currentLength) {
      for (let i = currentLength; i < targetCount; i++) {
        this.newApplicantSiblings.push(this.fb.group({
          passportNo: ['', Validators.required],
          relationship: ['', Validators.required]
        }));
      }
    } else if (targetCount < currentLength) {
      for (let i = currentLength - 1; i >= targetCount; i--) {
        this.newApplicantSiblings.removeAt(i);
      }
    }
  }
}
