import { Component, Input, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditorMeta } from './data/editor-meta';
import { FormsModule } from '@angular/forms';
import { EditorService } from './editor-service';
import { EditorData } from './data/editor-data';

@Component({
  selector: 'app-editor-popup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './editor-popup.component.html',
  styleUrl: './editor-popup.component.css'
})
export class EditorPopupComponent {
  @Input() title: string = 'Edit';

  @ViewChild('popupTemplate')
  private popupTemplate!: TemplateRef<any>;

  data!: EditorData;
  meta!: EditorMeta[];

  constructor(private modalService: NgbModal, private editorService: EditorService) { }

  ngOnInit(): void {
    this.editorService.setAction((data, meta) => {
      this.data = data;
      this.meta = meta;

      return new Promise<boolean>((resolve, reject) => {
        this.modalService.open(this.popupTemplate, { ariaLabelledBy: 'modal-basic-title' }).result.then(
          (result) => {
            resolve(true);
          }, (reason) => {
            resolve(false);
          }
        );
      });
    });
  }
}
