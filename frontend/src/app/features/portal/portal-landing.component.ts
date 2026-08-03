import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { RegistrationWizardComponent } from '../registration/registration-wizard/registration-wizard.component';
import { PortalService } from '../../core/services/portal.service';
import { PortalLink } from '../../core/models/portal-link.model';

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
  private portalService = inject(PortalService);
  private sanitizer = inject(DomSanitizer);

  currentView: 'links' | 'guidelines' | 'wizard' = 'links';
  
  // Dynamic Links from Database
  admissionLinks: PortalLink[] = [];
  footerLinks: PortalLink[] = [];
  isLoadingLinks: boolean = true;
  linksError: string = '';

  // PDF / Document Viewer Modal State
  isPdfModalOpen: boolean = false;
  activeDoc: PortalLink | null = null;
  safeDocUrl: SafeResourceUrl | null = null;

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
    this.loadPortalLinks();
  }

  loadPortalLinks() {
    this.isLoadingLinks = true;
    this.linksError = '';
    this.portalService.getLandingData().subscribe({
      next: (data) => {
        this.admissionLinks = data.admissionLinks || [];
        this.footerLinks = data.footerLinks || [];
        this.isLoadingLinks = false;
      },
      error: (err) => {
        console.error('Failed to load portal links from API, using fallback', err);
        this.linksError = 'Failed to load live links from server. Showing standard links.';
        this.isLoadingLinks = false;
        // Fallback default links if API is offline
        this.admissionLinks = [
          { id: 1, title: 'NEW APPLICATION', section: 'ADMISSION_LINK', linkType: 'INTERNAL_ROUTE', targetUrl: '/register', description: 'Register a new student for Academic Year 2026–2027', displayOrder: 1, isActive: true, openInNewTab: false },
          { id: 2, title: 'Notice to Parents', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/notice_to_parents.pdf', description: 'Important announcements and eligibility criteria', displayOrder: 2, isActive: true, openInNewTab: false },
          { id: 3, title: 'Indian Schools Websites', section: 'ADMISSION_LINK', linkType: 'EXTERNAL_URL', targetUrl: 'https://indianschoolsoman.com', description: 'Direct portals to all capital area Indian schools', displayOrder: 3, isActive: true, openInNewTab: true },
          { id: 4, title: 'FAQ', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/faq.pdf', description: 'Find answers regarding admission procedures', displayOrder: 4, isActive: true, openInNewTab: false },
          { id: 5, title: 'Languages offered in Schools', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/languages_offered.pdf', description: 'Overview of 2nd & 3rd languages available per school', displayOrder: 5, isActive: true, openInNewTab: false },
          { id: 6, title: 'Inter-School Transfer', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/inter_school_transfer.pdf', description: 'Transfer guidelines between Indian schools in Oman', displayOrder: 6, isActive: true, openInNewTab: false },
          { id: 7, title: 'Admissions to Other Nationalities', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/admissions_other_nationalities.pdf', description: 'Registration guidelines for non-Indian passport holders', displayOrder: 7, isActive: true, openInNewTab: false },
          { id: 8, title: 'Projected Vacancies', section: 'ADMISSION_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/projected_vacancies.pdf', description: 'Check seat availability across all classes & schools', displayOrder: 8, isActive: true, openInNewTab: false }
        ];
        this.footerLinks = [
          { id: 9, title: 'Product Description', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/product_description.pdf', displayOrder: 1, isActive: true, openInNewTab: false },
          { id: 10, title: 'Privacy Policy', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/privacy_policy.pdf', displayOrder: 2, isActive: true, openInNewTab: false },
          { id: 11, title: 'Delivery Policy', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/delivery_policy.pdf', displayOrder: 3, isActive: true, openInNewTab: false },
          { id: 12, title: 'ContactUS', section: 'FOOTER_LINK', linkType: 'PDF_DOCUMENT', targetUrl: 'assets/docs/contact_us.pdf', displayOrder: 4, isActive: true, openInNewTab: false }
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

    // Default fallback
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
    document.body.style.overflow = 'hidden'; // prevent background scrolling
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
