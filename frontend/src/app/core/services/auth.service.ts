import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserAccount, LoginRequest, LoginResponse, AdminDashboardStats, ApplicantSummary, UpdateApplicationStatusPayload } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private readonly authUrl = `${environment.apiUrl}/auth`;
  private readonly adminUrl = `${environment.apiUrl}/admin`;

  private currentUserSubject = new BehaviorSubject<UserAccount | null>(this.getStoredUser());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {}

  get currentUser(): UserAccount | null {
    return this.currentUserSubject.value;
  }

  get isLoggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  get isAdmin(): boolean {
    return this.currentUserSubject.value?.role === 'ADMIN';
  }

  get isClient(): boolean {
    return this.currentUserSubject.value?.role === 'CLIENT';
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.authUrl}/login`, credentials).pipe(
      tap(res => {
        if (res.success && res.user) {
          localStorage.setItem('iso_auth_user', JSON.stringify(res.user));
          localStorage.setItem('iso_auth_token', res.token);
          this.currentUserSubject.next(res.user);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('iso_auth_user');
    localStorage.removeItem('iso_auth_token');
    this.currentUserSubject.next(null);
  }

  private getStoredUser(): UserAccount | null {
    try {
      const stored = localStorage.getItem('iso_auth_user');
      return stored ? JSON.parse(stored) : null;
    } catch {
      return null;
    }
  }

  // ==================== ADMIN SUITE ENDPOINTS ====================

  getAdminStats(): Observable<AdminDashboardStats> {
    return this.http.get<AdminDashboardStats>(`${this.adminUrl}/stats`);
  }

  getAdminApplications(
    search?: string,
    school?: string,
    status?: string,
    className?: string
  ): Observable<ApplicantSummary[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (school && school !== 'ALL') params = params.set('school', school);
    if (status && status !== 'ALL') params = params.set('status', status);
    if (className && className !== 'ALL') params = params.set('className', className);

    return this.http.get<ApplicantSummary[]>(`${this.adminUrl}/applications`, { params });
  }

  getApplicationDetails(id: number): Observable<any> {
    return this.http.get<any>(`${this.adminUrl}/applications/${id}`);
  }

  updateApplicationStatus(id: number, payload: UpdateApplicationStatusPayload): Observable<any> {
    return this.http.patch<any>(`${this.adminUrl}/applications/${id}/status`, payload);
  }

  getAllUsers(): Observable<UserAccount[]> {
    return this.http.get<UserAccount[]>(`${this.adminUrl}/users`);
  }

  toggleUserStatus(id: number): Observable<any> {
    return this.http.patch<any>(`${this.adminUrl}/users/${id}/toggle`, {});
  }
}
