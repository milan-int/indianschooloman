import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LandingPageData, PortalLink } from '../models/portal-link.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PortalService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/portal`;

  /**
   * Retrieves active admission and footer links for the landing page
   */
  getLandingData(): Observable<LandingPageData> {
    return this.http.get<LandingPageData>(`${this.baseUrl}/landing-data`);
  }

  /**
   * Retrieves all links (including inactive) for administration
   */
  getAllLinks(): Observable<PortalLink[]> {
    return this.http.get<PortalLink[]>(`${this.baseUrl}/links`);
  }

  /**
   * Toggles active/inactive status of a link by id
   */
  toggleLinkStatus(id: number, isActive: boolean): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.baseUrl}/links/${id}/status`, { isActive });
  }
}
