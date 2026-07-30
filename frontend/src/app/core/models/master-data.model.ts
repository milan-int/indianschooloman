export interface MasterDataResponse {
  id: number;
  name: string;
}

export interface CountryMaster extends MasterDataResponse {
  code: string;
}

export interface PostalCodeMaster {
  id: number;
  code: string;
  name: string;
}

export interface GradeSchoolDto {
  id: number;
  schoolName: string;
}

export interface GradeMaster {
  id: number;
  gradeCode: string;
  gradeDisplay: string;
  preferenceType: number;
  schools: GradeSchoolDto[];
}
