import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Source } from '../models/source';
import { CreateOrdEditRssSource } from '../models/create_or_edit_rss_source';
import { ScrapeRss } from '../models/scrape_rss';

@Injectable({
  providedIn: 'root'
})
export class SourceService {
  constructor(private http: HttpClient) {}
  getAll(): Observable<Source[]> {
    return this.http.get<Source[]>(`/api/NewsSource/GetAll`);
  }

  createOrdEditRss(source: CreateOrdEditRssSource): Observable<{ sourceId: string }> {
    return this.http.post<{ sourceId: string }>('/api/NewsSource/CreateOrdEditRss', source); 
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/NewsSource/Delete/${id}`);
  }

  scrape(id: string): Observable<string> {
    let scrapeData = new ScrapeRss(id);
    return this.http.post<string>(`/api/NewsSource/ScrapeRss`, scrapeData);
  }
}
