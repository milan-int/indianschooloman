import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationWizardComponent } from './features/registration/registration-wizard/registration-wizard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RegistrationWizardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Indian Schools in Oman - Registration';
}
