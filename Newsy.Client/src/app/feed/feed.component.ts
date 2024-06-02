import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FeedService } from '../core/services/feed.service';
import { Observable } from 'rxjs';
import { AsyncPipe, DatePipe } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { FeedTagPopupComponent } from './feed-tag-popup/feed-tag-popup.component';
import { FeedDetails } from '../core/models/feed_details';
import { EventBus } from '../core/services/eventbus.service';
import { BusEvents, EmitEvent } from '../core/models/emit_event';

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [AsyncPipe, DatePipe, NgbDropdownModule, FeedTagPopupComponent],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent {
  id: string | null = '';
  feedDetails$!: Observable<FeedDetails>;
  constructor(private route: ActivatedRoute, 
    private router: Router, private feedService: FeedService, private eventBus: EventBus) { }

  ngOnInit(): void {
    this.refresh();
    this.route.params.subscribe(params => {
      this.refresh();
    });
  }

  refresh() : void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.feedDetails$ = this.feedService.get(this.id!);
  }

  remove(id:string) {
    this.feedService.delete(id).subscribe(result => {  
      this.eventBus.emit(new EmitEvent(BusEvents.feedRemoved, id));
      this.router.navigate(['/']); 
    });
  }
}
