export interface PortalLink {
  id: number;
  title: string;
  section: 'ADMISSION_LINK' | 'FOOTER_LINK' | string;
  linkType: 'INTERNAL_ROUTE' | 'PDF_DOCUMENT' | 'EXTERNAL_URL' | string;
  targetUrl: string;
  description?: string;
  displayOrder: number;
  isActive: boolean;
  isDeleted?: boolean;
  openInNewTab: boolean;
}

export interface SchoolInfo {
  id?: number;
  slNo: number;
  name: string;
  code: string;
  syllabus: 'CBSE' | 'CAMBRIDGE' | string;
  classes: string;
  location: string;
  website: string;
  displayOrder?: number;
  isActive?: boolean;
  isDeleted?: boolean;
}

export interface GuidelineInstruction {
  id: number;
  displayOrder?: number;
  title: string;
  detail: string;
  link?: string;
  linkText?: string;
  isActive?: boolean;
  isDeleted?: boolean;
}

export interface PortalConfig {
  id: number;
  configKey: string;
  configValue: string;
  section: string;
  description?: string;
  isActive: boolean;
  isDeleted?: boolean;
}

export interface PortalContact {
  helplinePhone: string;
  helplineEmail: string;
  officeHours: string;
  academicYear: string;
}

export interface LandingPageData {
  admissionLinks: PortalLink[];
  footerLinks: PortalLink[];
  schools: SchoolInfo[];
  guidelines: GuidelineInstruction[];
  contact: PortalContact;
}
