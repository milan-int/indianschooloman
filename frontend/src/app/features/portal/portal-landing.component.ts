import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { RegistrationWizardComponent } from '../registration/registration-wizard/registration-wizard.component';
import { PortalService } from '../../core/services/portal.service';
import { AuthService } from '../../core/services/auth.service';
import { PortalLink, SchoolInfo, GuidelineInstruction, PortalConfig, PortalContact } from '../../core/models/portal-link.model';
import { UserAccount, AdminDashboardStats, ApplicantSummary } from '../../core/models/auth.model';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-portal-landing',
  standalone: true,
  imports: [CommonModule, FormsModule, RegistrationWizardComponent],
  templateUrl: './portal-landing.component.html',
  styleUrl: './portal-landing.component.css'
})
export class PortalLandingComponent implements OnInit {
  private portalService = inject(PortalService);
  private authService = inject(AuthService);
  private sanitizer = inject(DomSanitizer);

  currentView: 'links' | 'guidelines' | 'admin' | 'client' | 'wizard' = 'links';
  activeAuthTab: 'login' | 'track' = 'login';
  schoolFilter: 'ALL' | 'CBSE' | 'CAMBRIDGE' = 'ALL';
  linkSearchQuery: string = '';

  // Current authenticated user session
  currentUser: UserAccount | null = null;

  // Dynamic Data from Backend API (Active Only for Public Views)
  admissionLinks: PortalLink[] = [];
  footerLinks: PortalLink[] = [];
  schools: SchoolInfo[] = [];
  guidelines: GuidelineInstruction[] = [];
  contact: PortalContact = {
    helplinePhone: '+968 2470 2567 / 2479 9700',
    helplineEmail: 'admissions@indianschoolsoman.com',
    officeHours: 'Sunday to Thursday (8:00 AM – 2:00 PM)',
    academicYear: '2026–2027',
    logoUrl: 'assets/logo.png',
    brandTitle: 'Indian Schools Oman',
    brandSubTitle: 'Central Admission System'
  };

  isLoadingData: boolean = true;
  apiError: string = '';

  // Admin Master & Application Suite State
  adminTab: 'applications' | 'guidelines' | 'schools' | 'links' | 'configs' | 'users' = 'applications';
  adminStats: AdminDashboardStats | null = null;
  adminApplications: ApplicantSummary[] = [];
  isLoadingAdmin: boolean = false;
  isLoadingApps: boolean = false;
  adminNotification: { message: string; type: 'success' | 'error' } | null = null;

  // Admin Application Filters & Search
  appSearchTerm: string = '';
  appFilterStatus: string = 'ALL';
  appFilterSchool: string = 'ALL';
  appFilterClass: string = 'ALL';

  // Search terms for dedicated admin pages
  guidelineSearchTerm: string = '';
  guidelinesViewMode: 'cards' | 'table' = 'cards';
  guidelineCategoryFilter: string = 'ALL';
  selectedPreviewGuideline: GuidelineInstruction | null = null;
  showGuidelinePreviewModal: boolean = false;
  schoolSearchTerm: string = '';
  linkSearchTerm: string = '';
  configSearchTerm: string = '';
  userSearchTerm: string = '';

  // Applicant Inspector Dedicated Page View
  showApplicantModal: boolean = false;
  isViewingApplicantPage: boolean = false;
  isLoadingApplicantDetail: boolean = false;
  selectedApplicant: any = null;
  statusUpdateRemarks: string = '';
  isUpdatingStatus: boolean = false;
  applicantModalPage: number = 1;

  // Admin Users List
  allUsers: UserAccount[] = [];

  // Admin Master Content Lists
  allGuidelines: GuidelineInstruction[] = [];
  allSchools: SchoolInfo[] = [];
  allLinks: PortalLink[] = [];
  allConfigs: PortalConfig[] = [];

  get adminFilteredGuidelines() {
    let list = this.allGuidelines;
    if (this.guidelineCategoryFilter && this.guidelineCategoryFilter !== 'ALL') {
      const cat = this.guidelineCategoryFilter.toUpperCase();
      list = list.filter(g => this.getGuidelineType(g.title) === cat || this.getGuidelineCategory(g.title).toUpperCase().includes(cat));
    }
    if (this.guidelineSearchTerm) {
      const term = this.guidelineSearchTerm.toLowerCase();
      list = list.filter(g => 
        (g.title && g.title.toLowerCase().includes(term)) || 
        (g.detail && g.detail.toLowerCase().includes(term)) ||
        (g.linkText && g.linkText.toLowerCase().includes(term))
      );
    }
    return list;
  }

  get activeGuidelinesCount(): number {
    return this.allGuidelines.filter(g => g.isActive).length;
  }

  get mandatoryGuidelinesCount(): number {
    return this.allGuidelines.filter(g => this.getGuidelineType(g.title) === 'MANDATORY').length;
  }

  get linkedGuidelinesCount(): number {
    return this.allGuidelines.filter(g => !!g.link).length;
  }

  getGuidelineIcon(title: string): string {
    const t = (title || '').toLowerCase();
    if (t.includes('eligib')) return '📋';
    if (t.includes('single') || t.includes('mandatory')) return '📜';
    if (t.includes('credential') || t.includes('notif') || t.includes('password') || t.includes('login')) return '🔐';
    if (t.includes('fee') || t.includes('pay') || t.includes('omr')) return '💳';
    if (t.includes('sibling') || t.includes('family')) return '👨‍👩‍👧';
    if (t.includes('transfer') || t.includes('inter-school')) return '🔄';
    if (t.includes('nationalit') || t.includes('expat')) return '🌍';
    if (t.includes('vacanc') || t.includes('seat') || t.includes('matrix')) return '📊';
    if (t.includes('age') || t.includes('criteria')) return '🎂';
    if (t.includes('document') || t.includes('upload') || t.includes('passport')) return '📁';
    return '💡';
  }

  getGuidelineType(title: string): string {
    const t = (title || '').toLowerCase();
    if (t.includes('mandatory') || t.includes('single') || t.includes('fee') || t.includes('passport')) return 'MANDATORY';
    if (t.includes('eligib') || t.includes('age')) return 'ELIGIBILITY';
    if (t.includes('sibling') || t.includes('transfer') || t.includes('nationalit')) return 'POLICY';
    if (t.includes('credential') || t.includes('login') || t.includes('notif')) return 'SYSTEM';
    return 'GENERAL';
  }

  getGuidelineCategory(title: string): string {
    const t = (title || '').toLowerCase();
    if (t.includes('eligib')) return 'Eligibility Criteria';
    if (t.includes('single') || t.includes('mandatory')) return 'Admission Compliance';
    if (t.includes('credential') || t.includes('notif')) return 'Account & Security';
    if (t.includes('fee') || t.includes('omr')) return 'Financial & Processing';
    if (t.includes('sibling')) return 'Sibling Priority';
    if (t.includes('transfer')) return 'Campus Transfer';
    if (t.includes('nationalit')) return 'Expatriate Admission';
    if (t.includes('vacanc') || t.includes('seat')) return 'Capacity Matrix';
    return 'General Guidance';
  }

  openGuidelinePreview(g: GuidelineInstruction) {
    this.selectedPreviewGuideline = g;
    this.showGuidelinePreviewModal = true;
  }

  closeGuidelinePreview() {
    this.selectedPreviewGuideline = null;
    this.showGuidelinePreviewModal = false;
  }

  get adminFilteredSchools() {
    if (!this.schoolSearchTerm) return this.allSchools;
    const term = this.schoolSearchTerm.toLowerCase();
    return this.allSchools.filter(s => 
      (s.name && s.name.toLowerCase().includes(term)) || 
      (s.code && s.code.toLowerCase().includes(term)) || 
      (s.location && s.location.toLowerCase().includes(term)) ||
      (s.syllabus && s.syllabus.toLowerCase().includes(term)) ||
      (s.classes && s.classes.toLowerCase().includes(term))
    );
  }

  get adminFilteredLinks() {
    if (!this.linkSearchTerm) return this.allLinks;
    const term = this.linkSearchTerm.toLowerCase();
    return this.allLinks.filter(l => 
      (l.title && l.title.toLowerCase().includes(term)) || 
      (l.section && l.section.toLowerCase().includes(term)) ||
      (l.targetUrl && l.targetUrl.toLowerCase().includes(term)) ||
      (l.description && l.description.toLowerCase().includes(term))
    );
  }

  get adminFilteredConfigs() {
    if (!this.configSearchTerm) return this.allConfigs;
    const term = this.configSearchTerm.toLowerCase();
    return this.allConfigs.filter(c => 
      (c.configKey && c.configKey.toLowerCase().includes(term)) || 
      (c.configValue && c.configValue.toLowerCase().includes(term)) ||
      (c.description && c.description.toLowerCase().includes(term))
    );
  }

  get adminFilteredUsers() {
    if (!this.userSearchTerm) return this.allUsers;
    const term = this.userSearchTerm.toLowerCase();
    return this.allUsers.filter(u => 
      (u.username && u.username.toLowerCase().includes(term)) || 
      (u.fullName && u.fullName.toLowerCase().includes(term)) ||
      (u.email && u.email.toLowerCase().includes(term)) ||
      (u.role && u.role.toLowerCase().includes(term)) ||
      (u.phoneNumber && u.phoneNumber.toLowerCase().includes(term))
    );
  }

  // Guideline Modal State
  showGuidelineModal: boolean = false;
  isEditingGuideline: boolean = false;
  guidelineForm: Partial<GuidelineInstruction> = {
    title: '',
    detail: '',
    link: '',
    linkText: '',
    displayOrder: 1,
    isActive: true
  };

  // School Modal State
  showSchoolModal: boolean = false;
  isEditingSchool: boolean = false;
  schoolForm: Partial<SchoolInfo> = {
    slNo: 1,
    name: '',
    code: '',
    syllabus: 'CBSE',
    classes: 'KG I – IX & XI',
    location: '',
    website: '',
    displayOrder: 1,
    isActive: true
  };

  // Link Modal State
  showLinkModal: boolean = false;
  isEditingLink: boolean = false;
  linkForm: Partial<PortalLink> = {
    title: '',
    section: 'ADMISSION_LINK',
    linkType: 'PDF_DOCUMENT',
    targetUrl: '',
    description: '',
    displayOrder: 1,
    isActive: true,
    openInNewTab: false
  };

  // Config Edit State
  editingConfigKey: string | null = null;
  configForm = {
    configValue: '',
    description: '',
    isActive: true
  };

  // Client Dashboard State
  clientApplication: any = null;
  isLoadingClient: boolean = false;

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
  isLoggingIn: boolean = false;

  // Track Status model
  trackRegNo: string = '';
  trackMobile: string = '';
  trackStatusResult: string = '';

  ngOnInit() {
    this.generateCaptcha();
    this.loadBackendData();

    // Subscribe to Auth State
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user) {
        if (user.role === 'ADMIN') {
          this.currentView = 'admin';
          this.loadAdminData();
        } else if (user.role === 'CLIENT') {
          this.currentView = 'client';
          this.loadClientData();
        }
      }
    });
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
        this.loadFallbackData();
      }
    });
  }

  // ==================== AUTHENTICATION ACTIONS ====================

  onLogin() {
    this.loginErrorMessage = '';
    this.loginSuccessMessage = '';

    if (!this.loginUsername.trim() || !this.loginPassword.trim()) {
      this.loginErrorMessage = 'Please enter both Username / Registration No. and Password.';
      return;
    }

    if (this.captchaInput.toUpperCase() !== this.captchaCode) {
      this.loginErrorMessage = 'Invalid Security Code. A new code has been generated.';
      this.generateCaptcha();
      return;
    }

    this.isLoggingIn = true;
    this.authService.login({
      username: this.loginUsername.trim(),
      password: this.loginPassword.trim()
    }).subscribe({
      next: (res) => {
        this.isLoggingIn = false;
        this.loginSuccessMessage = res.message;
        if (res.user?.role === 'ADMIN') {
          this.currentView = 'admin';
          this.loadAdminData();
        } else {
          this.currentView = 'client';
          this.loadClientData();
        }
      },
      error: (err) => {
        this.isLoggingIn = false;
        this.loginErrorMessage = err.error?.message || 'Login failed. Please verify credentials.';
        this.generateCaptcha();
      }
    });
  }

  onLogout() {
    this.authService.logout();
    this.currentUser = null;
    this.currentView = 'links';
    this.loginUsername = '';
    this.loginPassword = '';
    this.generateCaptcha();
    this.notify('You have been logged out successfully.');
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  onTrackStatus() {
    if (!this.trackRegNo || !this.trackMobile) {
      this.trackStatusResult = 'Please enter your Application Reference No. and Registered Mobile.';
      return;
    }
    this.trackStatusResult = `Searching record for ${this.trackRegNo}... Status: Under Initial Verification.`;
  }

  // ==================== ADMIN SUITE ====================

  loadAdminData() {
    this.isLoadingAdmin = true;
    this.loadAdminStats();
    this.loadAdminApplications();
    this.loadMasterContent();
    this.loadUsers();
  }

  loadAdminStats() {
    this.authService.getAdminStats().subscribe({
      next: (stats) => { this.adminStats = stats; }
    });
  }

  loadAdminApplications() {
    this.isLoadingApps = true;
    this.authService.getAdminApplications(
      this.appSearchTerm,
      this.appFilterSchool,
      this.appFilterStatus,
      this.appFilterClass
    ).subscribe({
      next: (data) => {
        this.adminApplications = data;
        this.isLoadingApps = false;
      },
      error: () => { this.isLoadingApps = false; }
    });
  }

  loadMasterContent() {
    this.portalService.getAllGuidelines(false).subscribe({
      next: (data) => { this.allGuidelines = data; }
    });
    this.portalService.getAllSchools(false).subscribe({
      next: (data) => { this.allSchools = data; }
    });
    this.portalService.getAllLinks(false).subscribe({
      next: (data) => { this.allLinks = data; }
    });
    this.portalService.getAllConfigs().subscribe({
      next: (data) => {
        this.allConfigs = data;
        this.isLoadingAdmin = false;
      },
      error: () => { this.isLoadingAdmin = false; }
    });
  }

  loadUsers() {
    this.authService.getAllUsers().subscribe({
      next: (data) => { this.allUsers = data; }
    });
  }

  inspectApplicant(id: number) {
    this.selectedApplicant = null;
    this.applicantModalPage = 1;
    this.isViewingApplicantPage = true;
    this.isLoadingApplicantDetail = true;
    this.showApplicantModal = false; // Disable any popup modal
    this.authService.getApplicationDetails(id).subscribe({
      next: (data) => {
        this.selectedApplicant = data;
        this.isLoadingApplicantDetail = false;
      },
      error: () => {
        this.notify('Failed to load application details.', 'error');
        this.isViewingApplicantPage = false;
        this.isLoadingApplicantDetail = false;
      }
    });
  }

  setApplicantModalPage(page: number) {
    this.applicantModalPage = page;
  }

  nextApplicantModalPage() {
    if (this.applicantModalPage < 4) {
      this.applicantModalPage++;
    }
  }

  prevApplicantModalPage() {
    if (this.applicantModalPage > 1) {
      this.applicantModalPage--;
    }
  }

  closeApplicantModal() {
    this.isViewingApplicantPage = false;
    this.showApplicantModal = false;
    this.selectedApplicant = null;
    this.statusUpdateRemarks = '';
  }

  printApplicantDossier() {
    window.print();
  }

  getApplicantPreferences(app: any): Array<{ order: number, name: string }> {
    if (!app) return [];
    if (Array.isArray(app.schoolPreferences) && app.schoolPreferences.length > 0) {
      return app.schoolPreferences.map((p: any, idx: number) => {
        if (typeof p === 'string') {
          return { order: idx + 1, name: p };
        }
        return {
          order: p.preferenceOrder || (idx + 1),
          name: p.schoolName || p.name || 'Campus Choice'
        };
      });
    }
    if (app.firstSchoolPreference) {
      return [{ order: 1, name: app.firstSchoolPreference }];
    }
    return [];
  }

  updateApplicantStatus(id: number, newStatus: string) {
    if (!confirm(`Are you sure you want to update this application status to "${newStatus}"?`)) return;

    this.isUpdatingStatus = true;
    this.authService.updateApplicationStatus(id, {
      status: newStatus,
      remarks: this.statusUpdateRemarks
    }).subscribe({
      next: () => {
        this.isUpdatingStatus = false;
        this.notify(`Application status updated to ${newStatus}.`);
        if (this.selectedApplicant) {
          this.selectedApplicant.status = newStatus;
        }
        this.loadAdminApplications();
        this.loadAdminStats();
      },
      error: () => {
        this.isUpdatingStatus = false;
        this.notify('Failed to update application status.', 'error');
      }
    });
  }

  toggleUser(user: UserAccount) {
    this.authService.toggleUserStatus(user.id).subscribe({
      next: () => {
        user.isActive = !user.isActive;
        this.notify(`User ${user.username} status updated to ${user.isActive ? 'Active' : 'Inactive'}.`);
      },
      error: () => this.notify('Failed to toggle user status.', 'error')
    });
  }

  // ==================== CLIENT / PARENT DASHBOARD ====================

  loadClientData() {
    if (!this.currentUser?.registrationId) return;
    this.isLoadingClient = true;
    this.authService.getApplicationDetails(this.currentUser.registrationId).subscribe({
      next: (data) => {
        this.clientApplication = data;
        this.isLoadingClient = false;
      },
      error: () => {
        this.isLoadingClient = false;
      }
    });
  }

  printClientApplication() {
    window.print();
  }

  // ==================== MASTER CONTENT CRUD (ADMIN ONLY) ====================

  notify(message: string, type: 'success' | 'error' = 'success') {
    this.adminNotification = { message, type };
    setTimeout(() => {
      if (this.adminNotification?.message === message) {
        this.adminNotification = null;
      }
    }, 4000);
  }

  openAddGuideline() {
    this.isEditingGuideline = false;
    this.guidelineForm = {
      title: '',
      detail: '',
      link: '',
      linkText: '',
      displayOrder: this.allGuidelines.length + 1,
      isActive: true
    };
    this.showGuidelineModal = true;
  }

  openEditGuideline(item: GuidelineInstruction) {
    this.isEditingGuideline = true;
    this.guidelineForm = { ...item };
    this.showGuidelineModal = true;
  }

  saveGuideline() {
    if (!this.guidelineForm.title?.trim() || !this.guidelineForm.detail?.trim()) {
      this.notify('Please provide both Title and Detail.', 'error');
      return;
    }

    if (this.isEditingGuideline && this.guidelineForm.id) {
      this.portalService.updateGuideline(this.guidelineForm.id, this.guidelineForm).subscribe({
        next: () => {
          this.notify('Guideline updated successfully.');
          this.showGuidelineModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to update guideline.', 'error')
      });
    } else {
      this.portalService.createGuideline(this.guidelineForm).subscribe({
        next: () => {
          this.notify('Guideline created successfully.');
          this.showGuidelineModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to create guideline.', 'error')
      });
    }
  }

  toggleGuideline(item: GuidelineInstruction) {
    const newStatus = !item.isActive;
    this.portalService.toggleGuidelineStatus(item.id, newStatus).subscribe({
      next: () => {
        item.isActive = newStatus;
        this.notify(`Guideline status changed to ${newStatus ? 'Active' : 'Inactive'}.`);
        this.loadBackendData();
      },
      error: () => this.notify('Failed to toggle guideline status.', 'error')
    });
  }

  deleteGuideline(item: GuidelineInstruction) {
    if (!confirm(`Are you sure you want to soft delete guideline #${item.id}: "${item.title}"?`)) return;

    this.portalService.deleteGuideline(item.id).subscribe({
      next: () => {
        this.notify('Guideline removed successfully (Soft Deleted).');
        this.loadMasterContent();
        this.loadBackendData();
      },
      error: () => this.notify('Failed to delete guideline.', 'error')
    });
  }

  // Schools CRUD
  openAddSchool() {
    this.isEditingSchool = false;
    this.schoolForm = {
      slNo: this.allSchools.length + 1,
      name: '',
      code: '',
      syllabus: 'CBSE',
      classes: 'KG I – IX & XI',
      location: '',
      website: 'https://',
      displayOrder: this.allSchools.length + 1,
      isActive: true
    };
    this.showSchoolModal = true;
  }

  openEditSchool(item: SchoolInfo) {
    this.isEditingSchool = true;
    this.schoolForm = { ...item };
    this.showSchoolModal = true;
  }

  saveSchool() {
    if (!this.schoolForm.name?.trim() || !this.schoolForm.code?.trim()) {
      this.notify('Please provide School Name and Code.', 'error');
      return;
    }

    if (this.isEditingSchool && this.schoolForm.id) {
      this.portalService.updateSchool(this.schoolForm.id, this.schoolForm).subscribe({
        next: () => {
          this.notify('School updated successfully.');
          this.showSchoolModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to update school.', 'error')
      });
    } else {
      this.portalService.createSchool(this.schoolForm).subscribe({
        next: () => {
          this.notify('School added successfully.');
          this.showSchoolModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to create school.', 'error')
      });
    }
  }

  toggleSchool(item: SchoolInfo) {
    if (!item.id) return;
    const newStatus = !item.isActive;
    this.portalService.toggleSchoolStatus(item.id, newStatus).subscribe({
      next: () => {
        item.isActive = newStatus;
        this.notify(`School status changed to ${newStatus ? 'Active' : 'Inactive'}.`);
        this.loadBackendData();
      },
      error: () => this.notify('Failed to toggle school status.', 'error')
    });
  }

  deleteSchool(item: SchoolInfo) {
    if (!item.id) return;
    if (!confirm(`Are you sure you want to soft delete school "${item.name}"?`)) return;

    this.portalService.deleteSchool(item.id).subscribe({
      next: () => {
        this.notify('School deleted successfully (Soft Deleted).');
        this.loadMasterContent();
        this.loadBackendData();
      },
      error: () => this.notify('Failed to delete school.', 'error')
    });
  }

  // Links CRUD
  openAddLink() {
    this.isEditingLink = false;
    this.linkForm = {
      title: '',
      section: 'ADMISSION_LINK',
      linkType: 'PDF_DOCUMENT',
      targetUrl: 'assets/docs/',
      description: '',
      displayOrder: this.allLinks.length + 1,
      isActive: true,
      openInNewTab: false
    };
    this.showLinkModal = true;
  }

  openEditLink(item: PortalLink) {
    this.isEditingLink = true;
    this.linkForm = { ...item };
    this.showLinkModal = true;
  }

  saveLink() {
    if (!this.linkForm.title?.trim() || !this.linkForm.targetUrl?.trim()) {
      this.notify('Please provide Title and Target URL.', 'error');
      return;
    }

    if (this.isEditingLink && this.linkForm.id) {
      this.portalService.updateLink(this.linkForm.id, this.linkForm).subscribe({
        next: () => {
          this.notify('Portal Link updated successfully.');
          this.showLinkModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to update portal link.', 'error')
      });
    } else {
      this.portalService.createLink(this.linkForm).subscribe({
        next: () => {
          this.notify('Portal Link created successfully.');
          this.showLinkModal = false;
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: () => this.notify('Failed to create link.', 'error')
      });
    }
  }

  toggleLink(item: PortalLink) {
    const newStatus = !item.isActive;
    this.portalService.toggleLinkStatus(item.id, newStatus).subscribe({
      next: () => {
        item.isActive = newStatus;
        this.notify(`Link status changed to ${newStatus ? 'Active' : 'Inactive'}.`);
        this.loadBackendData();
      },
      error: () => this.notify('Failed to toggle link status.', 'error')
    });
  }

  deleteLink(item: PortalLink) {
    if (!confirm(`Are you sure you want to soft delete link "${item.title}"?`)) return;

    this.portalService.deleteLink(item.id).subscribe({
      next: () => {
        this.notify('Link deleted successfully (Soft Deleted).');
        this.loadMasterContent();
        this.loadBackendData();
      },
      error: () => this.notify('Failed to delete link.', 'error')
    });
  }

  // Configs CRUD
  openEditConfig(item: PortalConfig) {
    this.editingConfigKey = item.configKey;
    this.configForm = {
      configValue: item.configValue,
      description: item.description || '',
      isActive: item.isActive
    };
  }

  cancelEditConfig() {
    this.editingConfigKey = null;
  }

  saveConfig(key: string) {
    this.portalService.updateConfig(key, this.configForm).subscribe({
      next: () => {
        this.notify(`Configuration '${key}' updated successfully.`);
        this.editingConfigKey = null;
        this.loadMasterContent();
        this.loadBackendData();
      },
      error: () => this.notify(`Failed to update config '${key}'.`, 'error')
    });
  }

  isUploadingLogo = false;

  onLogoFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.isUploadingLogo = true;
      this.portalService.uploadLogo(file).subscribe({
        next: () => {
          this.isUploadingLogo = false;
          this.notify('Portal logo uploaded and saved to database successfully!');
          this.loadMasterContent();
          this.loadBackendData();
        },
        error: (err) => {
          this.isUploadingLogo = false;
          this.notify(err?.error?.message || 'Failed to upload logo.', 'error');
        }
      });
    }
  }

  get logoUrl(): string {
    const raw = this.contact?.logoUrl;
    if (!raw) return 'assets/logo.png';
    if (raw.startsWith('http://') || raw.startsWith('https://') || raw.startsWith('assets/')) {
      return raw;
    }
    const backendRoot = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${backendRoot}/${raw.replace(/^\//, '')}`;
  }

  get brandTitle(): string {
    return this.contact?.brandTitle || 'Indian Schools Oman';
  }

  get brandSubTitle(): string {
    return this.contact?.brandSubTitle || 'Central Admission System';
  }

  // ==================== PUBLIC ACTIONS ====================

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

  resolvePdfUrl(rawUrl?: string): string {
    if (!rawUrl) return '';
    if (rawUrl.startsWith('http://') || rawUrl.startsWith('https://')) {
      return rawUrl;
    }
    if (rawUrl.startsWith('documents/') || rawUrl.startsWith('uploads/')) {
      const backendRoot = environment.apiUrl.replace(/\/api\/?$/, '');
      return `${backendRoot}/${rawUrl.replace(/^\//, '')}`;
    }
    if (rawUrl.startsWith('/')) {
      return rawUrl;
    }
    return `/${rawUrl}`;
  }

  openPdfModal(link: PortalLink) {
    this.activeDoc = link;
    const resolved = this.resolvePdfUrl(link.targetUrl);
    this.safeDocUrl = this.sanitizer.bypassSecurityTrustResourceUrl(resolved);
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
      const resolved = this.resolvePdfUrl(this.activeDoc.targetUrl);
      window.open(resolved, '_blank');
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

  forgotPasswordHint() {
    alert('Parents can log in using their Application Registration No. as Username and Student Passport Number as Password. For further assistance, please contact the Admission Helpline.');
  }

  openNewApplication() {
    this.currentView = 'guidelines';
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  openAdminView() {
    if (this.currentUser?.role === 'ADMIN') {
      this.currentView = 'admin';
      this.loadAdminData();
    } else {
      this.notify('Please log in with Administrator credentials to access Admin Suite.', 'error');
      this.currentView = 'links';
    }
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

  private loadFallbackData() {
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
      { id: 1, slNo: 1, name: 'Indian School Muscat', code: 'ISM', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Darsait / Muscat', website: 'https://ismoman.com', displayOrder: 1, isActive: true },
      { id: 2, slNo: 2, name: 'Indian School Darsait', code: 'ISD', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Darsait', website: 'https://isdoman.com', displayOrder: 2, isActive: true },
      { id: 3, slNo: 3, name: 'Indian School Al Wadi Al Kabir', code: 'ISWK', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Wadi Kabir', website: 'https://iswkoman.com', displayOrder: 3, isActive: true },
      { id: 4, slNo: 4, name: 'Indian School Al Wadi Al Kabir International', code: 'ISWKi', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI', location: 'Wadi Kabir', website: 'https://iswkoman.com', displayOrder: 4, isActive: true },
      { id: 5, slNo: 5, name: 'Indian School Al Ghubra', code: 'ISG', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Ghubra', website: 'https://isgoman.com', displayOrder: 5, isActive: true },
      { id: 6, slNo: 6, name: 'Indian School Al Ghubra International', code: 'ISGi', syllabus: 'CAMBRIDGE', classes: 'KG I – IX & XI', location: 'Al Ghubra', website: 'https://isgoman.com', displayOrder: 6, isActive: true },
      { id: 7, slNo: 7, name: 'Indian School Bousher', code: 'ISB', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Bousher', website: 'https://isboman.com', displayOrder: 7, isActive: true },
      { id: 8, slNo: 8, name: 'Indian School Seeb', code: 'ISAS', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Seeb', website: 'https://isseeoman.com', displayOrder: 8, isActive: true },
      { id: 9, slNo: 9, name: 'Indian School Maabela', code: 'ISAM', syllabus: 'CBSE', classes: 'KG I – IX & XI', location: 'Al Maabela', website: 'https://isamoman.com', displayOrder: 9, isActive: true }
    ];
    this.guidelines = [
      { id: 1, title: 'Eligibility', detail: 'This online registration form is meant for Indian Nationals seeking new admissions in Indian Schools in the capital area for the academic year 2026-2027.', isActive: true },
      { id: 2, title: 'Single Mandatory Application', detail: 'Online registration is mandatory. There is only one application form required for one child; our system will not accept duplicate passport entries.', isActive: true },
      { id: 3, title: 'Credentials & Notifications', detail: 'A unique login registration number and password will be generated automatically upon submission and sent to your registered email and mobile number.', isActive: true },
      { id: 4, title: 'Application Processing Fee', detail: 'A non-refundable processing fee of OMR 15/- is payable upon successful submission of the application form.', isActive: true },
      { id: 5, title: 'Sibling Preference Rule', detail: 'Online application is mandatory even for sibling admissions. To claim sibling preference, the parent must select the sibling\'s school as their First Preference.', isActive: true },
      { id: 6, title: 'Seat Vacancies', detail: 'Tentative vacancies across different schools are dynamically updated on the portal for parents to review before submitting preferences.', isActive: true },
      { id: 7, title: 'Admission Allotment', detail: 'School allotment is strictly subject to vacancy availability and merit criteria set by the Board of Directors.', isActive: true },
      { id: 8, title: 'Help & Queries', detail: 'Parents are strongly advised to check the Frequently Asked Questions (FAQs) section for guidance on common registration questions.', isActive: true },
      { id: 9, title: 'Inter-School Transfer', detail: 'Parents seeking inter-school transfer for their wards must complete the dedicated transfer portal:', link: 'https://forms.gle/P29avN2BoVufqWGz5', linkText: 'Inter-School Transfer Form', isActive: true },
      { id: 10, title: 'Other Nationalities', detail: 'Parents of non-Indian nationalities seeking admission in Indian schools must apply through the external foreign quota portal:', link: 'https://forms.gle/hEUAnuLePfyTveD89', linkText: 'Other Nationalities Form', isActive: true }
    ];
  }
}
