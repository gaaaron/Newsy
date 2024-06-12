import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Feed } from '../core/models/feed';
import { Observable } from 'rxjs';
import { FeedService } from '../core/services/feed.service';
import { AsyncPipe } from '@angular/common';
import { EventBus } from '../core/services/eventbus.service';
import { BusEvents } from '../core/models/emit_event';
import { EditorService } from '../core/components/editor-popup/editor-service';
import { EditorMeta } from '../core/components/editor-popup/data/editor-meta';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterLink, AsyncPipe],
  templateUrl: './menu.component.html'
})
export class MenuComponent {
  feeds$!: Observable<Feed[]>;

  constructor(private feedService: FeedService, private eventBus: EventBus, private editorService: EditorService) {}

  ngOnInit(): void {
    this.feeds$ = this.feedService.getAll();
    this.eventBus.on(BusEvents.feedRemoved, () => this.refresh());
  }

  refresh(){
    this.feeds$ = this.feedService.getAll();
  }

  async createFeed() : Promise<void> {
    let feed = { name: '' };
    let result = await this.editorService.show(feed, [
      new EditorMeta('name', 'Name', 'text')]);

    if (result) {    
      this.feedService.create(feed).subscribe(_ => this.refresh());
    }
  }
}
