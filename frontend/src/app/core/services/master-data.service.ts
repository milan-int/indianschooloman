import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MasterDataResponse, CountryMaster, PostalCodeMaster } from '../models/master-data.model';

@Injectable({
  providedIn: 'root'
})
export class MasterDataService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'https://localhost:7130/api/masterdata';

  getCountries(): Observable<CountryMaster[]> {
    return this.http.get<CountryMaster[]>(`${this.baseUrl}/countries`);
  }

  getMotherTongues(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/mothertongues`);
  }

  getRelationships(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/relationships`);
  }

  getSiblingRelationships(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/siblingrelationships`);
  }

  getNationalities(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/nationalities`);
  }

  getPostalCodes(): Observable<PostalCodeMaster[]> {
    return this.http.get<PostalCodeMaster[]>(`${this.baseUrl}/postalcodes`);
  }

  getGrades(): Observable<import('../models/master-data.model').GradeMaster[]> {
    return this.http.get<import('../models/master-data.model').GradeMaster[]>(`${this.baseUrl}/grades`);
  }

  getSiblingSchools(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/siblingschools`);
  }

  getSiblingClasses(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/siblingclasses`);
  }

  getGenders(): Observable<MasterDataResponse[]> {
    return this.http.get<MasterDataResponse[]>(`${this.baseUrl}/genders`);
  }
}
