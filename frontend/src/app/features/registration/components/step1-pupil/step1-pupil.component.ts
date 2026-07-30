import { Component, Input, Output, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { RegistrationFormService } from '../../services/registration-form.service';
import { HttpClient } from '@angular/common/http';
import { catchError, map, of, debounceTime, switchMap } from 'rxjs';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-step1-pupil',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step1-pupil.component.html'
})
export class Step1PupilComponent {
  private http = inject(HttpClient);
  formService = inject(RegistrationFormService);
  
  @Input() classes: string[] = [];
  @Input() nationalities: any[] = [];
  @Input() languages: any[] = [];
  @Input() countries: any[] = [];
  @Input() genders: any[] = [];

  @Output() editPreferences = new EventEmitter<void>();

  get form(): FormGroup {
    return this.formService.form;
  }

  ngOnInit() {
    this.form.get('passportNumber')?.valueChanges.pipe(
      debounceTime(500),
      switchMap(passportNo => {
        if (!passportNo) return of(null);
        return this.http.get<{exists: boolean}>(`${environment.apiUrl}/Registration/check-passport/${passportNo}`)
          .pipe(catchError(() => of({exists: false})));
      })
    ).subscribe(result => {
      if (result && result.exists) {
        this.form.get('passportNumber')?.setErrors({ duplicate: true });
      } else {
        const errors = this.form.get('passportNumber')?.errors;
        if (errors) {
          delete errors['duplicate'];
          if (Object.keys(errors).length === 0) {
            this.form.get('passportNumber')?.setErrors(null);
          } else {
            this.form.get('passportNumber')?.setErrors(errors);
          }
        }
      }
    });
  }
}
