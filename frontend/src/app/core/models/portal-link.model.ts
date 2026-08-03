export interface PortalLink {
  id: number;
  title: string;
  section: 'ADMISSION_LINK' | 'FOOTER_LINK' | string;
  linkType: 'INTERNAL_ROUTE' | 'PDF_DOCUMENT' | 'EXTERNAL_URL' | string;
  targetUrl: string;
  description?: string;
  displayOrder: number;
  isActive: boolean;
  openInNewTab: boolean;
}

export interface SchoolInfo {
  slNo: number;
  name: string;
  code: string;
  syllabus: 'CBSE' | 'CAMBRIDGE' | string;
  classes: string;
  location: string;
  website: string;
}

export interface GuidelineInstruction {
  id: number;
  title: string;
  detail: string;
  link?: string;
  linkText?: string;
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
