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

export interface LandingPageData {
  admissionLinks: PortalLink[];
  footerLinks: PortalLink[];
}
