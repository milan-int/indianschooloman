import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegistrationDto } from '../models/registration.dto';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'https://localhost:7130/api/registration';

  submitRegistration(data: RegistrationDto): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }
}
