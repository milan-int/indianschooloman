import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationWizardComponent } from '../registration/registration-wizard/registration-wizard.component';

interface SchoolInfo {
  slNo: number;
  name: string;
  syllabus: string;
  classes: string;
}

@Component({
  selector: 'app-portal-landing',
  standalone: true,
  imports: [CommonModule, FormsModule, RegistrationWizardComponent],
  templateUrl: './portal-landing.component.html',
  styleUrl: './portal-landing.component.css'
})
export class PortalLandingComponent implements OnInit {
  currentView: 'links' | 'guidelines' | 'wizard' = 'links';
  
  // Passport check for guidelines
  hasIndianPassport: boolean = true;

  // Login form model
  loginUsername: string = '';
  loginPassword: string = '';
  captchaInput: string = '';
  captchaCode: string = '';
  loginErrorMessage: string = '';

  // Schools list for table
  schools: SchoolInfo[] = [
    { slNo: 1, name: 'Indian School Muscat (ISM)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 2, name: 'Indian School Darsait (ISD)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 3, name: 'Indian School Al Wadi Al Kabir (ISWK)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 4, name: 'Indian School Al Wadi Al Kabir (ISWK International)', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI' },
    { slNo: 5, name: 'Indian School Al Ghubra (ISG)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 6, name: 'Indian School Al Ghubra (ISG International)', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI' },
    { slNo: 7, name: 'Indian School Bousher (ISB)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 8, name: 'Indian School Seeb (ISAS)', syllabus: 'CBSE', classes: 'KG I – IX & XI' },
    { slNo: 9, name: 'Indian School Maabela (ISAM)', syllabus: 'CBSE', classes: 'KG I – IX & XI' }
  ];

  // Guidelines list
  guidelines: string[] = [
    'This online registration form is meant for Indian Nationals seeking new admissions in Indian Schools in the capital area for the academic year 2026-2027.',
    'Online registration is mandatory. There is only one application form required for one child; system will not accept any duplication.',
    'A unique login registration number and password will be generated on submission of the application form. This will be sent to the email address and mobile number given in the application form.',
    'A non-refundable processing fee of OMR 15/- will be payable on each application.',
    'Online application form is required even for sibling\'s admission. To get the sibling preference in the same school, parent should essentially choose the same school as the first option.',
    'Tentative vacancies in different schools posted on the site may be checked by parents to know the number of seats available in different classes.',
    'The choice of Schools is limited to the availability of seats in the class for which admission is sought.',
    'Parents are advised to go through Frequently Asked Questions (FAQs) section for more clarification, if required.',
    'Parents seeking inter-school transfers of their wards within Schools in the capital area are required to fill the \'Inter-School Transfer Application form\'.',
    'Parents of other nationalities seeking admissions in Indian Schools in the capital area are required to fill the \'Admission to Other Nationalities form\'.'
  ];

  ngOnInit() {
    this.generateCaptcha();
  }

  generateCaptcha() {
    const chars = '23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz';
    let code = '';
    for (let i = 0; i < 6; i++) {
      code += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    this.captchaCode = code;
    this.captchaInput = '';
  }

  onLogin() {
    this.loginErrorMessage = '';
    if (!this.loginUsername || !this.loginPassword) {
      this.loginErrorMessage = 'Please enter both Username and Password.';
      return;
    }
    if (this.captchaInput.toLowerCase() !== this.captchaCode.toLowerCase()) {
      this.loginErrorMessage = 'Incorrect captcha code. Please try again.';
      this.generateCaptcha();
      return;
    }
    alert(`Login attempted for Registration No: ${this.loginUsername}`);
  }

  openNewApplication() {
    this.currentView = 'guidelines';
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  startRegistration() {
    if (!this.hasIndianPassport) {
      alert('Non-Indian passport holders should use the Admission to Other Nationalities portal.');
      return;
    }
    this.currentView = 'wizard';
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  goToLinks() {
    this.currentView = 'links';
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
