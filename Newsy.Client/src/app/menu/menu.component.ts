import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Feed } from '../core/models/feed';
import { Observable } from 'rxjs';
import { FeedService } from '../core/services/feed.service';
import { AsyncPipe } from '@angular/common';
import { FeedCreatorPopupComponent } from './feed-creator-popup/feed-creator-popup.component';
import { EventBus } from '../core/services/eventbus.service';
import { BusEvents } from '../core/models/emit_event';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterLink, AsyncPipe, FeedCreatorPopupComponent],
  templateUrl: './menu.component.html'
})
export class MenuComponent {
  feeds$!: Observable<Feed[]>;

  constructor(private feedService: FeedService, private eventBus: EventBus) {}

  ngOnInit(): void {
    this.feeds$ = this.feedService.getAll();
    this.eventBus.on(BusEvents.feedRemoved, () => this.refresh());
  }

  refresh(){
    this.feeds$ = this.feedService.getAll();
  }
}
