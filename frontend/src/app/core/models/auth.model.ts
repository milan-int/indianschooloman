export interface UserAccount {
  id: number;
  username: string;
  email: string;
  role: 'ADMIN' | 'CLIENT' | string;
  fullName: string;
  phoneNumber: string;
  registrationId?: number | null;
  registrationNo?: string | null;
  isActive: boolean;
  lastLoginAt?: string | null;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  token: string;
  user: UserAccount | null;
}

export interface AdminDashboardStats {
  totalApplications: number;
  submittedCount: number;
  underVerificationCount: number;
  approvedCount: number;
  seatAllottedCount: number;
  rejectedCount: number;
  totalSchools: number;
  totalGuidelines: number;
  totalUsers: number;
  applicationsByClass: { [key: string]: number };
  applicationsByFirstPreference: { [key: string]: number };
}

export interface ApplicantSummary {
  id: number;
  registrationNo: string;
  status: string;
  createdAt: string;
  submittedAt?: string | null;
  studentFullName: string;
  passportNumber: string;
  admissionClass: string;
  gender: string;
  dateOfBirth: string;
  parentFullName: string;
  parentRelationship: string;
  parentMobileNo: string;
  parentEmail: string;
  parentCivilId: string;
  firstSchoolPreference: string;
  schoolPreferences: string[];
  locality: string;
  postalCode: string;
  siblingCount: number;
}

export interface UpdateApplicationStatusPayload {
  status: string;
  remarks?: string;
  allottedSchool?: string;
}
