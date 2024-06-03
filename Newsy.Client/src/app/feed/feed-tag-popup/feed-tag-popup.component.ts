import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { FeedService } from '../../core/services/feed.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { FeedTag } from '../../core/models/feed_tag';
import { AsyncPipe } from '@angular/common';
import { UpdateFeedTags } from '../../core/models/update_feed_tags';

@Component({
  selector: 'app-feed-tag-popup',
  standalone: true,
  imports: [FormsModule, AsyncPipe],
  templateUrl: './feed-tag-popup.component.html'
})
export class FeedTagPopupComponent {
  name:string = '';
  tags: FeedTag[] = [];
  
  @Input() id:string = '';
  @Output() tagsUpdated = new EventEmitter<string>();

  constructor(private feedService: FeedService, private modalService: NgbModal) {}

  open(content: TemplateRef<any>) {
    this.feedService.getFeedTags(this.id).subscribe(tags => {
      this.tags = tags;
    });

		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
        var updateResult = this.feedService.updateFeedTags(new UpdateFeedTags(this.id, this.tags));
        updateResult.subscribe(result => {
          this.tagsUpdated.emit();
        });
			},(reason) => {}
		);
	}
}

