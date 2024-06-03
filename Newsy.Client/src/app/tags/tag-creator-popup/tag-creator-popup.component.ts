import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Output, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TagService } from '../../core/services/tag.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tag-creator-popup',
  standalone: true,
  imports: [FormsModule, AsyncPipe],
  templateUrl: './tag-creator-popup.component.html'
})
export class TagCreatorPopupComponent {
  name:string = '';
  textToMatch:string = '';
  @Output() tagCreated = new EventEmitter<string>();

  constructor(private tagService: TagService, private modalService: NgbModal) {}

  open(content: TemplateRef<any>) {
		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
        console.log(result);
        let tag = { name: this.name, textToMatch: this.textToMatch };
        var createResult = this.tagService.createContainsTag(tag);

        createResult.subscribe(id => {
          this.tagCreated.emit(id);
        });
			},(reason) => {}
		);
	}
}
