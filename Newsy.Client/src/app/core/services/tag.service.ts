import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  constructor(private http: HttpClient) {}
  getAll(): Observable<Tag[]> {
    return this.http.get<Tag[]>(`/api/Tag/GetAll`);
  }

  createContainsTag(tag: { name: string, textToMatch: string }): Observable<string> {
    return this.http.post<string>('/api/Tag/CreateContainsTag', tag); 
  }
}
