import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalLandingComponent } from './features/portal/portal-landing.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, PortalLandingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Indian Schools in Oman - Admission Portal';
}
