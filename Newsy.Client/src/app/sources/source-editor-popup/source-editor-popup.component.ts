import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { SourceService } from '../../core/services/source.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateOrdEditRssSource } from '../../core/models/create_or_edit_rss_source';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'source-editor-popup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './source-editor-popup.component.html'
})
export class SourceEditorPopupComponent {
  @Input() title:string = 'Create new RSS';
  @Input() icon:string = 'bi bi-plus';
  @Input() buttonClass:string = 'btn btn-sm btn-outline-success me-2';

  @Input() sourceId:string|null = null;
  @Input() sourceName:string = '';
  @Input() sourceRss:string = '';

  @Output() sourceSaved = new EventEmitter<string>();

  constructor(private sourceService: SourceService, private modalService: NgbModal) {}

  open(content: TemplateRef<any>) {
		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
        let source = new CreateOrdEditRssSource(this.sourceId, this.sourceName, this.sourceRss);
        var createResult = this.sourceService.createOrdEditRss(source);

        createResult.subscribe(result => {
          this.sourceSaved.emit(result.sourceId);
        });
			},(reason) => {}
		);
	}
}
