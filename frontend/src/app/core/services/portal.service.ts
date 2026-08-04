import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LandingPageData, PortalLink, SchoolInfo, GuidelineInstruction, PortalConfig } from '../models/portal-link.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PortalService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/portal`;

  /**
   * Retrieves active admission links, footer links, schools, guidelines, and contact info for the landing page
   */
  getLandingData(): Observable<LandingPageData> {
    return this.http.get<LandingPageData>(`${this.baseUrl}/landing-data`);
  }

  // ==================== LINKS ====================
  getAllLinks(includeDeleted: boolean = false): Observable<PortalLink[]> {
    return this.http.get<PortalLink[]>(`${this.baseUrl}/links?includeDeleted=${includeDeleted}`);
  }

  createLink(dto: Partial<PortalLink>): Observable<PortalLink> {
    return this.http.post<PortalLink>(`${this.baseUrl}/links`, dto);
  }

  updateLink(id: number, dto: Partial<PortalLink>): Observable<PortalLink> {
    return this.http.put<PortalLink>(`${this.baseUrl}/links/${id}`, dto);
  }

  toggleLinkStatus(id: number, isActive: boolean): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.baseUrl}/links/${id}/status`, { isActive });
  }

  deleteLink(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.baseUrl}/links/${id}`);
  }

  // ==================== SCHOOLS ====================
  getAllSchools(includeDeleted: boolean = false): Observable<SchoolInfo[]> {
    return this.http.get<SchoolInfo[]>(`${this.baseUrl}/schools?includeDeleted=${includeDeleted}`);
  }

  createSchool(dto: Partial<SchoolInfo>): Observable<SchoolInfo> {
    return this.http.post<SchoolInfo>(`${this.baseUrl}/schools`, dto);
  }

  updateSchool(id: number, dto: Partial<SchoolInfo>): Observable<SchoolInfo> {
    return this.http.put<SchoolInfo>(`${this.baseUrl}/schools/${id}`, dto);
  }

  toggleSchoolStatus(id: number, isActive: boolean): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.baseUrl}/schools/${id}/status`, { isActive });
  }

  deleteSchool(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.baseUrl}/schools/${id}`);
  }

  // ==================== GUIDELINES ====================
  getAllGuidelines(includeDeleted: boolean = false): Observable<GuidelineInstruction[]> {
    return this.http.get<GuidelineInstruction[]>(`${this.baseUrl}/guidelines?includeDeleted=${includeDeleted}`);
  }

  createGuideline(dto: Partial<GuidelineInstruction>): Observable<GuidelineInstruction> {
    return this.http.post<GuidelineInstruction>(`${this.baseUrl}/guidelines`, dto);
  }

  updateGuideline(id: number, dto: Partial<GuidelineInstruction>): Observable<GuidelineInstruction> {
    return this.http.put<GuidelineInstruction>(`${this.baseUrl}/guidelines/${id}`, dto);
  }

  toggleGuidelineStatus(id: number, isActive: boolean): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.baseUrl}/guidelines/${id}/status`, { isActive });
  }

  deleteGuideline(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.baseUrl}/guidelines/${id}`);
  }

  // ==================== CONFIGS ====================
  getAllConfigs(): Observable<PortalConfig[]> {
    return this.http.get<PortalConfig[]>(`${this.baseUrl}/configs`);
  }

  updateConfig(key: string, dto: { configValue: string; description?: string; isActive?: boolean }): Observable<PortalConfig> {
    return this.http.put<PortalConfig>(`${this.baseUrl}/configs/${key}`, dto);
  }

  uploadLogo(file: File): Observable<{ message: string; fileName: string; url: string }> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ message: string; fileName: string; url: string }>(`${this.baseUrl}/upload-logo`, formData);
  }
}
