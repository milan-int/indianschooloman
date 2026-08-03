import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { RegistrationWizardComponent } from '../registration/registration-wizard/registration-wizard.component';
import { PortalService } from '../../core/services/portal.service';
import { PortalLink, SchoolInfo, GuidelineInstruction, PortalContact } from '../../core/models/portal-link.model';

@Component({
  selector: 'app-portal-landing',
  standalone: true,
  imports: [CommonModule, FormsModule, RegistrationWizardComponent],
  templateUrl: './portal-landing.component.html',
  styleUrl: './portal-landing.component.css'
})
export class PortalLandingComponent implements OnInit {
  private portalService = inject(PortalService);
  private sanitizer = inject(DomSanitizer);

  currentView: 'links' | 'guidelines' | 'wizard' = 'links';
  activeAuthTab: 'login' | 'track' = 'login';
  schoolFilter: 'ALL' | 'CBSE' | 'CAMBRIDGE' = 'ALL';
  linkSearchQuery: string = '';

  // Dynamic Data from Backend API
  admissionLinks: PortalLink[] = [];
  footerLinks: PortalLink[] = [];
  schools: SchoolInfo[] = [];
  guidelines: GuidelineInstruction[] = [];
  contact: PortalContact = {
    helplinePhone: '+968 2470 2567 / 2479 9700',
    helplineEmail: 'admissions@indianschoolsoman.com',
    officeHours: 'Sunday to Thursday (8:00 AM – 2:00 PM)',
    academicYear: '2026–2027'
  };

  isLoadingData: boolean = true;
  apiError: string = '';

  // PDF / Document Viewer Modal State
  isPdfModalOpen: boolean = false;
  activeDoc: PortalLink | null = null;
  safeDocUrl: SafeResourceUrl | null = null;

  // Passport & Eligibility verification
  hasIndianPassport: boolean = true;
  hasConfirmedGuidelines: boolean = true;

  // Login form model
  loginUsername: string = '';
  loginPassword: string = '';
  showPassword: boolean = false;
  captchaInput: string = '';
  captchaCode: string = '';
  loginErrorMessage: string = '';
  loginSuccessMessage: string = '';

  // Track Status model
  trackRegNo: string = '';
  trackMobile: string = '';
  trackStatusResult: string = '';

  ngOnInit() {
    this.generateCaptcha();
    this.loadBackendData();
  }

  get filteredSchools(): SchoolInfo[] {
    if (this.schoolFilter === 'ALL') return this.schools;
    return this.schools.filter(s => s.syllabus.toUpperCase() === this.schoolFilter);
  }

  get filteredLinks(): PortalLink[] {
    if (!this.linkSearchQuery.trim()) return this.admissionLinks;
    const q = this.linkSearchQuery.toLowerCase();
    return this.admissionLinks.filter(l => 
      l.title.toLowerCase().includes(q) || (l.description && l.description.toLowerCase().includes(q))
    );
  }

  loadBackendData() {
    this.isLoadingData = true;
    this.apiError = '';
    this.portalService.getLandingData().subscribe({
      next: (data) => {
        this.admissionLinks = data.admissionLinks || [];
        this.footerLinks = data.footerLinks || [];
        this.schools = data.schools || [];
        this.guidelines = data.guidelines || [];
        if (data.contact) {
          this.contact = data.contact;
        }
        this.isLoadingData = false;
      },
      error: (err) => {
        console.warn('Backend API connection failed, loading fallback data', err);
        this.isLoadingData = false;
        // Fallback default links
        this.admissionLinks = [
          { id: 1, title: 'NEW APPLICATION', section: 'ADMISSION_LINK', linkType: 'INTERNAL_ROUTE', targetUrl: '/register', description: 'Register a new student for Academic Year 2026–2027', displayOrder: 1, isActive: true, openInNewTab: false },
          { id: 2, title: 'Notice to Parents', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/notice_to_parents.pdf', description: 'Official circular on eligibility & documents required', displayOrder: 2, isActive: true, openInNewTab: false },
          { id: 3, title: 'Indian Schools Directory & Websites', section: 'ADMISSION_LINK', linkType: 'EXTERNAL_URL', targetUrl: 'https://indianschoolsoman.com', description: 'Official campus portals and contact directories', displayOrder: 3, isActive: true, openInNewTab: true },
          { id: 4, title: 'Frequently Asked Questions (FAQ)', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/faq.pdf', description: 'Clear answers on admission steps, age criteria & fees', displayOrder: 4, isActive: true, openInNewTab: false },
          { id: 5, title: 'Languages Offered in Schools', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/languages_offered.pdf', description: '2nd & 3rd Language choices available per school campus', displayOrder: 5, isActive: true, openInNewTab: false },
          { id: 6, title: 'Inter-School Transfer Guidelines', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/inter_school_transfer.pdf', description: 'Procedure for transferring between capital area schools', displayOrder: 6, isActive: true, openInNewTab: false },
          { id: 7, title: 'Admissions to Other Nationalities', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/admissions_other_nationalities.pdf', description: 'Eligibility and intake criteria for expatriate children', displayOrder: 7, isActive: true, openInNewTab: false },
          { id: 8, title: 'Projected Seat Vacancies Matrix', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/projected_vacancies.pdf', description: 'Live overview of available seats by grade and campus', displayOrder: 8, isActive: true, openInNewTab: false }
        ];
        this.footerLinks = [
          { id: 9, title: 'Product Description', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/annexure_a.pdf', displayOrder: 1, isActive: true, openInNewTab: false },
          { id: 10, title: 'Privacy & Security Policy', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/notice_to_parents.pdf', displayOrder: 2, isActive: true, openInNewTab: false },
          { id: 11, title: 'Payment & Delivery Policy', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/annexure_a.pdf', displayOrder: 3, isActive: true, openInNewTab: false },
          { id: 12, title: 'Official Helpdesk & Contact', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/indian_schools_websites.pdf', displayOrder: 4, isActive: true, openInNewTab: false }
        ];
        this.schools = [
          { slNo: 1, name: 'Indian School Muscat', code: 'ISM', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Darsait / Muscat', website: 'https://ismoman.com' },
          { slNo: 2, name: 'Indian School Darsait', code: 'ISD', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Darsait', website: 'https://isdoman.com' },
          { slNo: 3, name: 'Indian School Al Wadi Al Kabir', code: 'ISWK', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Wadi Kabir', website: 'https://iswkoman.com' },
          { slNo: 4, name: 'Indian School Al Wadi Al Kabir International', code: 'ISWKi', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI', location: 'Wadi Kabir', website: 'https://iswkoman.com' },
          { slNo: 5, name: 'Indian School Al Ghubra', code: 'ISG', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Ghubra', website: 'https://isgoman.com' },
          { slNo: 6, name: 'Indian School Al Ghubra International', code: 'ISGi', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI', location: 'Al Ghubra', website: 'https://isgoman.com' },
          { slNo: 7, name: 'Indian School Bousher', code: 'ISB', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Bousher', website: 'https://isboman.com' },
          { slNo: 8, name: 'Indian School Seeb', code: 'ISAS', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Seeb', website: 'https://isseeoman.com' },
          { slNo: 9, name: 'Indian School Maabela', code: 'ISAM', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Maabela', website: 'https://isamoman.com' }
        ];
        this.guidelines = [
          { id: 1, title: 'Eligibility', detail: 'This online registration form is meant for Indian Nationals seeking new admissions in Indian Schools in the capital area for the academic year 2026-2027.' },
          { id: 2, title: 'Single Mandatory Application', detail: 'Online registration is mandatory. There is only one application form required for one child; our system will not accept duplicate passport entries.' },
          { id: 3, title: 'Credentials & Notifications', detail: 'A unique login registration number and password will be generated automatically upon submission and sent to your registered email and mobile number.' },
          { id: 4, title: 'Application Processing Fee', detail: 'A non-refundable processing fee of OMR 15/- is payable upon successful submission of the application form.' },
          { id: 5, title: 'Sibling Preference Rule', detail: 'Online application is mandatory even for sibling admissions. To claim sibling preference, the parent must select the sibling\'s school as their First Preference.' },
          { id: 6, title: 'Seat Vacancies', detail: 'Tentative vacancies across different schools are dynamically updated on the portal for parents to review before submitting preferences.' },
          { id: 7, title: 'Admission Allotment', detail: 'School allotment is strictly subject to vacancy availability and merit criteria set by the Board of Directors.' },
          { id: 8, title: 'Help & Queries', detail: 'Parents are strongly advised to check the Frequently Asked Questions (FAQs) section for guidance on common registration questions.' },
          { id: 9, title: 'Inter-School Transfer', detail: 'Parents seeking inter-school transfer for their wards must complete the dedicated transfer portal:', link: 'https://forms.gle/P29avN2BoVufqWGz5', linkText: 'Inter-School Transfer Form' },
          { id: 10, title: 'Other Nationalities', detail: 'Parents of non-Indian nationalities seeking admission in Indian schools must apply through the external foreign quota portal:', link: 'https://forms.gle/hEUAnuLePfyTveD89', linkText: 'Other Nationalities Form' }
        ];
      }
    });
  }

  handleLinkClick(link: PortalLink) {
    if (!link.isActive) return;

    if (link.linkType === 'INTERNAL_ROUTE' || link.title.toUpperCase().includes('NEW APPLICATION')) {
      this.openNewApplication();
      return;
    }

    if (link.linkType === 'EXTERNAL_URL' || link.openInNewTab) {
      window.open(link.targetUrl, '_blank');
      return;
    }

    if (link.linkType === 'PDF_DOCUMENT') {
      this.openPdfModal(link);
      return;
    }

    if (link.targetUrl.startsWith('http')) {
      window.open(link.targetUrl, '_blank');
    } else {
      this.openPdfModal(link);
    }
  }

  openPdfModal(link: PortalLink) {
    this.activeDoc = link;
    this.safeDocUrl = this.sanitizer.bypassSecurityTrustResourceUrl(link.targetUrl);
    this.isPdfModalOpen = true;
    document.body.style.overflow = 'hidden';
  }

  closePdfModal() {
    this.isPdfModalOpen = false;
    this.activeDoc = null;
    this.safeDocUrl = null;
    document.body.style.overflow = '';
  }

  openDocInNewTab() {
    if (this.activeDoc?.targetUrl) {
      window.open(this.activeDoc.targetUrl, '_blank');
    }
  }

  generateCaptcha() {
    const chars = '23456789ABCDEFGHJKLMNPQRSTUVWXYZ';
    let code = '';
    for (let i = 0; i < 5; i++) {
      code += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    this.captchaCode = code;
    this.captchaInput = '';
  }

  onLogin() {
    this.loginErrorMessage = '';
    this.loginSuccessMessage = '';
    if (!this.loginUsername || !this.loginPassword) {
      this.loginErrorMessage = 'Please enter both Registration No. and Password.';
      return;
    }
    if (this.captchaInput.toUpperCase() !== this.captchaCode) {
      this.loginErrorMessage = 'Invalid Security Code. A new code has been generated.';
      this.generateCaptcha();
      return;
    }
    this.loginSuccessMessage = `Logging in for Registration No: ${this.loginUsername}...`;
  }

  onTrackStatus() {
    if (!this.trackRegNo || !this.trackMobile) {
      this.trackStatusResult = 'Please enter your Application Reference No. and Registered Mobile.';
      return;
    }
    this.trackStatusResult = `Searching record for ${this.trackRegNo}... Status: Under Initial Verification.`;
  }

  openNewApplication() {
    this.currentView = 'guidelines';
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  startRegistration() {
    if (!this.hasIndianPassport) {
      alert('Non-Indian passport holders should use the Admission to Other Nationalities external form.');
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
