import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { FeedService } from '../../core/services/feed.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-feed-creator-popup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './feed-creator-popup.component.html'
})
export class FeedCreatorPopupComponent {
  name:string = '';
  @Output() feedCreated = new EventEmitter<string>();

  constructor(private feedService: FeedService, private modalService: NgbModal) {}

  open(content: TemplateRef<any>) {
		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
        console.log(result);
        let feed = { name: this.name };
        var createResult = this.feedService.create(feed);

        createResult.subscribe(result => {
          this.feedCreated.emit(result.feedId);
        });
			},(reason) => {}
		);
	}
}
