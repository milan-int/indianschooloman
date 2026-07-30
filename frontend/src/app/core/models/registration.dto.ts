export interface RegistrationDto {
  pupilFirstName: string;
  pupilSurname: string;
  passportNumber: string;
  passportExpiryDate: string;
  classSought: string;
  sex: string;
  nationality: string;
  visaNumber: string;
  visaExpiryDate: string;
  motherTongue: string;
  previousSchool: string;
  country: string;
  classLastAttended: string;
  dateOfBirth: string;
  placeOfBirth: string;
  parentName: string;
  parentSurname: string;
  relationship: string;
  parentPassportNo: string;
  civilNo: string;
  employer: string;
  occupation: string;
  parentNationality: string;
  email: string;
  gsm: string;
  residentialPhone: string;
  officePhone: string;
  postalCode: string;
  poBox: string;
  permanentAddress: string;
  houseFlatNo: string;
  wayNo: string;
  streetName: string;
  locality: string;
  schoolPreferences: string[];
  siblingsStudyingCount: number;
  siblingsSeekingAdmissionCount: number;
  existingSiblings: ExistingSiblingDto[];
  newApplicantSiblings: NewApplicantSiblingDto[];

  // Declaration
  declaration: boolean;
}

export interface ExistingSiblingDto {
  siblingName: string;
  schoolName: string;
  grNumber: string;
  className: string;
  division: string;
}

export interface NewApplicantSiblingDto {
  passportNo: string;
  relationship: string;
}
