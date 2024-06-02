import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feed } from '../models/feed';
import { FeedContent } from '../models/feed_content';
import { FeedTag } from '../models/feed_tag';
import { UpdateFeedTags } from '../models/update_feed_tags';
import { FeedDetails } from '../models/feed_details';

@Injectable({
  providedIn: 'root'
})
export class FeedService {
  constructor(private http: HttpClient) {}

  create(feed: { name: string }): Observable<{ feedId: string }> {
    return this.http.post<{ feedId: string }>('/api/Feed/Create', feed); 
  }

  getAll(): Observable<Feed[]> {
    return this.http.get<Feed[]>(`/api/Feed/GetAll`);
  }

  get(id: string): Observable<FeedDetails> {
    return this.http.get<FeedDetails>(`/api/Feed/Get/${id}`);
  }

  getFeedTags(id: string): Observable<FeedTag[]> {
    return this.http.get<FeedTag[]>(`/api/Feed/GetFeedTags/${id}`);
  }

  updateFeedTags(feedTags: UpdateFeedTags): Observable<void> {
    return this.http.post<void>('/api/Feed/UpdateFeedTags', feedTags); 
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/Feed/Delete/${id}`);
  }
}
