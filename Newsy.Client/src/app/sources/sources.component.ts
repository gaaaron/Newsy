import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Source } from '../core/models/source';
import { SourceService } from '../core/services/source.service';
import { AsyncPipe } from '@angular/common';
import { SourceEditorPopupComponent } from './source-editor-popup/source-editor-popup.component';
import { AppToastService } from '../core/services/toast.service';

@Component({
  selector: 'app-sources',
  standalone: true,
  imports: [AsyncPipe, SourceEditorPopupComponent],
  templateUrl: './sources.component.html'
})
export class SourcesComponent {
  sources$!: Observable<Source[]>;
  constructor(private sourceService: SourceService, private toast: AppToastService) {}

  ngOnInit(): void {
    this.sources$ = this.sourceService.getAll();
  }

  refresh(){
    this.sources$ = this.sourceService.getAll();
  }

  remove(id:string) {
    this.sourceService.delete(id).subscribe(result => {
      this.refresh();
    });
  }

  scrape(id:string) {
    this.sourceService.scrape(id).subscribe(result => {
      this.toast.show('Scrape finished', result);
    });
  }
}
